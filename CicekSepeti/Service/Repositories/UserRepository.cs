using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Utilities;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DataContext Context) : base(Context)
        {
        }

        public IResult Delete(User entity)
        {
            entity.StatusId = (int)EnumStatus.Passive;
            Context.User.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<User> Find(int id)
        {
            var entity = Context.User.Where(f => f.Id == id).FirstOrDefault();
            return entity != null ? new SuccessDataResult<User>(entity) : new ErrorDataResult<User>(null, "");
        }       

        public IResult Insert(User entity)
        {
            entity.Password = EncryptProvider.Md5(entity.Password);
            Context.User.Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            return new SuccessResult();
        }

        public IDataResult<List<User>> List()
        {
            var entities = Context.User.Where(f => f.StatusId == (int)EnumStatus.Active).ToList();
            return new SuccessDataResult<List<User>>(entities);
        }

        public IResult Update(User entity)
        {
            Context.User.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return new SuccessResult();
        }

        public IDataResult<User> FindByEmail(string email)
        {
            var user = Context.User.Where(f => f.Email == email && f.StatusId == (int)EnumStatus.Active).FirstOrDefault();
            return user != null ? new SuccessDataResult<User>(user) : new ErrorDataResult<User>(null, Messages.User.NotFoundUser);
        }

        public IDataResult<string> LogIn(User user, string password)
        {
            if (user.Password != EncryptProvider.Md5(password))
                return new ErrorDataResult<string>(string.Empty, "");

            string token = JWTManager.GenerateToken(user);
            try
            {
                var db = Cache.GetDatabase(0);
                db.StringSet($"{token}_user",JsonConvert.SerializeObject(user),TimeSpan.FromDays(1));
            }
            catch (System.Exception)
            {
                return new ErrorDataResult<string>(string.Empty,Messages.Error);
            }
            return new SuccessDataResult<string>(token);
        }

        public IDataResult<User> CheckToken(string token)
        {
            token += "_user";
            try
            {
                var db = Cache.GetDatabase(0);
                if (!db.KeyExists(token))
                    return new ErrorDataResult<User>(null, Messages.UnAuthorized);
                var userJson = db.StringGet(token);
                return new SuccessDataResult<User>(JsonConvert.DeserializeObject<User>(userJson));
            }
            catch (Exception)
            {

            }
            return new ErrorDataResult<User>(null, Messages.UnAuthorized);
        }
    }
}
