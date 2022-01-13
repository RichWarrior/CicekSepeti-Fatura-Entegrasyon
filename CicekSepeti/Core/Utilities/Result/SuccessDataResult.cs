using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessDataResult<T> : DataResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }
    }
}
