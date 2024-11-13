using Models;
using MySql.Data.MySqlClient;
using Parameters;

namespace Repositories
{
    public class ClientsRepo
    {
        public List<Clients> AllClientsRepo()
        {
            var result = new List<Clients>();
            try
            {
                using (var connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDb = "SELECT * FROM clients";
                    using (var command = new MySqlCommand(queryDb, connect))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Clients
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader.GetString("name")
                                });
                            }
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

        public Clients GetClientRepo(int id)
        {
            var result = new Clients();
            try
            {
                using (var connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "SELECT * FROM clients WHERE id = @id";
                    var command = new MySqlCommand(queryDB, connect);
                    command.Parameters.AddWithValue("id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Clients
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name")
                            };
                        }
                        else
                        {
                            throw new ArgumentException("No se encuentras datos en la base de datos");
                        }
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

        public void CreateClientRepo(Clients client)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "INSERT INTO clients (name) VALUES (@A)";
                    using (MySqlCommand command = new MySqlCommand(queryDB, connect))
                    {
                        command.Parameters.AddWithValue("@A", client.Name);
                        command.ExecuteNonQuery();
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
        }

        public void UpdateClientRepo(int clientID, Clients client)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "UPDATE invoices SET clientID=@A, iva=@B, subtotal=@C, total=@D WHERE id=@clientID";
                    using (MySqlCommand command = new MySqlCommand(queryDB, connect))
                    {
                        command.Parameters.AddWithValue("@clientID", clientID);
                        command.Parameters.AddWithValue("@A", client.Name);
                        var res = command.ExecuteNonQuery();
                        if (res < 1) throw new ArgumentException("No existe el ID del cliente para actualizar el nombre");
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
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
                throw;
            }
        }

        public void DeleteClientRepo(int clientID)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDb = "DELETE FROM clients WHERE id=@clientsID";
                    using (MySqlCommand command = new MySqlCommand(queryDb, connect))
                    {
                        command.Parameters.AddWithValue("@invoiceID", clientID);
                        var res = command.ExecuteNonQuery();
                        if (res < 1) throw new ArgumentException("No existe el ID del cliente para actualizar el nombre");
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
                Console.WriteLine($"Error Inesperado: {ex.Message}");
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
