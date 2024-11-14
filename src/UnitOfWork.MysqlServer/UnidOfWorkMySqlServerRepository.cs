using MySql.Data.MySqlClient;
using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServerRepository : IUnitOfWorkRepository
    {
        public UnidOfWorkMySqlServerRepository (MySqlConnection connect, MySqlTransaction transaction)
        {

        }
    }
}
