using collector.application.usecase.ied;
using collector.domain.entity;
using collector.domain.logger;
using collector.domain.repository;

namespace collector;

public class Worker : BackgroundService
{
    private readonly ILog _logger;
    private readonly GetEnabledIeds _getEnabledIeds;
    private readonly IIedRepository _iedRepository;
    private readonly ExtractDisturbanceFromIed _extractDisturbanceFromIed;
    
    public Worker(ILog logger, IIedRepository iedRepository, GetEnabledIeds getEnabledIeds, ExtractDisturbanceFromIed extractFromIed)
    {
        _logger = logger;
        _iedRepository = iedRepository;
        _getEnabledIeds = getEnabledIeds;
        _extractDisturbanceFromIed = extractFromIed;

        var newIed1 = Ied.Create("Abb", "REF-615", "TST", "21X1", "127.0.0.1", "127.0.0.1", 102, true);
        var newIed2 = Ied.Create("Sel", "L420", "TST", "21X2", "127.0.0.2", "127.0.0.2", 102, true);
        var newIed3 = Ied.Create("Siemens", "7SJ82", "TST", "21X3", "127.0.0.1", "127.0.0.1", 102, true);
        var newIed4 = Ied.Create("Ziv", "7SJ82", "TST", "21X4", "127.0.0.1", "127.0.0.1", 102, true);
        _iedRepository.Save(newIed1);
        _iedRepository.Save(newIed2);
        _iedRepository.Save(newIed3);
        _iedRepository.Save(newIed4);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            
            _logger.Log(LogLevel.Information, null, "Starting collector...");
            
            try 
            {
                // Obtém os IEDs habilitados
                var enabledIeds = await _getEnabledIeds.Execute();

                // Executa a coleta de distúrbios para cada IED habilitado em paralelo
                var collectTasks = enabledIeds.Select(ied => Task.Run(async () => 
                {
                    try 
                    {
                        await _extractDisturbanceFromIed.Execute(ied.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, null, ex.Message);
                    }
                    
                })).ToArray();

                // Aguarda todas as tarefas de coleta de distúrbios serem concluídas
                await Task.WhenAll(collectTasks);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, null, $"Worker.ExecuteAsync() - {ex.Message}");
            }

            _logger.Log(LogLevel.Information, null, "Collector finished");

            // Aguarda 30 minutos para um novo ciclo
            await Task.Delay(5000, stoppingToken);
        }
    }
}