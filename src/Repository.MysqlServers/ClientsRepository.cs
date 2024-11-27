using Models;
using MySql.Data.MySqlClient;
using Repository.Interfaces;

namespace Repository.MysqlServers
{
    public class ClientsRepository : Repository, IClientsRepository
    {
        public ClientsRepository(MySqlConnection connect, MySqlTransaction transaction) 
        { 
            this._connect = connect;
            this._transaction = transaction;
        }

        public List<Clients> GetAll()
        {
            var result = new List<Clients>();
            try
            {
                const string queryDb = "SELECT * FROM clients";
                using (var command = CreateMySqlCommand(queryDb))
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

        public Clients GetById(int id)
        {
            var result = new Clients();
            try
            {
                const string queryDB = "SELECT * FROM clients WHERE id = @id";
                var command = CreateMySqlCommand(queryDB);
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

        public void Create(Clients client)
        {
            try
            {
                const string queryDB = "INSERT INTO clients (name) VALUES (@A)";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@A", client.Name);
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

        public void Update(Clients client)
        {
            try
            {
                const string queryDB = "UPDATE clients SET name=@A WHERE id=@clientId";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@clientId", client.Id);
                    command.Parameters.AddWithValue("@A", client.Name);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("El ID no se encuentra registrado");
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
        }

        public void Remove(int clientID)
        {
            try
            {
                const string queryDb = "DELETE FROM clients WHERE id=@clientsID";
                using (MySqlCommand command = CreateMySqlCommand(queryDb))
                {
                    command.Parameters.AddWithValue("@clientsID", clientID);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("El ID no se encuentra registrado");
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
        }

    }
}
