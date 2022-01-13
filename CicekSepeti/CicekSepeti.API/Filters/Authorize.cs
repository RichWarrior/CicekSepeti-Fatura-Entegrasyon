using CicekSepeti.API.Models;
using Core.Entities;
using Core.Interfaces;
using Core.Utilities;
using Core.Utilities.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service;
using System;
using System.Linq;
using System.Net;

namespace CicekSepeti.API.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class Authorize : Attribute, IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Filters.Any(f => f.GetType() == typeof(AllowAnonymous)))
            {
                using (IUnitOfWork uow = new UnitOfWork())
                {
                    var token = JWTManager.GetToken(context.HttpContext);
                    if (String.IsNullOrEmpty(token))
                    {
                        UnAuthorized(context);
                        return;
                    }

                    IDataResult<User> existsUser = uow.User.CheckToken(token);
                    if (!existsUser.Success)
                    {
                        UnAuthorized(context);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void UnAuthorized(ActionExecutingContext context)
        {
            BaseResult<int> baseResult = new BaseResult<int>();
            baseResult.Message = "Yetkiniz Bulunamadı!";

            baseResult.StatusCode = HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(baseResult);
        }
    }
}
