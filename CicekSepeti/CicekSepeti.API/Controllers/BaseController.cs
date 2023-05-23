using AutoMapper;
using CicekSepeti.API.Filters;
using CicekSepeti.API.Models;
using Core.Entities;
using Core.Interfaces;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Net;

namespace CicekSepeti.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IUnitOfWork uow;
        /// <summary>
        /// 
        /// </summary>
        public readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        public readonly IStringLocalizer<BaseResource> localizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_localizer"></param>
        public BaseController(
                IUnitOfWork _uow,
                IMapper _mapper,
                IStringLocalizer<BaseResource> _localizer
            )
        {
            uow = _uow;
            mapper = _mapper;
            localizer = _localizer;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentUserToken => JWTManager.GetToken(HttpContext);
        /// <summary>
        /// 
        /// </summary>
        public int CurrentUserId => JWTManager.GetUserId(HttpContext, uow);
        /// <summary>
        /// 
        /// </summary>
        public User CurrentUser => JWTManager.GetUser(CurrentUserId, uow);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok<T>(T data, string message = "")
        {
            BaseResult<T> baseResult = new BaseResult<T>()
            {
                Data = data,
                Message = message,
                StatusCode = HttpStatusCode.OK
            };

            if (string.IsNullOrEmpty(message))
                baseResult.Message = localizer[Messages.Successful];
            return new OkObjectResult(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult NotFound<T>(T data, string message = "")
        {
            BaseResult<T> baseResult = new BaseResult<T>()
            {
                Data = data,
                Message = message,
                StatusCode = HttpStatusCode.NotFound
            };

            if (string.IsNullOrEmpty(message))
                baseResult.Message = localizer[Messages.Error];
            return new NotFoundObjectResult(baseResult);

        }
    }
}
