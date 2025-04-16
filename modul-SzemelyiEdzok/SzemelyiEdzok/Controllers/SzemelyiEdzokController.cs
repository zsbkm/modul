using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System.Web.Mvc;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class SzemelyiEdzokController : DnnController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
