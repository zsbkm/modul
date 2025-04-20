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
    internal class FoglalasokManager : IFoglalasokManager
    {
        public FoglalasokManager() : this(DotNetNuke.Entities.Users.UserController.Instance) { }

        public FoglalasokManager(IUserController userController)
        {
            UserController = userController ?? throw new ArgumentNullException(nameof(userController));
        }

        private IUserController UserController { get; }
        public string FoglalasKeszites(int SzemelyiEdzoID, string Nev, string Sport, DateTime Idopont, string Megjegyzes)
        {
            
            Api proxy = new Api("http://www.dnndev.me/", "1-088ba0bd-3b39-4494-aae4-6ac3219d1421");

            var order = new OrderDTO();
            // add billing information
            order.BillingAddress = new AddressDTO
            {
                AddressType = AddressTypesDTO.Billing,
                City = "West Palm Beach",
                CountryBvin = "BF7389A2-9B21-4D33-B276-23C9C18EA0C0",
                FirstName = "John",
                LastName = "Dough",
                Line1 = "319 N. Clematis Street",
                Line2 = "Suite 500",
                Phone = "561-228-5319",
                PostalCode = "33401",
                RegionBvin = "7EBE4F07-A844-47B8-BDA8-863DDDF5C778"
            };

            // add at least one line item
            order.Items = new List<LineItemDTO>();
            order.Items.Add(new LineItemDTO
            {
                ProductId = "06AD9013-C5AE-435F-BB6A-4AE3576ECB86",
                Quantity = 1
            });

            // add the shipping address
            order.ShippingAddress = new AddressDTO();
            order.ShippingAddress = order.BillingAddress;
            order.ShippingAddress.AddressType = AddressTypesDTO.Shipping;

            // specify who is creating the order
            order.UserEmail = "info@hotcakescommerce.com";
            order.UserID = "1";

            // call the API to create the order
            ApiResponse<OrderDTO> response = proxy.OrdersCreate(order);

            UserInfo currentUser = UserController.GetCurrentUserInfo();
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

                return "Sikeres foglalás!";
            }
        }
    }
}

