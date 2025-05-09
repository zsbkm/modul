using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SzemelyiEdzok.Tests
{
    // === Modell ===
    internal class Beosztas
    {
        public int EdzoId { get; set; }
        public DateTime Datum { get; set; }
    }

    // === Szolgáltatás ===
    internal class SzemelyiEdzokBeosztasTeszt
    {
        public List<Beosztas> GetBeosztasAdottHetre(int edzoId, DateTime hetKezdete)
        {
            var beosztasok = new List<Beosztas>();
            for (int i = 0; i < 7; i++)
            {
                beosztasok.Add(new Beosztas
                {
                    EdzoId = edzoId,
                    Datum = hetKezdete.AddDays(i)
                });
            }
            return beosztasok;
        }
    }

    // === Unit teszt ===
    [TestFixture]
    internal class SzemelyiEdzokBeosztasTests
    {
        [Test]
        public void GetBeosztasAdottHetre_ShouldReturnSevenDays()
        {
            // Arrange
            var service = new SzemelyiEdzokBeosztasTeszt();
            int edzoId = 1;
            DateTime hetKezdete = new DateTime(2025, 4, 28); // Például hétfői nap

            // Act
            var result = service.GetBeosztasAdottHetre(edzoId, hetKezdete);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(7));
            Assert.That(result[0].Datum, Is.EqualTo(hetKezdete));
            Assert.That(result[6].Datum, Is.EqualTo(hetKezdete.AddDays(6)));
            Assert.That(result.TrueForAll(b => b.EdzoId == edzoId));
        }
    }
}
