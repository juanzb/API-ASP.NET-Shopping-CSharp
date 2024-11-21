using Models;

namespace Repository.Interfaces
{
    public interface IInvoiceDetailsRespository
    {
        List<InvoicesDetails> GetAll();
        void Create(IEnumerable<InvoicesDetails> model, int invoiceId);
        IEnumerable<InvoicesDetails> GetByInvoiceId(int invoiceId);
        void RemoveByInvoiceId(int invoiceId);
    }
}
