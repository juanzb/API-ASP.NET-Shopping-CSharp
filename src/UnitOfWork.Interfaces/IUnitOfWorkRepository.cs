using Repository.Interfaces;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IInvoiceRepository InvoiceRepository { get; }
        IClientsRepository ClientsRepository{ get; }
        IInvoiceDetailsRespository InvoiceDetailsRespository { get; }
        IProductsRepository ProductsRepository { get; }

    }
}
