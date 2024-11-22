using Models;
using Repository.Interfaces.Actions;

namespace Repository.Interfaces
{
    public interface IInvoiceRepository : IReadRepository<Invoices, int>, ICreateRepository<Invoices>, IUpdateRepository<Invoices>, IRemoveRepository<int>
    {
    }
}
