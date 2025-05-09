using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modul.Tests
{
    // Modell
    public class Foglalasok
    {
        public int SzemelyiEdzoID { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
    }

    // Repository interfész
    public interface IFoglalasokRepository
    {
        List<Foglalasok> GetAll();
        void Add(Foglalasok foglalas);
    }

    // Üzleti logika, duplikáció ellenőrzéssel
    public class FoglalasokManager
    {
        private readonly IFoglalasokRepository _repository;

        public FoglalasokManager(IFoglalasokRepository repository)
        {
            _repository = repository;
        }

        public bool FoglalasLetrehozasa(Foglalasok foglalas)
        {
            bool vanDuplikacio = _repository.GetAll().Any(f =>
                f.SzemelyiEdzoID == foglalas.SzemelyiEdzoID &&
                f.Idopont == foglalas.Idopont);

            if (vanDuplikacio)
                return false;

            _repository.Add(foglalas);
            return true;
        }
    }

    // Tesztosztály
    [TestFixture]
    internal class DuplikacioEllenorzes
    {
        [Test]
        public void FoglalasLetrehozasa_UgyanarraAzIdopontraEsEdzore_Sikertelen()
        {
            // Arrange
            var foglaltIdopont = DateTime.Today.AddDays(1);
            var mockRepo = new Mock<IFoglalasokRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Foglalasok>
            {
                new Foglalasok
                {
                    SzemelyiEdzoID = 1,
                    Idopont = foglaltIdopont,
                    Nev = "Létező Vendég"
                }
            });

            var manager = new FoglalasokManager(mockRepo.Object);

            var ujFoglalas = new Foglalasok
            {
                SzemelyiEdzoID = 1,
                Idopont = foglaltIdopont,
                Nev = "Új Vendég"
            };

            // Act
            var eredmeny = manager.FoglalasLetrehozasa(ujFoglalas);

            // Assert
            Assert.IsFalse(eredmeny, "A foglalásnak sikertelennek kellett volna lennie a duplikáció miatt.");
            mockRepo.Verify(r => r.Add(It.IsAny<Foglalasok>()), Times.Never);
        }
    }
}
