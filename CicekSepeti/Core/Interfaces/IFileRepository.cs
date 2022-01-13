using Core.Entities;
using Core.Utilities.Result;
using Stream = System.IO.Stream;

namespace Core.Interfaces
{
    public interface IFileRepository : IRepository<File>
    {
        IDataResult<File> ReadData(Stream stream,File file,Parameter taxParameter);
    }
}
