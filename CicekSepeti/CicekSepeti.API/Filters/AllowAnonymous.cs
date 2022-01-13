using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CicekSepeti.API.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AllowAnonymous : Attribute, IActionFilter
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
        }
    }
}
