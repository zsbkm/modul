using DotNetNuke.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using SzemelyiEdzokSzemelyiEdzok.Models;
using SzemelyiEdzokSzemelyiEdzok.Services;
using SzemelyiEdzokSzemelyiEdzok.Services.Implementations;

namespace SzemelyiEdzokSzemelyiEdzok.Controllers
{
    public class SzemelyiEdzokController : DnnController
    {
        
        public ActionResult Index()
        {
            SzemelyiEdzokManager szemelyiEdzokManager = new SzemelyiEdzokManager();
            var settings = new ModuleController().GetModuleSettings(ModuleContext.ModuleId);
            var SzemelyiEdzokID = settings["SzemelyiEdzok_SzemelyiEdzoID"]?.ToString() ?? "";
            SzemelyiEdzo[] szemelyiEdzok;
            if (string.IsNullOrWhiteSpace(SzemelyiEdzokID))
            {
                szemelyiEdzok = szemelyiEdzokManager.GetSzemelyiEdzok();
            }
            else
            {
                szemelyiEdzok = szemelyiEdzokManager.GetSzemelyiEdzok(SzemelyiEdzokID);
            }
            
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