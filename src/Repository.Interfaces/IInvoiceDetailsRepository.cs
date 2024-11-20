using Models;

namespace Repository.Interfaces
{
    public interface IInvoiceDetailsInterface
    {
        List<InvoicesDetails> GetAll();
        void Create(IEnumerable<InvoicesDetails> model, int invoiceId);
        IEnumerable<InvoicesDetails> GetAllByInvoiceId(int invoiceId);
        void RemoveByInvoiceId(int invoiceId);
    }
}
