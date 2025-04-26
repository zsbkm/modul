using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;

namespace Foglalasok.Foglalasok.Controllers
{
    [DnnHandleError]
    public class FoglalasokController : DnnController
    {

        public ActionResult Index()
        {
            FoglalasokManager foglalasokManager = new FoglalasokManager();
            var foglalasok = foglalasokManager.GetFoglalasok();
            ViewBag.foglalasok = foglalasok;
            return View();
        }
    }
}
