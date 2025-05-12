using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.UI.UserControls;
using SzemelyiEdzokSzemelyiEdzok.Models;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Security.Policy;
using DotNetNuke.Entities.Modules;
using Hotcakes.CommerceDTO.v1;
using Hotcakes.CommerceDTO.v1.Orders;
using Hotcakes.CommerceDTO.v1.Contacts;
using System.Collections.Generic;
using Hotcakes.CommerceDTO.v1.Client;


namespace SzemelyiEdzokSzemelyiEdzok.Services.Implementations
{
    public class FoglalasokManager : IFoglalasokManager
    {
        public FoglalasokManager() : this(DotNetNuke.Entities.Users.UserController.Instance) { }

        public FoglalasokManager(IUserController userController)
        {
            UserController = userController ?? throw new ArgumentNullException(nameof(userController));
        }

        private IUserController UserController { get; }
        public string FoglalasKeszites(int SzemelyiEdzoID, string Nev, string Sport, DateTime Idopont, string Megjegyzes, string HotCakesApiKey)
        {
            UserInfo currentUser = UserController.GetCurrentUserInfo();

            /*
            Api proxy = new Api("http://rendfejl1000.northeurope.cloudapp.azure.com:8080/", HotCakesApiKey);

            var order = new OrderDTO();
            // add billing information
            order.BillingAddress = new AddressDTO
            {
                AddressType = AddressTypesDTO.Billing,
                City = "Budapest",
                CountryBvin = "ACF84F60-6B00-4131-A5BE-FA202F1EB569",
                FirstName = "zsbkm",
                LastName = "Fitness",
                Line1 = "Fővám tér 8",
                Line2 = "",
                Phone = "36 1 482 5000",
                PostalCode = "1093",
                RegionBvin = ""
            };

            // add at least one line item
            order.Items = new List<LineItemDTO>();
            order.Items.Add(new LineItemDTO
            {
                ProductId = "DFDE64B3-1FA4-48DB-B25D-3E6457E0D3F2",
                Quantity = 1
            });

            // add the shipping address
            order.ShippingAddress = new AddressDTO();
            order.ShippingAddress = order.BillingAddress;
            order.ShippingAddress.AddressType = AddressTypesDTO.Shipping;
            order.IsPlaced = true;
            // specify who is creating the order
            order.UserEmail = "";
            order.UserID = currentUser.UserID.ToString();

            // call the API to create the order
            ApiResponse<OrderDTO> response = proxy.OrdersCreate(order);
            */

            using (var ctx = DataContext.Instance())
            {
                Foglalasok foglalas = new Foglalasok
                {
                    SzemelyiEdzoID = SzemelyiEdzoID,
                    Nev = Nev,
                    Sport = Sport,
                    Idopont = Idopont,
                    Megjegyzes = Megjegyzes,
                    DNN_azonosito = currentUser.UserID
                };

                ctx.GetRepository<Foglalasok>().Insert(foglalas);
            }

            return "Sikeres foglalás!";
        }

        public Foglalasok[] GetFoglalasok()
        {
            UserInfo currentUser = UserController.GetCurrentUserInfo();

            using (var ctx = DataContext.Instance())
            {
                if (currentUser.IsSuperUser)
                {
                    return ctx.GetRepository<Foglalasok>().Find("").ToArray();
                }
                else
                {
                    // Modified query with JOIN and OR condition
                    return ctx.GetRepository<Foglalasok>()
                        .Find("JOIN Szemelyi_edzok ON Foglalasok.SzemelyiEdzoID = Szemelyi_edzok.ID WHERE Foglalasok.DNN_azonosito = @0 OR Szemelyi_edzok.DNN_azonosito = @0", currentUser.UserID)
                        .ToArray();
                }
            }
        }
    }
}

