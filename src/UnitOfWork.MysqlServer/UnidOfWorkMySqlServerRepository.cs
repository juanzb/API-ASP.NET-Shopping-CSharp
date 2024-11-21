using MySql.Data.MySqlClient;
using Repository.Interfaces;
using Repository.MysqlServers;
using UnitOfWork.Interfaces;

namespace UnitOfWork.MysqlServer
{
    public class UnidOfWorkMySqlServerRepository : IUnitOfWorkRepository
    {
        public IInvoiceRepository InvoiceRepository { get; }
        public IClientsRepository ClientsRepository { get; }
        public IInvoiceDetailsRespository InvoiceDetailsRespository { get; }
        public IProductsRepository ProductsRepository { get; }

        public UnidOfWorkMySqlServerRepository (MySqlConnection connect, MySqlTransaction transaction)
        {
            InvoiceRepository = new InvoicesRepository (connect, transaction);
            ClientsRepository = new ClientsRepository (connect, transaction);
            InvoiceDetailsRespository = new InvoicesDetailsRepository (connect, transaction);
            ProductsRepository = new ProductsRepository(connect, transaction);
        }
    }
}
