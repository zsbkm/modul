using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace SzemelyiEdzok.Tests
{
    public class SzemelyiEdzo
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public DateTime Szul_ido { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Bio { get; set; }
        public string Motto { get; set; }
        public string Sportok { get; set; }
        public string Napszak { get; set; }
    }

    public interface ISzemelyiEdzoRepository
    {
        Task<List<SzemelyiEdzo>> GetAllAsync();
    }

    public class SzemelyiEdzokManager
    {
        private readonly ISzemelyiEdzoRepository _repository;

        public SzemelyiEdzokManager(ISzemelyiEdzoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<SzemelyiEdzo>> GetAllTrainersAsync()
        {
            return _repository.GetAllAsync();
        }
    }

    [TestFixture]
    public class TrainerServiceTests
    {
        [Test]
        public async Task GetAllTrainers_ShouldReturnListOfSzemelyiEdzok()
        {
            // Arrange
            var mockRepo = new Mock<ISzemelyiEdzoRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<SzemelyiEdzo>
                {
                    new SzemelyiEdzo { ID = 1, Nev = "Kiss Anna", Szul_ido = new DateTime(1990, 1, 1) },
                    new SzemelyiEdzo { ID = 2, Nev = "Nagy Péter", Szul_ido = new DateTime(1985, 6, 10) }
                });

            var service = new SzemelyiEdzokManager(mockRepo.Object);

            // Act
            var result = await service.GetAllTrainersAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(t => t.Nev == "Kiss Anna"), Is.True);
            Assert.That(result.Any(t => t.Nev == "Nagy Péter"), Is.True);
        }
    }
}
