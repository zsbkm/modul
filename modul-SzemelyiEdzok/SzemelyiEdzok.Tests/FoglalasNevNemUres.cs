using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace Modul.Tests.NevEllenorzes
{
    // Modell
    public class Foglalasok
    {
        public int SzemelyiEdzoID { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
        public string Sport { get; set; }
        public string Megjegyzes { get; set; }
    }

    // Egyedi interfész csak ehhez a teszthez
    public interface IFoglalasNevEllenorzoRepository
    {
        List<Foglalasok> GetAll();
        void Add(Foglalasok foglalas);
    }

    // Üzleti logika névellenőrzéssel
    public class FoglalasNevEllenorzoManager
    {
        private readonly IFoglalasNevEllenorzoRepository _repository;

        public FoglalasNevEllenorzoManager(IFoglalasNevEllenorzoRepository repository)
        {
            _repository = repository;
        }

        public bool FoglalasLetrehozasa(Foglalasok foglalas)
        {
            if (string.IsNullOrWhiteSpace(foglalas.Nev))
                return false;

            _repository.Add(foglalas);
            return true;
        }
    }

    // Unit teszt
    [TestFixture]
    internal class FoglalasNevNemUres
    {
        [Test]
        public void FoglalasLetrehozasa_UresNev_Sikertelen()
        {
            // Arrange
            var mockRepo = new Mock<IFoglalasNevEllenorzoRepository>();
            var manager = new FoglalasNevEllenorzoManager(mockRepo.Object);

            var foglalas = new Foglalasok
            {
                SzemelyiEdzoID = 1,
                Idopont = DateTime.Today.AddDays(1),
                Nev = "", // üres név
                Sport = "Spinning",
                Megjegyzes = "Név nem lett megadva"
            };

            // Act
            var eredmeny = manager.FoglalasLetrehozasa(foglalas);

            // Assert
            Assert.IsFalse(eredmeny, "A foglalásnak sikertelennek kellett volna lennie üres név esetén.");
            mockRepo.Verify(r => r.Add(It.IsAny<Foglalasok>()), Times.Never);
        }
    }
}
