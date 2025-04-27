using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.Api;
using System;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;
using DotNetNuke.Data;
using SzemelyiEdzokSzemelyiEdzok.Models;
using System.Collections.Generic;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class IdopontvalasztoApiController : DnnApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetFoglalasok(int id, DateTime datum)
        {
            try
            {
                using (var ctx = DataContext.Instance())
                {
                    var startDate = datum.Date;
                    var endDate = startDate.AddDays(1);
                    var r = ctx.GetRepository<Foglalasok>().Find("WHERE SzemelyiEdzoID = @0 AND idopont >= @1 AND idopont < @2", id, startDate, endDate);

                    List<string> outputList = new List<string>();
                    foreach (var item in r)
                    {
                        string time = item.Idopont.ToString("HH:mm");
                        outputList.Add(time);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, outputList);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}