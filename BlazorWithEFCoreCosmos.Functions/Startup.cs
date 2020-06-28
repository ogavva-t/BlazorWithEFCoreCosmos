



using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorWithEFCoreCosmos.Repository;
using BlazorWithEFCoreCosmos.Infrastructure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.IO;

[assembly: FunctionsStartup(typeof(BlazorWithEFCoreCosmos.Functions.Startup))]
namespace BlazorWithEFCoreCosmos.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton(s => new CosmosDbConnectionFactory(
               config["DbConnection:Cosmos:AccountEndpoint"],
               config["DbConnection:Cosmos:AccountKey"],
               config["DbConnection:Cosmos:DatabaseName"]
            ));

            builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();
        }
    }
}

