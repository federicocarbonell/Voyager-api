using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using VoyageAPI.Logic;

namespace VoyageAPI.Filter
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IEmployeeLogic _logic;
        public AuthorizationFilter(IEmployeeLogic logic)
        {
            _logic = logic;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (token == null || token == "")
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    ContentType = "json",
                    Content = "You are not authorized"
                };
            }
            else
            {
                var query = from us
                            in _logic.GetAll()
                            where (us.Token == token)
                            select us;
                if (query.Count() == 0)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        ContentType = "json",
                        Content = "The token you put has expired"
                    };
                }
            }
        }
    }
}
