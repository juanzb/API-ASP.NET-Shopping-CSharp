using Models;
using MySql.Data.MySqlClient;
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
                    var allInvoices = connect.Repositories.InvoiceRepository.GetAll();

                    foreach (var invoice in allInvoices)
                    {
                        invoice.Client = connect.Repositories.ClientsRepository.GetById(invoice.ClientID);
                        invoice.Detail = (List<InvoicesDetails>)connect.Repositories.InvoiceDetailsRespository.GetByInvoiceId(invoice.Id);

                        foreach (var item in invoice.Detail)
                        {
                            item.Product = connect.Repositories.ProductsRepository.GetById(item.ProductID);
                            item.Invoice = invoice;
                        }

                        result.Add(invoice);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
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
                        item.Invoice = result;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error DB: {ex.Message}");
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Argument: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inesperado: {ex.Message}");
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateInvoiceService: {ex.Message}");
                throw;
            }
        }

        public void UpdateInvoiceService(Invoices invoice)
        {
            try
            {
                // preparando la data para insertar en base de datos
                PepareModal(invoice);

                // Se Actualiza la data de la compra en la base de datos
                using (var connect = _unitOfWOrk.Create())
                {
                    connect.Repositories.InvoiceRepository.Update(invoice);
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
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error Argument: {ex.Message}");
                throw;
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

        //        private void SetClient(Invoices invoice)
        //        {
        //            try
        //            {
        //                Clients client = new ClientsRepository().GetClientRepo(invoice.ClientID);

        //                invoice.ClientID = client.Id;
        //                invoice.Client = new Clients
        //                {
        //                    Id = client.Id,
        //                    Name = client.Name
        //                };
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error SetClient: {ex.Message}");
        //                throw;
        //            }
        //        }

        //        private void SetDetails(Invoices invoice)
        //        {
        //            try
        //            {
        //                List<InvoicesDetails> detailsGet = new InvoicesDetailsRepo().GetInvoicesDetailByInvoiceIDRepo(invoice.Id);

        //                foreach (var detail in detailsGet)
        //                {
        //                    detail.Invoice = invoice;

        //                    // Products
        //                    SetProduct(detail);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error SetDetail: {ex.Message}");
        //                throw;
        //            }
        //        }

        //        private void SetProduct(InvoicesDetails detail)
        //        {
        //            try
        //            {
        //                Products productGet = new ProductsRepository().GetProductsRepo(detail.ProductID);

        //                detail.Product = new Products
        //                {
        //                    Id = productGet.Id,
        //                    Name = productGet.Name,
        //                    Price = productGet.Price
        //                };
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error SetProduct: {ex.Message}");
        //                throw;
        //            }
        //        }
        //    }
    }
}
