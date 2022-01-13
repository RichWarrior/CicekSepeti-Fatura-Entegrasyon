
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorDataResult<T> : DataResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public ErrorDataResult(T data) : base(data, false)
        {
        }
    }
}
