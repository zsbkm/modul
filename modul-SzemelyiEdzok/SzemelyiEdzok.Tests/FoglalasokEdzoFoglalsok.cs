using NUnit.Framework;
using Moq;
using System;

namespace SzemelyiEdzok.Tests
{
    // DTO: Egyszerű foglalás modell
    public class Foglalas
    {
        public int EdzoId { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
    }

    // Interfész a repository-hoz
    public interface IFoglalasRepository
    {
        void AddFoglalas(Foglalas foglalas);
    }

    // Üzleti logikát tartalmazó osztály
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

    // UNIT TESZT OSZTÁLY
    [TestFixture]
    public class SzemelyiEdzoFoglalasok
    {
        [Test]
        public void FoglalasLetrehozasa_HelyesAdatokkal_Sikeres()
        {
            // Arrange
            var mockRepo = new Mock<IFoglalasRepository>();
            var manager = new FoglalasManager(mockRepo.Object);

            var foglalas = new Foglalas
            {
                EdzoId = 1,
                Idopont = DateTime.Today.AddDays(1),
                Nev = "Teszt Felhasználó"
            };

            // Act
            var sikeres = manager.FoglalasLetrehozasa(foglalas);

            // Assert
            Assert.IsTrue(sikeres, "A foglalásnak sikeresnek kellett volna lennie.");
            mockRepo.Verify(r => r.AddFoglalas(It.Is<Foglalas>(f =>
                f.EdzoId == 1 &&
                f.Nev == "Teszt Felhasználó" &&
                f.Idopont.Date == DateTime.Today.AddDays(1).Date
            )), Times.Once);
        }
    }
}