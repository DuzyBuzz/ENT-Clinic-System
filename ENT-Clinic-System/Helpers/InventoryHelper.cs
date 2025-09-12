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
        public bool AddStockMovement(int itemId, string movementType, int quantity, bool applyDiscount = false)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // 🔹 Step 1: Get cost price from items first
                    decimal sellingPrice = 0;
                    string priceQuery = "SELECT selling_price FROM items WHERE item_id=@itemId";
                    using (var cmd = new MySqlCommand(priceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        object result = cmd.ExecuteScalar();
                        if (result != null) sellingPrice = Convert.ToDecimal(result);
                    }

                    // 🔹 Step 2: Now calculate price details correctly
                    var priceDetails = CalculateFinalPrice(sellingPrice, applyDiscount, quantity);

                    // 🔹 Step 3: Insert into stock_movements
                    string movementQuery = @"INSERT INTO stock_movements 
                        (item_id, movement_type, quantity, discount_amount, tax_amount)
                        VALUES (@itemId, @movementType, @quantity, @discount_amount, @tax_amount)";

                    using (var cmd = new MySqlCommand(movementQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Parameters.AddWithValue("@movementType", movementType);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@discount_amount", priceDetails.DiscountAmount);
                        cmd.Parameters.AddWithValue("@tax_amount", priceDetails.TaxAmount);
                        cmd.ExecuteNonQuery();
                    }

                    // 🔹 Step 4: If it's a StockOut, also insert into sales
                    if (movementType.Equals("StockOut", StringComparison.OrdinalIgnoreCase))
                    {
                        string salesQuery = @"INSERT INTO sales 
                          (item_id, quantity, unit_price, discount_amount, tax_amount, total_price)
                          VALUES (@itemId, @quantity, @unit_price, @discount_amount, @tax_amount, @total_price)";

                        using (var cmd = new MySqlCommand(salesQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@itemId", itemId);
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                            cmd.Parameters.AddWithValue("@unit_price", priceDetails.BasePrice / quantity); // per unit
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
                    string query = @"SELECT item_id, item_name, description, category, cost_price, selling_price, stock_quantity
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
