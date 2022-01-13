using System.Collections.Generic;

namespace Core.Dtos.Request.Invoice
{
    public class ProcessInvoiceRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<int> Invoices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InvoiceStatusId { get; set; }
    }
}
