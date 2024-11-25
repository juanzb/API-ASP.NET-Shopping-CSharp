using Models;
using Repository.Interfaces.Actions;

namespace Repository.Interfaces
{
    public interface IInvoiceDetailsRespository :
        IReadRepository<InvoicesDetails, int>,
        ICreateRepository<List<InvoicesDetails>>
    //IUpdateRepository<InvoicesDetails>, 
    //IRemoveRepository<int>
    {
        List<InvoicesDetails> GetByInvoiceId(int id);

        void RemoveByInvoiceId(int ID);
    }
}
