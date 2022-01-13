using System;
using System.Collections.Generic;

namespace Core.Dtos.Response.File
{
    public class ListFileResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<File> Files { get; set; }

        public sealed class File
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
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
            public DateTime CreationDate{ get; set; }
        }

        public ListFileResponse()
        {
            Files = new List<File>();
        }
    }
}
