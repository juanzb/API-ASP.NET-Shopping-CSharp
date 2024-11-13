using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Invoices
    {
        public List<InvoicesDetails> Detail { get; set; }
        public int Id { get; set; }
        public int ClientID { get; set; }
        public Clients Client { get; set; }
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
