using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ENT_Clinic_System.Helpers
{
    internal class InventoryHelper
    {


        // ================================
        // 🔹 STOCK MOVEMENTS
        // ================================

        public bool AddStockMovement(int itemId, string movementType, int quantity, int? discountId = null, int? taxId = null)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO stock_movements (item_id, movement_type, quantity, discount_id, tax_id) 
                                     VALUES (@item_id, @movement_type, @quantity, @discount_id, @tax_id)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@movement_type", movementType);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@discount_id", (object)discountId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tax_id", (object)taxId ?? DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting stock movement: " + ex.Message);
                return false;
            }
        }

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
                Console.WriteLine("Error getting stock quantity: " + ex.Message);
                return 0;
            }
        }

        public DataTable GetAllItems()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT item_id, item_name, category, cost_price, selling_price, stock_quantity 
                                     FROM items ORDER BY item_name";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching items: " + ex.Message);
            }
            return dt;
        }

        // ================================
        // 🔹 ITEM MANAGEMENT
        // ================================

        /// <summary>
        /// Adds a new item to the inventory.
        /// </summary>
        public bool AddItem(string itemName, string category, decimal costPrice, decimal sellingPrice)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO items (item_name, category, cost_price, selling_price, stock_quantity) 
                                     VALUES (@item_name, @category, @cost_price, @selling_price, 0)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@cost_price", costPrice);
                        cmd.Parameters.AddWithValue("@selling_price", sellingPrice);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding item: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates an existing item (e.g. prices, category).
        /// </summary>
        public bool UpdateItem(int itemId, string itemName, string category, decimal costPrice, decimal sellingPrice)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE items 
                                     SET item_name=@item_name, category=@category, cost_price=@cost_price, selling_price=@selling_price 
                                     WHERE item_id=@item_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@cost_price", costPrice);
                        cmd.Parameters.AddWithValue("@selling_price", sellingPrice);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating item: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes an item from inventory (be careful!).
        /// </summary>
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
                Console.WriteLine("Error deleting item: " + ex.Message);
                return false;
            }
        }
    }
}
