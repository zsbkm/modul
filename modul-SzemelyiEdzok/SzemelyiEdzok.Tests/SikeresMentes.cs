using NUnit.Framework;
using Moq;
using System;

namespace Modul.Tests.Sikeres
{
    // Modell
    public class FoglalasAdat
    {
        public int EdzoId { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
        public string Sport { get; set; }
        public string Megjegyzes { get; set; }
    }

    // Interfész
    public interface IFoglalasTarolo
    {
        void Hozzaad(FoglalasAdat foglalas);
    }

    // Logika
    public class FoglalasSzolgaltatas
    {
        private readonly IFoglalasTarolo _tarolo;

        public FoglalasSzolgaltatas(IFoglalasTarolo tarolo)
        {
            _tarolo = tarolo;
        }

        public bool Letrehoz(FoglalasAdat foglalas)
        {
            if (string.IsNullOrWhiteSpace(foglalas.Nev))
                return false;

            _tarolo.Hozzaad(foglalas);
            return true;
        }
    }

    // Teszt
    [TestFixture]
    internal class SikeresMentes
    {
        [Test]
        public void Letrehoz_HelyesAdatokkal_Sikeres()
        {
            // Arrange
            var mockTarolo = new Mock<IFoglalasTarolo>();
            var szolgaltatas = new FoglalasSzolgaltatas(mockTarolo.Object);

            var foglalas = new FoglalasAdat
            {
                EdzoId = 1,
                Idopont = DateTime.Today.AddDays(1),
                Nev = "Kovács Béla",
                Sport = "Spinning",
                Megjegyzes = "Délelőtti edzés"
            };

            // Act
            var sikeres = szolgaltatas.Letrehoz(foglalas);

            // Assert
            Assert.IsTrue(sikeres);
            mockTarolo.Verify(t => t.Hozzaad(It.Is<FoglalasAdat>(f =>
                f.Nev == "Kovács Béla" &&
                f.EdzoId == 1 &&
                f.Sport == "Spinning"
            )), Times.Once);
        }
    }
}
