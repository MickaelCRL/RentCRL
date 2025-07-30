using System.ComponentModel;

namespace RentCRL.Infrastructure.Tests.Integration
{
    public class BaseTests
    {
        protected CosmosDbTestEnvironment _environment;
        protected CosmosDbService _service;

        public async Task BaseSetUp()
        {
            _environment = new CosmosDbTestEnvironment();
            _service = _environment.Service;
            await _service.EnsureDatabaseAndContainerExistAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _environment.DeleteAndDisposeAsync();
        }
    }
}
