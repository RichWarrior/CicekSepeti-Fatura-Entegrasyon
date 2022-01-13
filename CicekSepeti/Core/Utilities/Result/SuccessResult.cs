
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    public class SuccessResult : Result
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SuccessResult(string message) : base(true, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public SuccessResult() : base(true)
        {
        }
    }
}
