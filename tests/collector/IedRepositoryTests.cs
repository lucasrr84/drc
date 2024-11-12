using collector.domain.entity;
using collector.infra.repository;
using NUnit.Framework;

namespace tests.collector
{
    public class IedRepositoryTests
    {
        private IIedRepository _iedRepository;


        [SetUp]
        public void Setup()
        {
            _iedRepository = new IedRepositoryMemory();
        }

        [Test]
        public async Task Deve_Incluir_Ied()
        {
            // Arrange
            var newIed = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
            
            // Act
            await _iedRepository.Save(newIed);
            var ied = await _iedRepository.GetById(newIed.Id.GetValue());
            
            // Assert
            Assert.That(ied?.Id.GetValue(), Is.EqualTo(newIed.Id.GetValue()));
        }
    }
}