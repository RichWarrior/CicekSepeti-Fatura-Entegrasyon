using AutoMapper;
using CicekSepeti.API.Filters;
using Core.Dtos.Request.Auth;
using Core.Dtos.Response.Auth;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace CicekSepeti.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthController : BaseController
    {

        readonly IStringLocalizer<UserResource> l;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_localizer"></param>
        /// <param name="_l"></param>
        public AuthController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _localizer,
            IStringLocalizer<UserResource> _l
            )
            : base(_uow, _mapper, _localizer)
        {
            l = _l;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost("login")]
        [AllowAnonymous]        
        public IActionResult LogIn([FromBody] LogInRequestDTO dto)
        {
            LogInResponse response = new LogInResponse();

            var userExists = uow.User.FindByEmail(dto.Email);
            if (!userExists.Success)
                return NotFound(response, l[userExists.Message]);

            var user = userExists.Data;

            var login = uow.User.LogIn(user, dto.Password);

            if(!login.Success)
                return NotFound(response,l[login.Message]);

            response = mapper.Map<LogInResponse>(user);
            response.Token = login.Data;
            response.ExpireDate = DateTime.Now.AddDays(1);

            return Ok(response);
        }
    }
}
