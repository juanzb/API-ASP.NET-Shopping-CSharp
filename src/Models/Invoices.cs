namespace Models
{
    public class Invoices
    {
        public int Id { get; set; }
        public int ClientID { get; set; }
        public Clients Client { get; set; }
        public List<InvoicesDetails> Detail { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        //metodo constructor para incializar las propiedades de la clase
        public Invoices()
        {
            Detail = new List<InvoicesDetails>();
        }
    }
}
