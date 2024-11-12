using collector.application.usecase.ied;
using collector.domain.dto;
using collector.domain.entity;
using collector.infra.driver;
using collector.infra.logger;
using collector.infra.repository;
using NUnit.Framework;

namespace tests.collector
{
    [TestFixture]
    public class IedTests
    {
        private IIedRepository _iedRepository;
        private GetAllIeds _getAllIeds;
        private GetEnabledIeds _getEnabledIeds;
        private GetIedById _getIedById;
        private ExtractDisturbanceFromIed _extractDisturbanceFromIed;
        private ILog _logger;


        [SetUp]
        public void Setup()
        {
            _logger = new ConsoleLog();
            _iedRepository = new IedRepositoryMemory();
            _getAllIeds = new GetAllIeds(_iedRepository);
            _getEnabledIeds = new GetEnabledIeds(_iedRepository, _logger);
            _getIedById = new GetIedById(_iedRepository);
            _extractDisturbanceFromIed = new ExtractDisturbanceFromIed(_iedRepository, _logger);
        }

        [Test]
        public async Task Deve_Retornar_Todos_Ieds()
        {
            // Arrange
            var newIed1 = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
            var newIed2 = Ied.Create("Efacec", "L420", "TST", "21X2", "127.0.0.2", "127.0.0.2", 102, false);
            var newIed3 = Ied.Create("Siemens", "7SJ82", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);

            // Act
            await _iedRepository.Save(newIed1);
            await _iedRepository.Save(newIed2);
            await _iedRepository.Save(newIed3);
           
            var ieds = await _getAllIeds.Execute();
            
            // Assert    
            Assert.That(ieds.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Deve_Retornar_Ieds_Habilitados()
        {
            // Arrange
            var newIed1 = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
            var newIed2 = Ied.Create("Efacec", "L420", "TST", "21X2", "127.0.0.2", "127.0.0.2", 102, false);
            var newIed3 = Ied.Create("Siemens", "7SJ82", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);

            // Act
            await _iedRepository.Save(newIed1);
            await _iedRepository.Save(newIed2);
            await _iedRepository.Save(newIed3);
           
            var enabledIeds = await _getEnabledIeds.Execute();
            
            // Assert    
            Assert.That(enabledIeds.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Deve_Retornar_Ied_Pelo_Id()
        {
            // Arrange
            var newIed1 = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);

            // Act
            await _iedRepository.Save(newIed1);
           
            var ied = await _getIedById.Execute(newIed1.Id.GetValue());
            
            // Assert    
            Assert.That(ied.Id, Is.EqualTo(newIed1.Id.GetValue()));
        }

        [Test]
        public void Deve_Retornar_Instancia_Do_Fabricante()
        {
            // Arrange
            var ied = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);

            var iedDto = new IedDto
        {
            Id = ied.Id.GetValue(),
            Manufacturer = ied.Manufacturer,
            Model = ied.Model,
            Enabled = ied.Enabled,
            SubstationName = ied.SubstationName.GetValue(),
            BayName = ied.BayName.GetValue(),
            LocalIpAddress = ied.LocalIpAddress,
            ExternalIpAddress = ied.ExternalIpAddress,
            TcpPort = ied.TcpPort,
        };

            // Act
            var protocol = new DriverFactory(_logger, iedDto).Create("Siemens");

            // Assert
            Assert.IsInstanceOf<Siemens>(protocol);            
        }

        [Test]
        public async Task Deve_Tentar_Conectar_A_Um_Ied()
        {
            // Arrange
            var ied = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
            
            // Redirecionar a saída do Console para um StringWriter
            var originalOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            await _iedRepository.Save(ied);
            await _extractDisturbanceFromIed.Execute(ied.Id.GetValue());
            
            // Assert
            var result = stringWriter.ToString().Trim();
            Assert.That(result, Does.Contain($"TST21X1 - MMS - Connecting..."));

            Console.SetOut(originalOut);        
        }

        [Test]
        public async Task Deve_Tentar_Comunicar_Com_Varios_Ieds()
        {
            // Arrange
            var newIed1 = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
            var newIed2 = Ied.Create("Efacec", "L420", "TST", "21X2", "127.0.0.2", "127.0.0.2", 102, true);
            var newIed3 = Ied.Create("Siemens", "7SJ82", "TST", "21X3", "127.0.0.1", "127.0.0.1", 102, true);
            var newIed4 = Ied.Create("Sel", "751", "TST", "21X4", "127.0.0.1", "127.0.0.1", 102, true);

            // Act
            await _iedRepository.Save(newIed1);
            await _iedRepository.Save(newIed2);
            await _iedRepository.Save(newIed3);
            await _iedRepository.Save(newIed4);
           
            // Redirecionar a saída do Console para um StringWriter
            var originalOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var enabledIeds = await _getEnabledIeds.Execute();
            var collectTasks = enabledIeds.Select(ied => _extractDisturbanceFromIed.Execute(ied.Id)).ToArray();
            await Task.WhenAll(collectTasks);
            
            // Assert
            var result = stringWriter.ToString();

            Assert.That(result, Does.Contain("TST21X1 - MMS - Connecting..."));
            Assert.That(result, Does.Contain("TST21X2 - MMS - Connecting..."));
            Assert.That(result, Does.Contain("TST21X3 - MMS - Connecting..."));
            Assert.That(result, Does.Contain("TST21X4 - TELNET - Connecting..."));
            Console.SetOut(originalOut);
        }
    }
}