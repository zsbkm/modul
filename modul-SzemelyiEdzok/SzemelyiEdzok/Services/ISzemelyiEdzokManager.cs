using SzemelyiEdzokSzemelyiEdzok.Models;

namespace SzemelyiEdzokSzemelyiEdzok.Services
{
    public interface ISzemelyiEdzokManager
    {
        SzemelyiEdzo[] GetSzemelyiEdzok();
        string GetSzemelyiEdzoNevByID(int ID);
    }
}
