using Models;
using MySql.Data.MySqlClient;
using Parameters;

namespace Repositories
{
    public class ProductsRepo
    {
        public List<Products> AllProductsRepo()
        {
            var result = new List<Products>();
            try
            {
                using (var connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDb = "SELECT * FROM products";
                    using (var command = new MySqlCommand(queryDb, connect))
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

        public Products GetProductsRepo(int id)
        {
            var result = new Products();
            try
            {
                using (var connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "SELECT * FROM products WHERE id = @id";
                    var command = new MySqlCommand(queryDB, connect);
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

        public void CreateProductsRepo(Products product)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "INSERT INTO products (name, price) VALUES (@A,@B)";
                    using (MySqlCommand command = new MySqlCommand(queryDB, connect))
                    {
                        command.Parameters.AddWithValue("@A", product.Name);
                        command.Parameters.AddWithValue("@B", product.Price);
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

        public void UpdateProductsRepo(int productID, Products product)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDB = "UPDATE products SET name=@A, price=@B WHERE id=@productID";
                    using (MySqlCommand command = new MySqlCommand(queryDB, connect))
                    {
                        command.Parameters.AddWithValue("@productID", productID);
                        command.Parameters.AddWithValue("@A", product.Name);
                        command.Parameters.AddWithValue("@B", product.Price);
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

        public void DeleteClientRepo(int productID)
        {
            try
            {
                using (MySqlConnection connect = new MySqlConnection(ParametersDB.ShopDB))
                {
                    connect.Open();
                    const string queryDb = "DELETE FROM products WHERE id=@productID";
                    using (MySqlCommand command = new MySqlCommand(queryDb, connect))
                    {
                        command.Parameters.AddWithValue("@productID", productID);
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
