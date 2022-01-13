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
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository(DataContext Context) : base(Context)
        {

        }

        public IResult Delete(Invoice entity)
        {
            entity.StatusId = (int)EnumStatus.Passive;
            Context.Invoice.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<Invoice> Find(int id)
        {
            var entity = Context.Invoice.Where(f => f.Id == id && f.StatusId == (int)EnumStatus.Active).FirstOrDefault();
            return entity != null ? new SuccessDataResult<Invoice>(entity) : new ErrorDataResult<Invoice>(null, "");
        }

        public IResult Insert(Invoice entity)
        {
            Context.Invoice.Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            return new SuccessResult();
        }

        public IDataResult<List<Invoice>> List()
        {
            var entities= Context.Invoice.Where(f=>f.StatusId == (int)EnumStatus.Active).ToList();
            return new SuccessDataResult<List<Invoice>>(entities);
        }       

        public IResult Update(Invoice entity)
        {
            entity.ModifiedDate = DateTime.Now;
            //Context.Invoice.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<List<Invoice>> List(int fileId)
        {
            var entities = Context.Invoice.Where(f => f.FileId == fileId)
                   .Include(f => f.InvoiceStatus)
                   .ToList();
            return new SuccessDataResult<List<Invoice>>(entities);
        }

        public IDataResult<List<Invoice>> List(List<int> ids)
        {
            var entities = Context.Invoice.Where(f => ids.Contains(f.Id))
                .Include(f => f.InvoiceStatus)
                .ToList();
            return new SuccessDataResult<List<Invoice>>(entities);      
        }
    }
}
