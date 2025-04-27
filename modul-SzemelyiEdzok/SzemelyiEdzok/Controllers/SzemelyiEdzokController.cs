using DotNetNuke.Collections;
using DotNetNuke.Entities.Modules;
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
        
        public ActionResult Index()
        {
            SzemelyiEdzokManager szemelyiEdzokManager = new SzemelyiEdzokManager();
            var szemelyiEdzok = szemelyiEdzokManager.GetSzemelyiEdzok();
            ViewBag.Edzok = szemelyiEdzok;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult FoglalasKeszites(int SzemelyiEdzoID, string Nev, string Sport, DateTime Idopont, string Megjegyzes, int ModuleId)
        {
            var settings = new ModuleController().GetModuleSettings(ModuleId);
            var HotCakesApiKey = settings["SzemelyiEdzok_HotCakesApiKey"]?.ToString() ?? "";
            FoglalasokManager foglalasokManager = new FoglalasokManager();
            string resultMessage = foglalasokManager.FoglalasKeszites(SzemelyiEdzoID, Nev, Sport, Idopont, Megjegyzes, HotCakesApiKey);
            return Json(new { success = true, message = resultMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}