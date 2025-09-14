using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class InventoryHelper
    {
        // ================================
        // 🔹 Get system setting (e.g., tax, discount, markup)
        // ================================
        private decimal GetSettingValue(string key)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT setting_value FROM system_settings WHERE setting_key = @key LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", key);
                        object result = cmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal value))
                        {
                            return value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error fetching setting '{key}': {ex.Message}");
            }

            return 0; // default if not found
        }

        // ================================
        // 🔹 Smart Price Calculation
        // ================================
        public (decimal BasePrice, decimal DiscountAmount, decimal PriceAfterDiscount,
                decimal TaxAmount, decimal FinalPrice) CalculateFinalPrice(decimal sellingPrice, bool applyDiscount, int quantity = 1)
        {
            decimal discountPercent = applyDiscount ? GetSettingValue("discount_percentage") : 0;
            decimal taxPercent = GetSettingValue("tax_percentage");
            //decimal markupPercent = GetSettingValue("markup_percentage");


            //// Step 1: Compute base selling price = cost + markup
            //decimal sellingPrice = costPrice * (1 + markupPercent / 100);

            // Step 2: Multiply by quantity
            decimal basePrice = sellingPrice * quantity;

            // Step 3: Apply discount first
            decimal discountAmount = basePrice * (discountPercent / 100);
            decimal priceAfterDiscount = basePrice - discountAmount;

            // Step 4: Apply tax after discount
            decimal taxAmount = priceAfterDiscount * (taxPercent / 100);

            // Step 5: Final price
            decimal finalPrice = priceAfterDiscount + taxAmount;

            return (basePrice, discountAmount, priceAfterDiscount, taxAmount, finalPrice);
        }


        // ================================
        // 🔹 Stock Movements (insert + sales tracking)
        // ================================
        public bool AddStockMovement(int itemId, string movementType, int quantity, DateTime expirationDate, bool applyDiscount, bool hasExpiration)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // 🔹 Step 1: Get selling price (unit price)
                    decimal sellingPrice = 0;
                    string priceQuery = "SELECT selling_price FROM items WHERE item_id=@itemId";
                    using (var cmd = new MySqlCommand(priceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        object result = cmd.ExecuteScalar();
                        if (result != null) sellingPrice = Convert.ToDecimal(result);
                    }

                    // 🔹 Step 2: Calculate price details
                    var priceDetails = CalculateFinalPrice(sellingPrice, applyDiscount, quantity);

                    // 🔹 Step 3: Insert into stock_movements
                    string movementQuery = @"INSERT INTO stock_movements 
                (item_id, movement_type, quantity, discount_amount, tax_amount, expiration_date)
                VALUES (@itemId, @movementType, @quantity, @discount_amount, @tax_amount, @expiration_date)";

                    using (var cmd = new MySqlCommand(movementQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Parameters.AddWithValue("@movementType", movementType);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@discount_amount", priceDetails.DiscountAmount);
                        cmd.Parameters.AddWithValue("@tax_amount", priceDetails.TaxAmount);

                        if (hasExpiration)
                            cmd.Parameters.AddWithValue("@expiration_date", expirationDate);
                        else
                            cmd.Parameters.AddWithValue("@expiration_date", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // 🔹 Step 4: If Stock OUT, also insert into sales
                    if (movementType.Equals("OUT", StringComparison.OrdinalIgnoreCase))
                    {
                        string salesQuery = @"INSERT INTO sales 
                  (item_id, quantity, unit_price, discount_amount, tax_amount, total_price)
                  VALUES (@itemId, @quantity, @unit_price, @discount_amount, @tax_amount, @total_price)";

                        using (var cmd = new MySqlCommand(salesQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@itemId", itemId);
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                            cmd.Parameters.AddWithValue("@unit_price", sellingPrice); // ✅ unit price only
                            cmd.Parameters.AddWithValue("@discount_amount", priceDetails.DiscountAmount);
                            cmd.Parameters.AddWithValue("@tax_amount", priceDetails.TaxAmount);
                            cmd.Parameters.AddWithValue("@total_price", priceDetails.FinalPrice);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in stock movement/sales: " + ex.Message);
                return false;
            }
        }




        // ================================
        // 🔹 Stock Quantity
        // ================================
        public int GetStockQuantity(int itemId)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT stock_quantity FROM items WHERE item_id = @item_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error getting stock quantity: " + ex.Message);
                return 0;
            }
        }

        // ================================
        // 🔹 Get all items
        // ================================
        public DataTable GetAllItems()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT item_id, item_name, description, category, cost_price, selling_price, stock_quantity, created_at, updated_at
                                     FROM items ORDER BY item_name";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error fetching items: " + ex.Message);
            }
            return dt;
        }

        // ================================
        // 🔹 Item Management
        // ================================
        public bool AddItem(string itemName, string description, string category, decimal costPrice, decimal sellingPrice, bool applyDiscount = false)
        {
            try
            {

                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO items 
                                     (item_name, description, category, cost_price, selling_price, stock_quantity) 
                                     VALUES (@item_name, @description, @category, @cost_price, @selling_price, 0)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@cost_price", costPrice);
                        cmd.Parameters.AddWithValue("@selling_price", sellingPrice);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error adding item: " + ex.Message);
                return false;
            }
        }

        public bool UpdateItem(int itemId, string itemName, string description, string category, decimal costPrice, decimal sellingPrice, bool applyDiscount = false)
        {
            try
            {

                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE items 
                                     SET item_name=@item_name, description=@description, category=@category, cost_price=@cost_price, 
                                         selling_price=@selling_price
                                     WHERE item_id=@item_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@cost_price", costPrice);
                        cmd.Parameters.AddWithValue("@selling_price", sellingPrice);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error updating item: " + ex.Message);
                return false;
            }
        }
        public int AddInvoice(string customerName, DataTable items, decimal amountReceived)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            decimal subtotal = 0, totalDiscount = 0, totalTax = 0, netTotal = 0;

                            // 🔹 Step 1: Calculate totals
                            foreach (DataRow row in items.Rows)
                            {
                                int itemId = Convert.ToInt32(row["item_id"]);
                                int qty = Convert.ToInt32(row["quantity"]);
                                decimal price = Convert.ToDecimal(row["unit_price"]);
                                bool applyDiscount = Convert.ToBoolean(row["apply_discount"]);

                                var priceDetails = CalculateFinalPrice(price, applyDiscount, qty);

                                subtotal += priceDetails.BasePrice;
                                totalDiscount += priceDetails.DiscountAmount;
                                totalTax += priceDetails.TaxAmount;
                                netTotal += priceDetails.FinalPrice;
                            }

                            // 🔹 Calculate change
                            decimal changeDue = amountReceived - netTotal;
                            if (changeDue < 0) changeDue = 0;

                            // 🔹 Step 2: Insert invoice (header) with amount_received and change_due
                            string invoiceQuery = @"INSERT INTO invoices 
                    (customer_name, invoice_date, subtotal, discount_total, tax_total, net_total, amount_received, change_due)
                    VALUES (@customer_name, NOW(), @subtotal, @discount_total, @tax_total, @net_total, @amount_received, @change_due);
                    SELECT LAST_INSERT_ID();";

                            int invoiceId;
                            using (var cmd = new MySqlCommand(invoiceQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@customer_name", customerName);
                                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                                cmd.Parameters.AddWithValue("@discount_total", totalDiscount);
                                cmd.Parameters.AddWithValue("@tax_total", totalTax);
                                cmd.Parameters.AddWithValue("@net_total", netTotal);
                                cmd.Parameters.AddWithValue("@amount_received", amountReceived);
                                cmd.Parameters.AddWithValue("@change_due", changeDue);

                                invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 🔹 Step 3: Insert invoice items
                            foreach (DataRow row in items.Rows)
                            {
                                int itemId = Convert.ToInt32(row["item_id"]);
                                int qty = Convert.ToInt32(row["quantity"]);
                                decimal price = Convert.ToDecimal(row["unit_price"]);
                                bool applyDiscount = Convert.ToBoolean(row["apply_discount"]);

                                var priceDetails = CalculateFinalPrice(price, applyDiscount, qty);

                                string itemQuery = @"INSERT INTO invoice_items 
                        (invoice_id, item_id, quantity, unit_price, discount_amount, tax_amount, total_price)
                        VALUES (@invoice_id, @item_id, @quantity, @unit_price, @discount_amount, @tax_amount, @total_price)";

                                using (var cmd = new MySqlCommand(itemQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@invoice_id", invoiceId);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.Parameters.AddWithValue("@quantity", qty);
                                    cmd.Parameters.AddWithValue("@unit_price", price);
                                    cmd.Parameters.AddWithValue("@discount_amount", priceDetails.DiscountAmount);
                                    cmd.Parameters.AddWithValue("@tax_amount", priceDetails.TaxAmount);
                                    cmd.Parameters.AddWithValue("@total_price", priceDetails.FinalPrice);
                                    cmd.ExecuteNonQuery();
                                }

                                // 🔹 Step 4: Update stock and sales
                                AddStockMovement(itemId, "OUT", qty, DateTime.Now, applyDiscount, false);
                            }

                            transaction.Commit();
                            return invoiceId;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Error creating invoice: " + ex.Message);
                return -1;
            }
        }



        public bool DeleteItem(int itemId)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM items WHERE item_id=@item_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error deleting item: " + ex.Message);
                return false;
            }
        }
    }
}
