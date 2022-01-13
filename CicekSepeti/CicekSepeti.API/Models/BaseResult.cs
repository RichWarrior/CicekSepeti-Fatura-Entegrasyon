using System;
using System.Net;

namespace CicekSepeti.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BaseResult()
        {
            Data = (T)Activator.CreateInstance(typeof(T));
            Message = string.Empty;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
