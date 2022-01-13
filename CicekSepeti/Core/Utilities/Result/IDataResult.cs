
namespace Core.Utilities.Result
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataResult<T> : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        T Data { get; }
    }
}
