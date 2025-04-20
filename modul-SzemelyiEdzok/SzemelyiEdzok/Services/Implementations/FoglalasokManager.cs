using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.UI.UserControls;
using SzemelyiEdzokSzemelyiEdzok.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SzemelyiEdzokSzemelyiEdzok.Services.Implementations
{
    internal class FoglalasokManager : IFoglalasokManager
    {
        public FoglalasokManager() { }

        internal FoglalasokManager(IUserController userController)
        {
            UserController = userController ?? throw new ArgumentNullException(nameof(userController));
        }

        private IUserController UserController { get; }
        public string FoglalasKeszites(int SzemelyiEdzoID, string Nev, string Sport, DateTime Idopont, string Megjegyzes)
        {
            using (var ctx = DataContext.Instance())
            {
                Foglalasok foglalas = new Foglalasok
                {
                    SzemelyiEdzoID = SzemelyiEdzoID,
                    Nev = Nev,
                    Sport = Sport,
                    Idopont = Idopont,
                    Megjegyzes = Megjegyzes,
                    DNN_azonosito = 5
                };

                ctx.GetRepository<Foglalasok>().Insert(foglalas);

                return "Sikeres foglalás!";
            }
        }
    }
}

