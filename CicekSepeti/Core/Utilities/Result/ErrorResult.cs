
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorResult : Result
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorResult(string message) : base(false, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ErrorResult() : base(false)
        {
        }
    }
}
