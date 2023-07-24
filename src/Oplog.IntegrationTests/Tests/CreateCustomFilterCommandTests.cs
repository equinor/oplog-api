using NUnit.Framework;
using Oplog.Core.Commands;

namespace Oplog.IntegrationTests.Tests
{
    public class CreateCustomFilterCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldCreateCustomFilter()
        {
            var filterItems = new List<CreateCustomFilterItem>
            {
                new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
                new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
            };

            var createCustomFilterCommad = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", false, "Test", false, filterItems);

            var result = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommad);
            Assert.IsTrue(result.ResultType == ResultType.Success);
        }

        //TODO: tests for global and admin filters
    }
}
