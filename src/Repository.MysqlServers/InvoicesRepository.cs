using Models;
using MySql.Data.MySqlClient;
using Repository.Interfaces;

namespace Repository.MysqlServers
{
    public class InvoicesRepository : Repository, IInvoiceRepository
    {
        public InvoicesRepository(MySqlConnection connect, MySqlTransaction transaction)
        {
            this._connect = connect;
            this._transaction = transaction;
        }

        public List<Invoices> GetAll()
        {
            var result = new List<Invoices>();
            try
            {
                const string queryDb = "SELECT * FROM invoices";
                using (var command = CreateMySqlCommand(queryDb))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Invoices
                            {
                                Id = reader.GetInt32("id"),
                                Iva = reader.GetDecimal("iva"),
                                SubTotal = reader.GetDecimal("Subtotal"),
                                Total = reader.GetDecimal("total"),
                                ClientID = reader.GetInt32("clientID"),
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
            return result;
        }

        public Invoices GetById(int id)
        {
            var result = new Invoices();
            try
            {
                const string queryDB = "SELECT * FROM invoices WHERE id = @id";
                var command = CreateMySqlCommand(queryDB);
                command.Parameters.AddWithValue("id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Invoices
                        {
                            Id = reader.GetInt32("id"),
                            ClientID = reader.GetInt32("clientID"),
                            Iva = reader.GetDecimal("iva"),
                            SubTotal = reader.GetDecimal("subtotal"),
                            Total = reader.GetDecimal("total"),
                        };
                    }
                    else
                    {
                        throw new ArgumentException("El ID no se encuentra registrado");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error de Argumento: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
            return result;
        }

        public void Create(Invoices invoice)
        {
            try
            {
                const string queryDB = "INSERT INTO invoices (clientID, iva, subtotal, total) VALUES (@A,@B,@C,@D)";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@A", invoice.ClientID);
                    command.Parameters.AddWithValue("@B", invoice.Iva);
                    command.Parameters.AddWithValue("@C", invoice.SubTotal);
                    command.Parameters.AddWithValue("@D", invoice.Total);
                    command.ExecuteNonQuery();

                    invoice.Id = Convert.ToInt32(command.LastInsertedId);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
        }

        public void Update(Invoices invoice)
        {
            try
            {
                const string queryDB = "UPDATE invoices SET clientID=@A, iva=@B, subtotal=@C, total=@D WHERE id=@invoiceID";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@invoiceID", invoice.Id);
                    command.Parameters.AddWithValue("@A", invoice.ClientID);
                    command.Parameters.AddWithValue("@B", invoice.Iva);
                    command.Parameters.AddWithValue("@C", invoice.SubTotal);
                    command.Parameters.AddWithValue("@D", invoice.Total);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("El ID no se encuentra registrado");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                const string queryDb = "DELETE FROM invoices WHERE id=@invoiceID";
                using (MySqlCommand command = CreateMySqlCommand(queryDb))
                {
                    command.Parameters.AddWithValue("@invoiceID", id);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("El ID no se encuentra registrado");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
        }
    }
}
