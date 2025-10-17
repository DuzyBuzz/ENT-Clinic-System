using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class InventoryHelper
    {
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
                    string query = @"SELECT item_id, brand_name, generic_name, strength, dosage, category, description,
                                    cost_price, selling_price, quantity, created_at, updated_at
                                     FROM items ORDER BY brand_name";

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
        public bool DeleteStockMovement(int movementId)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM stock_movements WHERE movement_id=@movementId";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@movementId", movementId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting stock movement: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // ================================
        // 🔹 Add new item
        // ================================
        public bool AddItem(string brandName, string genericName, string strength, string dosage,
            string category, string description, decimal costPrice, decimal sellingPrice)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO items 
                                    (brand_name, generic_name, strength, dosage, category, description,
                                     cost_price, selling_price, quantity, created_at, updated_at)
                                     VALUES (@brand_name, @generic_name, @strength, @dosage, @category, @description,
                                             @cost_price, @selling_price, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brand_name", brandName);
                        cmd.Parameters.AddWithValue("@generic_name", genericName);
                        cmd.Parameters.AddWithValue("@strength", strength);
                        cmd.Parameters.AddWithValue("@dosage", dosage);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@description", description);
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

        // ================================
        // 🔹 Update item
        // ================================
        public bool UpdateItem(int itemId, string brandName, string genericName, string strength, string dosage,
            string category, string description, decimal costPrice, decimal sellingPrice)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE items 
                                     SET brand_name=@brand_name, generic_name=@generic_name, strength=@strength,
                                         dosage=@dosage, category=@category, description=@description,
                                         cost_price=@cost_price, selling_price=@selling_price, updated_at=CURRENT_TIMESTAMP
                                     WHERE item_id=@item_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@brand_name", brandName);
                        cmd.Parameters.AddWithValue("@generic_name", genericName);
                        cmd.Parameters.AddWithValue("@strength", strength);
                        cmd.Parameters.AddWithValue("@dosage", dosage);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@description", description);
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

        // ================================
        // 🔹 Delete item
        // ================================
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

        // ================================
        // 🔹 Add stock movement
        // ================================
        public bool AddStockMovement(int itemId, string movementType, int quantity, DateTime expirationDate, bool hasExpiration)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // 🔹 Step 1: Get selling price
                    decimal sellingPrice = 0;
                    string priceQuery = "SELECT selling_price FROM items WHERE item_id=@itemId";
                    using (var cmd = new MySqlCommand(priceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            sellingPrice = Convert.ToDecimal(result);
                        else
                            throw new Exception("Item not found.");
                    }

                    // 🔹 Step 2: Insert stock movement only
                    string movementQuery = @"INSERT INTO stock_movements 
                                     (item_id, movement_type, quantity, unit_price, expiration_date)
                                     VALUES (@itemId, @movementType, @quantity, @unit_price, @expiration_date)";
                    using (var cmd = new MySqlCommand(movementQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Parameters.AddWithValue("@movementType", movementType);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@unit_price", sellingPrice);
                        cmd.Parameters.AddWithValue("@expiration_date", hasExpiration ? (object)expirationDate : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // ✅ Remove manual quantity update
                    // The DB trigger will handle updating items.quantity

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in stock movement: " + ex.Message, "Stock Movement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        // ================================
        // 🔹 Get stock quantity
        // ================================
        public int GetStockQuantity(int itemId)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT quantity FROM items WHERE item_id=@itemId";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
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
                            // Parse discount percent
                            decimal discountPercent = 0;
                            decimal.TryParse(discountPercentText, out discountPercent);

                            // Insert invoice header
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

                            // Insert invoice items
                            foreach (DataRow row in items.Rows)
                            {
                                int itemId = Convert.ToInt32(row["item_id"]);
                                int qty = Convert.ToInt32(row["quantity"]);
                                decimal price = Convert.ToDecimal(row["unit_price"]);
                                decimal itemTotal = price * qty;

                                // Handle prescription_id if available
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

                                // Update stock only for items
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
        public bool AddWriteOff(int itemId, int quantity, string reason)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Step 1: Insert into write_off_movements
                            string writeOffQuery = @"
                        INSERT INTO write_off_movements
                        (item_id, quantity, reason, created_at, updated_at)
                        VALUES (@itemId, @quantity, @reason, NOW(), NOW())";

                            using (var cmd = new MySqlCommand(writeOffQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@itemId", itemId);
                                cmd.Parameters.AddWithValue("@quantity", quantity);
                                cmd.Parameters.AddWithValue("@reason", reason);
                                cmd.ExecuteNonQuery();
                            }

                            // Step 2: Insert into stock_movements
                            string stockMovementQuery = @"
                        INSERT INTO stock_movements
                        (item_id, movement_type, quantity, expiration_date)
                        VALUES (@itemId, 'WRITE-OFF', @quantity, NULL)";

                            using (var cmd = new MySqlCommand(stockMovementQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@itemId", itemId);
                                cmd.Parameters.AddWithValue("@quantity", quantity);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error recording write-off: " + ex.Message, "Write-Off Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Write-Off Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




    }
}
