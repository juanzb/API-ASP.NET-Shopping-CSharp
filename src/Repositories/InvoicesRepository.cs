using Models;
using MySql.Data.MySqlClient;
using Parameters;
using Repository.Interfaces;
using System.Transactions;

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
                        throw new ArgumentException("No se encuentras datos en la base de datos");
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

                InvoicesDetailsRepository invoiceDetailsRepo = new InvoicesDetailsRepository();
                // Insertar data de la compra en la tabal "invoicedetails"
                invoiceDetailsRepo.InsertInvoiceDetailRepo(invoice.Id, invoice.Detail, _connect, _transaction);

                // Fin de la Transacción
                transaction.Commit();
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
            throw new NotImplementedException();
        }

        public void Update(Invoices t)
        {
            throw new NotImplementedException();
        }


        //public List<Invoices> AllInvoicesRepo()
        //{
        //    var result = new List<Invoices>();
        //    try
        //    {
        //        using (var connect = new MySqlConnection(ParametersDB.ShopDB))
        //        {
        //            connect.Open();
        //            const string queryDb = "SELECT * FROM invoices";
        //            using (var command = new MySqlCommand(queryDb, connect))
        //            {
        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.Add(new Invoices
        //                        {
        //                            Id = reader.GetInt32("id"),
        //                            Iva = reader.GetDecimal("iva"),
        //                            SubTotal = reader.GetDecimal("Subtotal"),
        //                            Total = reader.GetDecimal("total"),
        //                            ClientID = reader.GetInt32("clientID"),
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine($"Error DB: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inesperado: {ex.Message}");
        //        throw;
        //    }
        //    return result;
        //}

        //public Invoices GetInvoiceByIdRepo(int id)
        //{
        //    var result = new Invoices();
        //    try
        //    {
        //        using (var connect = new MySqlConnection(ParametersDB.ShopDB))
        //        {
        //            connect.Open();
        //            const string queryDB = "SELECT * FROM invoices WHERE id = @id";
        //            var command = new MySqlCommand(queryDB, connect);
        //            command.Parameters.AddWithValue("id", id);

        //            using (var reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    result = new Invoices
        //                    {
        //                        Id = reader.GetInt32("id"),
        //                        ClientID = reader.GetInt32("clientID"),
        //                        Iva = reader.GetDecimal("iva"),
        //                        SubTotal = reader.GetDecimal("subtotal"),
        //                        Total = reader.GetDecimal("total"),
        //                    };
        //                }
        //                else
        //                {
        //                    throw new ArgumentException("No se encuentras datos en la base de datos");
        //                }
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine($"Error DB: {ex.Message}");
        //        throw;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Console.WriteLine($"Error de Argumento: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inesperado: {ex.Message}");
        //        throw;
        //    }
        //    return result;
        //}

        //public void CreateInvoiceRepo(Invoices invoice)
        //{
        //    try
        //    {
        //        using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
        //        {
        //            connect.Open();
        //            using (var transaction = connect.BeginTransaction())
        //            {
        //                const string queryDB = "INSERT INTO invoices (clientID, iva, subtotal, total) VALUES (@A,@B,@C,@D)";
        //                using (MySqlCommand command = new MySqlCommand(queryDB, connect, transaction))
        //                {
        //                    command.Parameters.AddWithValue("@A", invoice.ClientID);
        //                    command.Parameters.AddWithValue("@B", invoice.Iva);
        //                    command.Parameters.AddWithValue("@C", invoice.SubTotal);
        //                    command.Parameters.AddWithValue("@D", invoice.Total);
        //                    command.ExecuteNonQuery();

        //                    invoice.Id = Convert.ToInt32(command.LastInsertedId);
        //                }

        //                InvoicesDetailsRepo invoiceDetailsRepo = new InvoicesDetailsRepo();
        //                // Insertar data de la compra en la tabal "invoicedetails"
        //                invoiceDetailsRepo.InsertInvoiceDetailRepo(invoice.Id, invoice.Detail, connect, transaction);

        //                // Fin de la Transacción
        //                transaction.Commit();
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine($"Error DB: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inesperado: {ex.Message}");
        //        throw;
        //    }
        //}

        //public void UpdateInvoiceRepo(int invoiceID, Invoices invoice)
        //{
        //    try
        //    {
        //        using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
        //        {
        //            connect.Open();
        //            using (var transaction = connect.BeginTransaction())
        //            {
        //                const string queryDB = "UPDATE invoices SET clientID=@A, iva=@B, subtotal=@C, total=@D WHERE id=@invoiceID";
        //                using (MySqlCommand command = new MySqlCommand(queryDB, connect, transaction))
        //                {
        //                    command.Parameters.AddWithValue("@invoiceID", invoiceID);
        //                    command.Parameters.AddWithValue("@A", invoice.ClientID);
        //                    command.Parameters.AddWithValue("@B", invoice.Iva);
        //                    command.Parameters.AddWithValue("@C", invoice.SubTotal);
        //                    command.Parameters.AddWithValue("@D", invoice.Total);
        //                    var res = command.ExecuteNonQuery();
        //                    if (res < 1) throw new ArgumentException("No se encuentra en la base de datos");
        //                }

        //                InvoicesDetailsRepo invoiceDetailsRepo = new InvoicesDetailsRepo();
        //                // Eliminar data de la compra "invoicedetails"
        //                invoiceDetailsRepo.DeleteInvoiceDetailRepo(invoiceID, connect, transaction);

        //                // Insertar data de la compra en la tabal "invoicedetails"
        //                invoiceDetailsRepo.InsertInvoiceDetailRepo(invoiceID, invoice.Detail, connect, transaction);

        //                //Fin de la Transacción
        //                transaction.Commit();
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine($"Error DB: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inesperado: {ex.Message}");
        //        throw;
        //    }
        //}

        //public void DeleteInvoiceRepo(int invoiceID)
        //{
        //    try
        //    {
        //        using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
        //        {
        //            connect.Open();
        //            using (MySqlTransaction transaction = connect.BeginTransaction())
        //            {
        //                const string queryDb = "DELETE FROM invoices WHERE id=@invoiceID";
        //                using (MySqlCommand command = new MySqlCommand(queryDb, connect, transaction))
        //                {
        //                    command.Parameters.AddWithValue("@invoiceID", invoiceID);
        //                    var res = command.ExecuteNonQuery();
        //                    if (res < 1) throw new ArgumentException("No se encuentra en la base de datos");

        //                    InvoicesDetailsRepo invoiceDetailRepo = new InvoicesDetailsRepo();
        //                    invoiceDetailRepo.DeleteInvoiceDetailRepo(invoiceID, connect, transaction);
        //                }
        //                transaction.Commit();
        //            }
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine($"Error DB: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error Inesperado: {ex.Message}");
        //        throw;
        //    }
        //}

    }
}
