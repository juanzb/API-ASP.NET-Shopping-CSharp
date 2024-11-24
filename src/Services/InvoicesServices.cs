using Models;
using Parameters;
using UnitOfWork.Interfaces;

namespace Services
{
    public class InvoicesServices
    {
        private IUnitOfWork _unitOfWOrk;

        public InvoicesServices(IUnitOfWork UnitOfWork)
        {
            this._unitOfWOrk = UnitOfWork;
        }

        public List<Invoices> AllInvoicesService()
        {
            var result = new List<Invoices>();
            try
            {
                using (var connect = _unitOfWOrk.Create())
                {
                    List<Invoices> allInvoices = connect.Repositories.InvoiceRepository.GetAll();

                    foreach (var invoice in allInvoices)
                    {
                        invoice.Client = connect.Repositories.ClientsRepository.GetById(invoice.ClientID);
                        invoice.Detail = (List<InvoicesDetails>)connect.Repositories.InvoiceDetailsRespository.GetByInvoiceId(invoice.Id);

                        foreach (var item in invoice.Detail)
                        {
                            item.Product = connect.Repositories.ProductsRepository.GetById(item.ProductID);
                        }

                        result.Add(invoice);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AllInvoicesService: {ex.Message}");
                throw;
            }
            return result;
        }

        public Invoices GetInvoiceService(int id)
        {
            var result = new Invoices();
            try
            {
                using (var connect = _unitOfWOrk.Create())
                {
                    result = connect.Repositories.InvoiceRepository.GetById(id);
                    result.Client = connect.Repositories.ClientsRepository.GetById(result.ClientID);
                    result.Detail = (List<InvoicesDetails>)connect.Repositories.InvoiceDetailsRespository.GetByInvoiceId(result.Id);

                    foreach (var item in result.Detail)
                    {
                        item.Product = connect.Repositories.ProductsRepository.GetById(item.ProductID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetInvoiceService: {ex.Message}");
                throw;
            }
            return result;
        }

        public void CreateInvoiceService(Invoices invoice)
        {
            try
            {
                // preparando la data para insertar en base de datos
                PepareModal(invoice);
                
                // Se Agrega la data de la compra en la base de datos
                using (var connect = _unitOfWOrk.Create())
                {
                    connect.Repositories.InvoiceRepository.Create(invoice);
                    connect.Repositories.InvoiceDetailsRespository.Create(invoice.Detail, invoice.Id);

                    connect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateInvoiceService: {ex.Message}");
                throw;
            }
        }

        public void UpdateInvoiceService(Invoices invoice, int invoiceId)
        {
            try
            {
                // preparando la data para insertar en base de datos
                PepareModal(invoice);

                // Se Actualiza la data de la compra en la base de datos
                using (var connect = _unitOfWOrk.Create())
                {
                    connect.Repositories.InvoiceRepository.Update(invoice, invoiceId);
                    connect.Repositories.InvoiceDetailsRespository.RemoveByInvoiceId(invoiceId);
                    connect.Repositories.InvoiceDetailsRespository.Create(invoice.Detail, invoiceId);

                    connect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateInvoiceService: {ex.Message}");
                throw;
            }
        }

        public void DeleteInvoiceService(int invoiceID)
        {
            try
            {
                using (var connect = _unitOfWOrk.Create())
                {
                    connect.Repositories.InvoiceRepository.Remove(invoiceID);
                    connect.Repositories.InvoiceDetailsRespository.RemoveByInvoiceId(invoiceID);
                    connect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteInvoiceService: {ex.Message}");
                throw;
            }
        }

        private void PepareModal(Invoices model)
        {
            foreach (var detail in model.Detail)
            {
                detail.Total = detail.Price * detail.Quantity;
                detail.Iva = detail.Total * ParametersIVA.iva;
                detail.SubTotal = detail.Total + detail.Iva;
            }
            model.Total = model.Detail.Sum(x => x.Total);
            model.Iva = model.Detail.Sum(x => x.Iva);
            model.SubTotal = model.Detail.Sum(x => x.SubTotal);
        }

    }
}
