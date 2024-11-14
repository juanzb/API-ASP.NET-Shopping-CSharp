using Models;

namespace Repository.Interfaces
{
    internal interface InvoiceInterface
    {
        public void Create(Invoices invoice) { }
        public void Update(int id, Invoices invoice) { }
        public void Get(Invoices invoice) { }
        public void Delete(int id) { }
    }
}
