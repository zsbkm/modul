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
    public class SzemelyiEdzokManager : ISzemelyiEdzokManager
    {
        public SzemelyiEdzokManager() { }

        internal SzemelyiEdzokManager(IUserController userController)
        {
            UserController = userController ?? throw new ArgumentNullException(nameof(userController));
        }

        private IUserController UserController { get; }

        public SzemelyiEdzo[] GetSzemelyiEdzok()
        {
            using (var ctx = DataContext.Instance())
            {
                var r = ctx.GetRepository<SzemelyiEdzo>().Find("WHERE aktiv = 1").ToArray();
                return r;
            }
        }

        public string GetSzemelyiEdzoNevByID(int ID)
        {
            using (var ctx = DataContext.Instance())
            {
                var r = ctx.GetRepository<SzemelyiEdzo>().GetById(ID).Nev;
                return r;
            }
        }
    }
}

