using Models;
using MySql.Data.MySqlClient;
using Parameters;
using Repository.Interfaces;

namespace Repository.MysqlServers
{
    public class ProductsRepository : Repository, IProductsRepository
    {
        public ProductsRepository(MySqlConnection connect, MySqlTransaction transaction)
        {
            this._connect = connect;
            this._transaction = transaction;
        }
        public List<Products> GetAll()
        {
            var result = new List<Products>();
            try
            {
                const string queryDb = "SELECT * FROM products";
                using (var command = CreateMySqlCommand(queryDb))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Products
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Price = reader.GetDecimal("price"),
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

        public Products GetById(int id)
        {
            var result = new Products();
            try
            {
                const string queryDB = "SELECT * FROM products WHERE id = @id";
                var command = CreateMySqlCommand(queryDB);
                command.Parameters.AddWithValue("id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Products
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price")
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

        public void Create(Products product)
        {
            try
            {
                const string queryDB = "INSERT INTO products (name, price) VALUES (@A,@B)";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@A", product.Name);
                    command.Parameters.AddWithValue("@B", product.Price);
                    command.ExecuteNonQuery();
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

        public void Update(int productID, Products product)
        {
            try
            {
                const string queryDB = "UPDATE products SET name=@A, price=@B WHERE id=@productID";
                using (MySqlCommand command = CreateMySqlCommand(queryDB))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@A", product.Name);
                    command.Parameters.AddWithValue("@B", product.Price);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("No existe el ID del cliente para actualizar el nombre");
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

        public void Remove(int productID)
        {
            try
            {
                const string queryDb = "DELETE FROM products WHERE id=@productID";
                using (MySqlCommand command = CreateMySqlCommand(queryDb))
                {
                    command.Parameters.AddWithValue("@productID", productID);
                    var res = command.ExecuteNonQuery();
                    if (res < 1) throw new ArgumentException("No existe el ID del cliente para actualizar el nombre");
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
