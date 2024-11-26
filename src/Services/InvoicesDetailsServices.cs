using Models;
using UnitOfWork.Interfaces;

namespace Services
{
    public class InvoicesDetailsServices
    {
        IUnitOfWork _unitOfWork;
        public InvoicesDetailsServices(IUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public List<InvoicesDetails> AllInvoiceDetailsService()
        {
            List<InvoicesDetails> details;
            try
            {
                using (IUnitOfWorkAdapter connect = _unitOfWork.Create())
                {
                    details = connect.Repositories.InvoiceDetailsRespository.GetAll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return details;
        }

        public void CreateInvoiceDetail(List<InvoicesDetails> details)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.InvoiceDetailsRespository.Create(details);
                    connect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inserting invoice detail: {ex.Message}");
                throw;
            }
        }

        public void RemoveInvoiceDetail(int invoiceID)
        {
            try
            {
                using (var connect = _unitOfWork.Create())
                {
                    connect.Repositories.InvoiceDetailsRespository.RemoveByInvoiceId(invoiceID);
                    connect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting invoice detail: {ex.Message}");
                throw;
            }
        }
    }
}
