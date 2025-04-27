using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.Api;
using System;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class IdopontvalasztoApiController : DnnApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            try
            {
                var values = new string[] { "value1", "value2" };
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}