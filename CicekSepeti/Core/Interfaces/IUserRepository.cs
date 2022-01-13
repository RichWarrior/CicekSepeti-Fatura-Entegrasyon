using Core.Entities;
using Core.Utilities.Result;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IDataResult<User> FindByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IDataResult<string> LogIn(User user, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IDataResult<User> CheckToken(string token);
    }
}
