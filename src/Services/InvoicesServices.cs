using Models;
using MySql.Data.MySqlClient;
using Parameters;
using Repositories;

namespace Services
{
    public class InvoicesServices
    {
        public List<Invoices> AllInvoicesService()
        {
            var result = new List<Invoices>();
            try
            {
                List<Invoices> allInvoices = new InvoicesRepo().AllInvoicesRepo();
                
                foreach (var invoice in allInvoices)
                {
                    // Cliente
                    SetClient(invoice);

                    // Details y Products
                    SetDetails(invoice);   

                    result.Add(invoice);
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
                Invoices InvoiceGet = new InvoicesRepo().GetInvoiceRepo(id);
                // Client
                SetClient(InvoiceGet);
                // Details and Products
                SetDetails(InvoiceGet);

                result = InvoiceGet;
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
                new InvoicesRepo().CreateInvoiceRepo(invoice);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateInvoiceService: {ex.Message}");
                throw;
            }
        }

        public void UpdateInvoiceService(int invoiceID, Invoices invoice)
        {
            try
            {
                // preparando la data para insertar en base de datos
                PepareModal(invoice);

                // Se Actualiza la data de la compra en la base de datos
                new InvoicesRepo().UpdateInvoiceRepo(invoiceID ,invoice);
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
                // Se Actualiza la data de la compra en la base de datos
                new InvoicesRepo().DeleteInvoiceRepo(invoiceID);
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

        private void SetClient(Invoices invoice)
        {
            try { 
                Clients client = new ClientsRepo().GetClientRepo(invoice.ClientID);

                invoice.ClientID = client.Id;
                invoice.Client = new Clients
                {
                    Id = client.Id,
                    Name = client.Name
                };
            }   
            catch (Exception ex)
            {
                Console.WriteLine($"Error SetClient: {ex.Message}");
                throw;
            }
        }

        private void SetDetails(Invoices invoice)
        {
            try
            {
                List<InvoicesDetails> detailsGet = new InvoicesDetailsRepo().GetInvoicesDetailByInvoiceIDRepo(invoice.Id);

                foreach (var detail in detailsGet)
                {
                    detail.Invoice = invoice;

                    // Products
                    SetProduct(detail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SetDetail: {ex.Message}");
                throw;
            }
        }

        private void SetProduct(InvoicesDetails detail)
        {
            try
            {
                Products productGet = new ProductsRepo().GetProductsRepo(detail.ProductID);

                detail.Product = new Products
                {
                    Id = productGet.Id,
                    Name = productGet.Name,
                    Price = productGet.Price
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SetProduct: {ex.Message}");
                throw;
            }
        }
    }
}
