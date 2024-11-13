namespace Clients
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ServiceInvoice = new InvoiceService();
            var all = ServiceInvoice.GetAll();
            var invoice = ServiceInvoice.Get(5);
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
                        Quantity = 6,
                        Price = 2000,
                    },
                },
            };

            try
            {
                //ServiceInvoice.CreateInvoice(newInvoice);
                //ServiceInvoice.UpdateInvoice(5216,newInvoice);
                //ServiceInvoice.DeleteInvoice(60);
                //Console.WriteLine("Compra exitosa, que tenga un buen día!");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Lo sentimos, error de compra, intente de nuevo!");
                Console.Write(ex.Message);
            }

            //var clients = ServiceInvoice.Get(9);
            //var InvoiceDetails = ServiceInvoice.Get(9);
            Console.Read();
        }
    }
}
