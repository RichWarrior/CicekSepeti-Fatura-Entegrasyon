using Core.Entities;
using Core.Utilities.Result;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        IDataResult<List<Invoice>> List(int fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IDataResult<List<Invoice>> List(List<int> ids);
    }
}
