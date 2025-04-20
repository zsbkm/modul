using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Services;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class SzemelyiEdzokController : DnnController
    {
        readonly SzemelyiEdzokManager szemelyiEdzokManager = new SzemelyiEdzokManager();
        public ActionResult Index()
        {
            var szemelyiEdzok = szemelyiEdzokManager.GetSzemelyiEdzok();
            ViewBag.Edzok = szemelyiEdzok;
            return View();
        }

        [AllowAnonymous]
        public ActionResult FoglalasKeszites(int id)
        {
            try
            {
                return Json(new { success = true, message = "Sikeres foglalás! " + id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return Json(new { success = false, message = "Hiba történt a foglalás során." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
