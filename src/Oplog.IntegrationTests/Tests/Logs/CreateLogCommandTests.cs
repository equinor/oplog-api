using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Enums;

namespace Oplog.IntegrationTests.Tests.Logs
{
    public class CreateLogCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldCreateLog()
        {
            var createLogCommand = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "Donald Trump", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var result = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);
            Assert.IsTrue(result.ResultType == ResultType.Success);
        }

        [Test]
        public async Task Dispatch_ShouldUpdateLog()
        {
            var createLogCommand = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var createResult = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);

            var updateCommand = new UpdateLogCommand(createResult.LogId, logType: 415, subType: 1079, comment: "Update comment", operationsArea: 10000, author: "Donald Trump", unit: 1086, effectiveTime: DateTime.Now, updatedBy: "bonm@equinor.com", isCritical: false);
            var updateResult = await CommandDispatcher.Dispatch<UpdateLogCommand, UpdateLogResult>(updateCommand);

            Assert.IsTrue(updateResult.ResultType == ResultType.Success);
        }
    }
}
