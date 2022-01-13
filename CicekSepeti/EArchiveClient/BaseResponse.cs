using System;
using System.Net;

namespace EArchiveClient
{
    public class BaseResponse<T>
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

        public BaseResponse()
        {
            Data = (T)Activator.CreateInstance(typeof(T));
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
