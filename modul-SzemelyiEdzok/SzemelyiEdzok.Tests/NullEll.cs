using NUnit.Framework;
using Moq;
using System;

namespace Modul.Tests.NullEll
{
    // Új modell
    public class FoglalasAdat
    {
        public int EdzoId { get; set; }
        public DateTime Idopont { get; set; }
        public string Nev { get; set; }
    }

    // Új interfész
    public interface IFoglalasTarolo
    {
        void Hozzaad(FoglalasAdat foglalas);
    }

    // Szolgáltatás logika
    public class FoglalasSzolgaltatas
    {
        private readonly IFoglalasTarolo _tarolo;

        public FoglalasSzolgaltatas(IFoglalasTarolo tarolo)
        {
            _tarolo = tarolo;
        }

        public bool Letrehoz(FoglalasAdat foglalas)
        {
            if (foglalas == null || string.IsNullOrWhiteSpace(foglalas.Nev))
                return false;

            _tarolo.Hozzaad(foglalas);
            return true;
        }
    }

    [TestFixture]
    internal class NullFoglalasTeszt
    {
        [Test]
        public void Letrehoz_NullFoglalas_SikertelenEsNemMent()
        {
            // Arrange
            var mockTarolo = new Mock<IFoglalasTarolo>();
            var szolgaltatas = new FoglalasSzolgaltatas(mockTarolo.Object);

            FoglalasAdat foglalas = null;

            // Act
            var eredmeny = szolgaltatas.Letrehoz(foglalas);

            // Assert
            Assert.IsFalse(eredmeny, "A foglalásnak sikertelennek kellett lennie, mert null az objektum.");
            mockTarolo.Verify(t => t.Hozzaad(It.IsAny<FoglalasAdat>()), Times.Never);
        }
    }
}
