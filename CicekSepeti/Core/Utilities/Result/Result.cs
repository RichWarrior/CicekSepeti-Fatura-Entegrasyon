
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        public Result(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; }
    }
}
