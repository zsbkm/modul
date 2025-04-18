using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class SzemelyiEdzokController : DnnController
    {
        SzemelyiEdzokManager szemelyiEdzokManager = new SzemelyiEdzokManager();
        public ActionResult Index()
        {
            var szemelyiEdzok = szemelyiEdzokManager.GetSzemelyiEdzok();
            ViewBag.Edzok = szemelyiEdzok;
            return View();
        }
    }
}
