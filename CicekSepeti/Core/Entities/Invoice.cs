using System;

namespace Core.Entities
{
    public class Invoice : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public File File { get; set; }
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
        public int Piece { get; set; }
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
        public int InvoiceStatusId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvoiceStatus InvoiceStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
}
