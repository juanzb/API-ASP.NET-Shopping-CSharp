using MySql.Data.MySqlClient;
using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServerAdapter : IUnitOfWorkAdapter
    {
        private MySqlConnection _connect { get; set; }
        private MySqlTransaction _transaction { get; set; }
        public IUnitOfWorkRepository Repositories { get; set; }

        public UnidOfWorkMySqlServerAdapter(string connectionString)
        {
            _connect = new MySqlConnection(connectionString);
            _connect.Open();

            _transaction = _connect.BeginTransaction();

            Repositories = new UnidOfWorkMySqlServerRepository(_connect, _transaction);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            else if (_connect != null)
            {
                _connect.Close();
                _connect.Dispose();
            }

            Repositories = null;
        }

        public void SaveChanges()
        {
            _transaction.Commit();
        }

    }
}
