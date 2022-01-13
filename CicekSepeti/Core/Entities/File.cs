using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class File : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TermStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TermEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InvoiceCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalTax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalSubTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Invoice> Invoices { get; }

        /// <summary>
        /// 
        /// </summary>
        public File()
        {
            Invoices = new List<Invoice>();
        }
    }
}
