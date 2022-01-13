
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 
        /// </summary>
        bool Success { get; }
        /// <summary>
        /// 
        /// </summary>
        string Message { get; }
    }
}
