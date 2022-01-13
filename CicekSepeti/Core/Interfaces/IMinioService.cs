using Core.Dtos.Response.File;
using Core.Utilities.Result;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMinioService
    {
        Task<bool> Upload(string fileName, string filePath, string contentType);
        Task<IDataResult<DownloadResponse>>GetLink(string id);
    }
}
