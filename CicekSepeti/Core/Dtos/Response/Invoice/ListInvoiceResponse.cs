using System;
using System.Collections.Generic;

namespace Core.Dtos.Response.Invoice
{
    public class ListInvoiceResponse
    {
        public List<Invoice> Invoices { get; set; }

        public sealed class Invoice
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string OrderId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SubOrderId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string InvoiceUniqueKey { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ProductSecondName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal Piece { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal Price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal Tax { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal TaxRate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal SubTotal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CustomerName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CustomerVKN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string TaxOffice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime InvoiceDate { get; set; }          
            /// <summary>
            /// 
            /// </summary>
            public string InvoiceStatus { get; set; }
        }

        public ListInvoiceResponse()
        {
            Invoices = new List<Invoice>();
        }
    }
}
