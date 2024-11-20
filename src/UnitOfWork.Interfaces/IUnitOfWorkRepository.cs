using Repository.Interfaces;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IInvoiceRepository InvoiceRepository { get; }

    }
}
