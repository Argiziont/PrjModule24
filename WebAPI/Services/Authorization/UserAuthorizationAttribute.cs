using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Services.DataBase;

namespace WebAPI.Services.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public UserAuthorizationAttribute()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }
        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        ///     This will Authorize User
        /// </summary>
        /// <returns></returns>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext == null) return;
            filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authTokens);

            var token = authTokens.FirstOrDefault();

            if (token != null)
            {
                var authToken = token;
                if (IsValidToken(authToken))
                {
                    filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                    filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                }
                else
                {
                    filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");


                    filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase =
                        "Not Authorized";
                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Invalid Token"
                        }
                    };
                }
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase =
                    "Please Provide authToken";
                filterContext.Result = new JsonResult("Please Provide authToken")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Please Provide authToken"
                    }
                };
            }
        }

        private bool IsValidToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal =
                    tokenHandler.ValidateToken(authToken.Split(' ')[1], validationParameters, out validatedToken);
            }
            catch
            {
                return false;
            }
          
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };
        }
    }
}