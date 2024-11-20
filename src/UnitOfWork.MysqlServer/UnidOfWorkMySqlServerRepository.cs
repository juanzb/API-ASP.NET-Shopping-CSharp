using MySql.Data.MySqlClient;
using Repository.Interfaces;
using Repository.MysqlServers;
using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServerRepository : IUnitOfWorkRepository
    {
        public IInvoiceRepository InvoiceRepository { get; }

        public UnidOfWorkMySqlServerRepository (MySqlConnection connect, MySqlTransaction transaction)
        {
            InvoiceRepository = new InvoicesRepository (connect, transaction);
        }
    }
}
