using MySql.Data.MySqlClient;

namespace Repository.MysqlServers
{
    public abstract class Repository
    {
        protected MySqlConnection _connect;
        protected MySqlTransaction _transaction;
        protected MySqlCommand CreateMySqlCommand(string query)
        {
            return new MySqlCommand(query, _connect, _transaction);
        }
    
    }
}
