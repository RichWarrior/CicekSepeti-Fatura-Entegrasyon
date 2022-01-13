
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResult<T> : Result, IDataResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
    }
}
