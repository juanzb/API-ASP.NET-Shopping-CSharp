using Models;
using MySql.Data.MySqlClient;
using Repository.Interfaces;

namespace Repository.MysqlServers
{
    public class InvoicesDetailsRepository : Repository, IInvoiceDetailsRespository
    {
        public InvoicesDetailsRepository(MySqlConnection connect, MySqlTransaction transaction) 
        { 
            this._connect = connect;
            this._transaction = transaction;
        }
        public List<InvoicesDetails> GetAll()
        {
            var result = new List<InvoicesDetails>();
            try
            {
                const string queryDb = "SELECT * FROM invoicesDetails";
                using (var command = CreateMySqlCommand(queryDb))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new InvoicesDetails
                            {  
                                Id = reader.GetInt32("id"),
                                InvoiceID = reader.GetInt32("invoiceID"),
                                ProductID = reader.GetInt32("productID"),
                                Quantity = reader.GetInt32("quantity"),
                                Price = reader.GetDecimal("price"),
                                Iva = reader.GetDecimal("iva"),
                                SubTotal = reader.GetDecimal("Subtotal"),
                                Total = reader.GetDecimal("total"),
                            });
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public InvoicesDetails GetById(int id)
        {
            var result = new InvoicesDetails();
            try
            {
                const string queryDB = "SELECT * FROM invoicesDetails WHERE id = @id";
                var command = CreateMySqlCommand(queryDB);
                command.Parameters.AddWithValue("id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new InvoicesDetails
                        {
                            Id = reader.GetInt32("id"),
                            InvoiceID = reader.GetInt32("invoiceID"),
                            ProductID = reader.GetInt32("productID"),
                            Quantity = reader.GetInt32("quantity"),
                            Price = reader.GetDecimal("price"),
                            Iva = reader.GetDecimal("iva"),
                            SubTotal = reader.GetDecimal("Subtotal"),
                            Total = reader.GetDecimal("total"),
                        };
                    }
                    else
                    {
                        throw new ArgumentException("El ID no se encuentra registrado");
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<InvoicesDetails> GetByInvoiceId(int invoiceID)
        {
            var result = new List<InvoicesDetails>();
            try
            {
                string queryDb = "SELECT * FROM invoicesdetails WHERE invoiceID = @invoiceID";
                var commandMysql = CreateMySqlCommand(queryDb);
                commandMysql.Parameters.AddWithValue("@invoiceID", invoiceID);

                using (var reader = commandMysql.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new InvoicesDetails
                        {
                            Id = reader.GetInt32("id"),
                            InvoiceID = reader.GetInt32("invoiceID"),
                            ProductID = reader.GetInt32("productID"),
                            Quantity = reader.GetInt32("quantity"),
                            Price = reader.GetDecimal("price"),
                            Iva = reader.GetDecimal("iva"),
                            SubTotal = reader.GetDecimal("Subtotal"),
                            Total = reader.GetDecimal("total"),
                        });
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void Create(List<InvoicesDetails> details)
        {
            try
            {
                const string queryDB = "INSERT INTO invoicesdetails " +
                    "(invoiceID, productID, quantity, price, iva, subtotal, total) " +
                    "VALUES (@A,@B,@C,@D,@E,@F,@G)";
                foreach (var detail in details)
                {
                    using (var command = CreateMySqlCommand(queryDB))
                    {
                        command.Parameters.AddWithValue("@A", detail.InvoiceID);
                        command.Parameters.AddWithValue("@B", detail.ProductID);
                        command.Parameters.AddWithValue("@C", detail.Quantity);
                        command.Parameters.AddWithValue("@D", detail.Price);
                        command.Parameters.AddWithValue("@E", detail.Iva);
                        command.Parameters.AddWithValue("@F", detail.SubTotal);
                        command.Parameters.AddWithValue("@G", detail.Total);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveByInvoiceId(int invoiceID)
        {
            try
            {
                const string queryDB = "Delete FROM invoicesdetails WHERE invoiceID=@invoiceID";
                using (var command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@invoiceID", invoiceID);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
