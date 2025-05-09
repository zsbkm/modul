using Moq;
using NUnit.Framework;
using System;
using SzemelyiEdzokTeszt.Classes;

namespace SzemelyiEdzokTeszt.Tests
{
    [TestFixture]
    public class FoglalasokManagerTests
    {
        [Test]
        public void FoglalasLetrehozasa_HelyesAdatokkal_EgyHivasTortenik()
        {
            // Arrange
            var mockRepo = new Mock<IFoglalasokRepository>();
            var manager = new FoglalasokManager(mockRepo.Object);

            var foglalas = new Foglalasok
            {
                SzemelyiEdzoID = 5,
                Nev = "Teszt Elek",
                Sport = "Crossfit",
                Idopont = DateTime.Today.AddDays(1),
                Megjegyzes = "Erőnléti edzés"
            };

            // Act
            manager.FoglalasLetrehozasa(foglalas);

            // Assert
            mockRepo.Verify(r => r.Add(It.Is<Foglalasok>(f =>
                f.SzemelyiEdzoID == 5 &&
                f.Nev == "Teszt Elek" &&
                f.Sport == "Crossfit" &&
                f.Idopont.Date == DateTime.Today.AddDays(1).Date &&
                f.Megjegyzes == "Erőnléti edzés"
            )), Times.Once);
        }
    }
}
