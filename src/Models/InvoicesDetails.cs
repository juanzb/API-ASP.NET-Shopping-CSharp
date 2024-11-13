﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InvoicesDetails
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public Products Product { get; set; }
        public int InvoiceID { get; set; }
        public Invoices Invoice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Iva { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}