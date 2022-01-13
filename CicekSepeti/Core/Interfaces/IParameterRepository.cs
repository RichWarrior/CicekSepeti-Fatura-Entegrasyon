
using Core.Entities;
using Core.Utilities.Result;

namespace Core.Interfaces
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        IDataResult<Parameter> FindByName(string name);
    }
}
