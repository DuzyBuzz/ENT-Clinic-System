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

        //// ================================
        //// 🔹 Smart Price Calculation
        //// ================================
        //public (decimal BasePrice, decimal DiscountAmount, decimal PriceAfterDiscount,
        //        decimal TaxAmount, decimal FinalPrice) CalculateFinalPrice(decimal sellingPrice, bool applyDiscount, int quantity = 1)
        //{
        //    decimal discountPercent = applyDiscount ? GetSettingValue("discount_percentage") : 0;
        //    decimal taxPercent = GetSettingValue("tax_percentage");
        //    //decimal markupPercent = GetSettingValue("markup_percentage");


        //    //// Step 1: Compute base selling price = cost + markup
        //    //decimal sellingPrice = costPrice * (1 + markupPercent / 100);

        //    // Step 2: Multiply by quantity
        //    decimal basePrice = sellingPrice * quantity;

        //    // Step 3: Apply discount first
        //    decimal discountAmount = basePrice * (discountPercent / 100);
        //    decimal priceAfterDiscount = basePrice - discountAmount;

        //    // Step 4: Apply tax after discount
        //    decimal taxAmount = priceAfterDiscount * (taxPercent / 100);

        //    // Step 5: Final price
        //    decimal finalPrice = priceAfterDiscount + taxAmount;

        //    return (basePrice, discountAmount, priceAfterDiscount, taxAmount, finalPrice);
        //}


        // ================================
        // 🔹 Stock Movements (insert + sales tracking)
        // ================================
        public bool AddStockMovement(int itemId, string movementType, int quantity, DateTime expirationDate, bool hasExpiration)
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

                    // 🔹 Step 2: Insert into stock_movements
                    string movementQuery = @"INSERT INTO stock_movements 
                (item_id, movement_type, quantity, unit_price, expiration_date)
                VALUES (@itemId, @movementType, @quantity, @unit_price, @expiration_date)";

                    using (var cmd = new MySqlCommand(movementQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Parameters.AddWithValue("@movementType", movementType);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@unit_price", sellingPrice);

                        if (hasExpiration)
                            cmd.Parameters.AddWithValue("@expiration_date", expirationDate);
                        else
                            cmd.Parameters.AddWithValue("@expiration_date", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in stock movement: " + ex.Message);
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
        public bool AddItem(string itemName, string description, string category, decimal costPrice, decimal sellingPrice)
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

        public bool UpdateItem(int itemId, string itemName, string description, string category, decimal costPrice, decimal sellingPrice)
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
        public int AddInvoice(
            string customerName,
            DataTable items,
            decimal subtotal,
            decimal discountAmount,
            decimal netTotal,
            decimal amountReceived,
            decimal changeDue,
            string discountPercentText,
            string note,
            string invoiceType)
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
                            // 🔹 Parse discount percent
                            decimal discountPercent = 0;
                            decimal.TryParse(discountPercentText, out discountPercent);

                            // 🔹 Step 1: Insert invoice header (no more calculations here)
                            string invoiceQuery = @"
                        INSERT INTO invoices 
                        (customer_name, invoice_date, subtotal, discount_amount, net_total, 
                         amount_received, change_due, invoice_type, note, discount_percent)
                        VALUES (@customer_name, NOW(), @subtotal, @discount_amount, @net_total, 
                                @amount_received, @change_due, @invoice_type, @note, @discount_percent);
                        SELECT LAST_INSERT_ID();";

                            int invoiceId;
                            using (var cmd = new MySqlCommand(invoiceQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@customer_name", customerName);
                                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                                cmd.Parameters.AddWithValue("@discount_amount", discountAmount);
                                cmd.Parameters.AddWithValue("@net_total", netTotal);
                                cmd.Parameters.AddWithValue("@amount_received", amountReceived);
                                cmd.Parameters.AddWithValue("@change_due", changeDue);
                                cmd.Parameters.AddWithValue("@invoice_type", invoiceType);
                                cmd.Parameters.AddWithValue("@note", note ?? "");
                                cmd.Parameters.AddWithValue("@discount_percent", discountPercent);

                                invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 🔹 Step 2: Insert invoice items
                            foreach (DataRow row in items.Rows)
                            {
                                int itemId = Convert.ToInt32(row["item_id"]);
                                int qty = Convert.ToInt32(row["quantity"]);
                                decimal price = Convert.ToDecimal(row["unit_price"]);
                                decimal itemTotal = price * qty;

                                // 🔹 NEW: Handle prescription_id if available
                                int prescriptionId = row.Table.Columns.Contains("prescription_id")
                                    ? Convert.ToInt32(row["prescription_id"])
                                    : 0;

                                string itemQuery = @"
                            INSERT INTO invoice_items 
                            (invoice_id, item_id, quantity, unit_price, total_price, prescription_id)
                            VALUES (@invoice_id, @item_id, @quantity, @unit_price, @total_price, @prescription_id)";

                                using (var cmd = new MySqlCommand(itemQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@invoice_id", invoiceId);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.Parameters.AddWithValue("@quantity", qty);
                                    cmd.Parameters.AddWithValue("@unit_price", price);
                                    cmd.Parameters.AddWithValue("@total_price", itemTotal);
                                    cmd.Parameters.AddWithValue("@prescription_id", prescriptionId > 0 ? (object)prescriptionId : DBNull.Value);

                                    cmd.ExecuteNonQuery();
                                }

                                // 🔹 Update stock only for ITEMS
                                if (invoiceType == "ITEMS")
                                {
                                    AddStockMovement(itemId, "OUT", qty, DateTime.Now, false);
                                }
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
