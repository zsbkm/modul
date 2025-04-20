using System;
using SzemelyiEdzokSzemelyiEdzok.Models;

namespace SzemelyiEdzokSzemelyiEdzok.Services
{
    public interface IFoglalasokManager
    {
        string FoglalasKeszites(int SzemelyiEdzoID, string Nev, string Sport, DateTime Idopont, string Megjegyzes);
    }
}
