using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using DotNetNuke.Data;
using SzemelyiEdzokSzemelyiEdzok.Models;
using System.Web.Http;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    [DnnAuthorize(StaticRoles = "Registered Users")]
    public class IdopontvalasztoApiController : DnnApiController
    {
        [HttpGet]
        public IHttpActionResult GetFoglalasok(int id, DateTime datum)
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
                    outputList.Sort();
                    return Ok(outputList);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}