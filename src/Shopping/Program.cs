using Models;
//using Repositories;
using Services;
using UnitOfWork.Interfaces;
using UnitOfWork.MysqlServer;

namespace Shopping
{
    internal class Program
    {
        static void Main()
        {
            var unitofwork = new UnidOfWorkMySqlServer();
            var serviceInvoice = new InvoicesServices(unitofwork);
            //var se = new ClientsServices();
            //var p = new ProductsRepository();
            //var ServiceInvoice = new InvoicesServices();
            //var all = ServiceInvoice.AllInvoicesService();
            //var invoice = ServiceInvoice.GetInvoiceService(5);
            var newInvoice = new Invoices
            {
                ClientID = 4,
                Detail = new List<InvoicesDetails> {
                    new InvoicesDetails
                    {
                        ProductID = 1,
                        Quantity = 3,
                        Price = 1000,
                    },
                    new InvoicesDetails
                    {
                        ProductID = 5,
                        Quantity = 10,
                        Price = 5000,
                    },
                    new InvoicesDetails
                    {
                        ProductID = 2,
                        Quantity = 10,
                        Price = 3000,
                    },
                    new InvoicesDetails
                    {
                        ProductID = 4,
                        Quantity = 10,
                        Price = 4000,
                    },
                },
            };

            try
            {
                //serviceInvoice.CreateInvoiceService(newInvoice);
                serviceInvoice.DeleteInvoiceService(64);
                Invoices data = serviceInvoice.GetInvoiceService(64);

                //var pp = p.AllProductsRepo();
                //foreach (var item in pp)
                //{
                //    Console.WriteLine(item.Name);
                //}
                //var invoice = ServiceInvoice.GetInvoiceService(62);
                //Console.WriteLine(invoice.Iva);
                //List<Invoices> allInvoice = ServiceInvoice.AllInvoicesService();
                //foreach (var item in allInvoice)
                //{
                //    Console.WriteLine(item.Client.Name);                
                //}
                //ServiceInvoice.CreateInvoiceService(newInvoice);
                //ServiceInvoice.UpdateInvoiceService(61, newInvoice);
                //ServiceInvoice.DeleteInvoiceService(54);
                Console.WriteLine("Compra exitosa, que tenga un buen día!");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Lo sentimos, error de compra, intente de nuevo!");
                Console.Write(ex);
            }

            //var clients = ServiceInvoice.Get(9);
            //var InvoiceDetails = ServiceInvoice.Get(9);
            Console.Read();
        }
    }
}
