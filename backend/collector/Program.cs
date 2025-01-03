using collector;
using collector.application.usecase.ied;
using collector.domain.driver;
using collector.domain.gateway;
using collector.domain.logger;
using collector.domain.repository;
using collector.infra.driver;
using collector.infra.gateway;
using collector.infra.logger;
using collector.infra.repository;
using Microsoft.AspNetCore.Builder;

//var builder = Host.CreateApplicationBuilder(args);
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddSingleton<ILog, ConsoleLog>();
builder.Services.AddTransient<IGatewayFactory, GatewayFactory>();
builder.Services.AddTransient<IDriverFactory, DriverFactory>();
builder.Services.AddSingleton<IIedRepository, IedRepositoryMemory>();
builder.Services.AddTransient<GetEnabledIeds>();
builder.Services.AddTransient<ExtractDisturbanceFromIed>();
builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();

// Adiciona o suporte ao appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var host = builder.Build();

/*
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) host.UseSystemd();
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) host.RunWindowsService();
else host.Run();
*/

host.MapControllers();

host.Run("http://localhost:3000");
