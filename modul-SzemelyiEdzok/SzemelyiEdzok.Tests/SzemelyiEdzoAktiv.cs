using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modul.Tests
{
    // Egyszerű modell osztály
    public class SzemelyiEdzo
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public bool Aktiv { get; set; }
    }

    // Mockolható repository interfész
    public interface ISzemelyiEdzoRepository
    {
        List<SzemelyiEdzo> GetAll();
    }

    // A tesztelendő üzleti logika
    public class SzemelyiEdzokManager
    {
        private readonly ISzemelyiEdzoRepository _repository;

        public SzemelyiEdzokManager(ISzemelyiEdzoRepository repository)
        {
            _repository = repository;
        }

        public List<SzemelyiEdzo> GetAktivEdzok()
        {
            return _repository.GetAll().Where(e => e.Aktiv).ToList();
        }
    }

    [TestFixture]
    internal class SzemelyiEdzoAktiv
    {
        [Test]
        public void GetAktivEdzok_CsakAktivEdzoketAdVissza()
        {
            // Arrange
            var mockRepo = new Mock<ISzemelyiEdzoRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<SzemelyiEdzo>
            {
                new SzemelyiEdzo { ID = 1, Nev = "Anna", Aktiv = true },
                new SzemelyiEdzo { ID = 2, Nev = "Péter", Aktiv = false },
                new SzemelyiEdzo { ID = 3, Nev = "Lili", Aktiv = true }
            });

            var manager = new SzemelyiEdzokManager(mockRepo.Object);

            // Act
            var aktivEdzok = manager.GetAktivEdzok();

            // Assert
            Assert.That(aktivEdzok.Count, Is.EqualTo(2));
            Assert.That(aktivEdzok.All(e => e.Aktiv), Is.True);
            Assert.That(aktivEdzok.Any(e => e.Nev == "Péter"), Is.False);
        }
    }
}
