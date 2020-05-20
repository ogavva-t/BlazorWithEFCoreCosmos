using Microsoft.EntityFrameworkCore;

namespace BlazorWithEFCoreCosmos.Infrastructure.Cosmos
{
    public interface IConnectionFactory
    {
        CosmosDbContext CreateConnection();
    }
    public class CosmosDbConnectionFactory : IConnectionFactory
    {
        readonly string _accountEndpoint;
        readonly string _accountKey;
        readonly string _databaseName;

        public CosmosDbConnectionFactory(string accountEndpoint, string accountKey, string databaseName)
        {
            _accountEndpoint = accountEndpoint;
            _accountKey = accountKey;
            _databaseName = databaseName;
        }
        public CosmosDbContext CreateConnection()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CosmosDbContext>();
            optionsBuilder.UseCosmos(
                    accountEndpoint: _accountEndpoint,
                    accountKey: _accountKey,
                    databaseName: _databaseName
            );

            return new CosmosDbContext(optionsBuilder.Options);
        }
    }

}

