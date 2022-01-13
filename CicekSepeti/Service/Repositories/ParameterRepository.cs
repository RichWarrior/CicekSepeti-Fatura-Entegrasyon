using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Utilities;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repositories
{
    public class ParameterRepository : BaseRepository, IParameterRepository
    {
        public ParameterRepository(DataContext Context) : base(Context)
        {
        }

        public IResult Delete(Parameter entity)
        {
            Context.Parameter.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<Parameter> Find(int id)
        {
            var entity = Context.Parameter.Where(f => f.Id == id).FirstOrDefault();
            return entity != null ? new SuccessDataResult<Parameter>(entity) : new ErrorDataResult<Parameter>(null,"");
        }       

        public IResult Insert(Parameter entity)
        {
            Context.Parameter.Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            return new SuccessResult();
        }

        public IDataResult<List<Parameter>> List()
        {
            var list = Context.Parameter.Where(f => f.StatusId == (int)EnumStatus.Active).ToList();
            return new SuccessDataResult<List<Parameter>>(list);
        }

        public IResult Update(Parameter entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Context.Parameter.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<Parameter> FindByName(string name)
        {
            var entity = Context.Parameter.Where(f => f.Name == name && f.StatusId == (int)EnumStatus.Active).FirstOrDefault();
            return entity != null ? new SuccessDataResult<Parameter>(entity) : new ErrorDataResult<Parameter>(null, Messages.Parameter.NotFoundParameter);
        }
    }
}
