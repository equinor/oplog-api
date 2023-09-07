using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.Logs
{
    public class CreateLogCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldCreateLog()
        {
            var createLogCommand = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "Donald Trump", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var result = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);
           
            Assert.IsTrue(result.ResultType == ResultTypeConstants.Success);
        }       
    }
}
