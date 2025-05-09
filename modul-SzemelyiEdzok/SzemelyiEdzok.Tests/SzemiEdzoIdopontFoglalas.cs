using NUnit.Framework;
using Moq;
using System;

namespace Modul.Tests
{
    // DTO osztály
    public class Foglalas
    {
        public int EdzoId { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
    }

    // Interfész a repositoryhoz
    public interface IFoglalasRepository
    {
        void AddFoglalas(Foglalas foglalas);
    }

    // Foglaláskezelő logika
    public class FoglalasManager
    {
        private readonly IFoglalasRepository _repository;

        public FoglalasManager(IFoglalasRepository repository)
        {
            _repository = repository;
        }

        public bool FoglalasLetrehozasa(Foglalas foglalas)
        {
            if (foglalas == null || foglalas.Idopont < DateTime.Today)
                return false;

            _repository.AddFoglalas(foglalas);
            return true;
        }
    }

    [TestFixture]
    internal class SzemiEdzoIdopontFoglalas
    {
        [Test]
        public void FoglalasLetrehozasa_MultbeliDatum_Sikertelen()
        {
            // Arrange
            var mockRepo = new Mock<IFoglalasRepository>();
            var manager = new FoglalasManager(mockRepo.Object);

            var foglalas = new Foglalas
            {
                EdzoId = 1,
                Idopont = DateTime.Today.AddDays(-3), // múltbeli dátum
                Nev = "Teszt Felhasználó"
            };

            // Act
            var eredmeny = manager.FoglalasLetrehozasa(foglalas);

            // Assert
            Assert.IsFalse(eredmeny, "A foglalásnak sikertelennek kellett volna lennie, mert múltbeli időpontot adtunk meg.");
            mockRepo.Verify(r => r.AddFoglalas(It.IsAny<Foglalas>()), Times.Never);
        }
    }
}
