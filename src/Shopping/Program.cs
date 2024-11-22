using Models;
using Services;
using UnitOfWork.MysqlServer;

namespace Shopping
{
    internal class Program
    {
        static void Main()
        {
            var unitofwork = new UnidOfWorkMySqlServer();
            var serviceInvoice = new InvoicesServices(unitofwork);
            //var serviceInvoiceDetails = new InvoicesDetailsServices(unitofwork);
            //var serviceClient = new ClientsServices(unitofwork);
            //var serviceProducts = new ProductsServices(unitofwork);

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
                    //new InvoicesDetails
                    //{
                    //    ProductID = 2,
                    //    Quantity = 10,
                    //    Price = 3000,
                    //},
                    //new InvoicesDetails
                    //{
                    //    ProductID = 4,
                    //    Quantity = 10,
                    //    Price = 4000,
                    //},
                },
            };

            try
            {
                List<Invoices> datos = serviceInvoice.AllInvoicesService();
                //Invoices data = serviceInvoice.GetInvoiceService(4);
                //serviceInvoice.CreateInvoiceService(newInvoice);
                //serviceInvoice.UpdateInvoiceService(newInvoice, 10);
                //serviceInvoice.DeleteInvoiceService(10);

                Console.WriteLine("Compra exitosa, que tenga un buen día!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lo sentimos, error de compra, intente de nuevo!");
                Console.Write(ex);
            }

            Console.Read();
        }
    }
}
