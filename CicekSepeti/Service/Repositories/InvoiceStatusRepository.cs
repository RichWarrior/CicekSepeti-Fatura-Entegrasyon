using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repositories
{
    public class InvoiceStatusRepository : BaseRepository, IInvoiceStatusRepository
    {
        public InvoiceStatusRepository(DataContext Context) : base(Context)
        {
        }

        public IResult Delete(InvoiceStatus entity)
        {
            entity.StatusId = (int)EnumStatus.Passive;
            Context.InvoiceStatus.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<InvoiceStatus> Find(int id)
        {
            var entity = Context.InvoiceStatus.Where(f => f.Id == id).FirstOrDefault();
            return entity != null ? new SuccessDataResult<InvoiceStatus>(entity) : new ErrorDataResult<InvoiceStatus>(null);
        }

        public IResult Insert(InvoiceStatus entity)
        {
            Context.InvoiceStatus.Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            return new SuccessResult();
        }

        public IDataResult<List<InvoiceStatus>> List()
        {
            var entities = Context.InvoiceStatus.Where(f => f.StatusId == (int)EnumStatus.Active).ToList();
            return new SuccessDataResult<List<InvoiceStatus>>(entities);
        }

        public IResult Update(InvoiceStatus entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Context.InvoiceStatus.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }
    }
}
