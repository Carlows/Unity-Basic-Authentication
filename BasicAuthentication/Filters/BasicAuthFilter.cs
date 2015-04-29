using BasicAuthentication.Infrastructure;
using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Ninject;
using System.Web.Mvc;

namespace BasicAuthentication.Filters
{
    public class BasicAuthFilter : AuthorizationFilterAttribute
    {
        bool Active = true;

        public BasicAuthFilter() { }

        public BasicAuthFilter(bool active)
        {
            Active = active;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Active)
            {
                var identity = ParseAuthorizationHeader(actionContext);
                if (identity == null)
                {
                    Challenge(actionContext);
                    return;
                }

                if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
                {
                    Challenge(actionContext);
                    return;
                }

                var principal = new GenericPrincipal(identity, null);
                
                Thread.CurrentPrincipal = principal;

                if (HttpContext.Current != null)
                    HttpContext.Current.User = principal;

                
                base.OnAuthorization(actionContext);
            }
        }

        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var userManager = (IUserManager)DependencyResolver.Current.GetService(typeof(IUserManager));
            return userManager.AuthenticateCredentials(username, password);
        }

        protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            int splitOn = authHeader.IndexOf(':');
            string userName = authHeader.Substring(0, splitOn);
            string password = authHeader.Substring(splitOn + 1);

            return new BasicAuthenticationIdentity(userName, password);
        }
        
        void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }
    }
}