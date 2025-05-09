using System;
using System.Collections.Generic;

namespace SzemelyiEdzokTeszt.Classes
{
    public class Foglalasok
    {
        public int ID { get; set; }
        public int SzemelyiEdzoID { get; set; }
        public string Nev { get; set; }
        public string Sport { get; set; }
        public DateTime Idopont { get; set; }
        public string Megjegyzes { get; set; }
    }
    public interface IFoglalasokRepository
    {
        void Add(Foglalasok foglalas);
        List<Foglalasok> GetAll();
    }

    public class FoglalasokManager
    {
        private readonly IFoglalasokRepository _repository;

        public FoglalasokManager(IFoglalasokRepository repository)
        {
            _repository = repository;
        }

        public void FoglalasLetrehozasa(Foglalasok foglalas)
        {
            _repository.Add(foglalas);
        }
    }
}
