using BasicAuthentication.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAuthentication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [BasicAuthFilter]
        public string Get()
        {
            return "42";
        }
    }
}
