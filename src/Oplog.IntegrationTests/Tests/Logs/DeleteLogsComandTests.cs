using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Enums;

namespace Oplog.IntegrationTests.Tests.Logs
{
    public class DeleteLogsComandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldDeleteAllLogs()
        {
            var createLogCommandA = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var createResultA = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommandA);

            var createLogCommandB = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var createResultB = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommandB);

            var ids = new List<int>
            {
                createResultA.LogId,
                createResultB.LogId
            };

            var deleteResult = await CommandDispatcher.Dispatch<DeleteLogsCommand, DeleteLogsResult>(new DeleteLogsCommand(ids));

            Assert.IsTrue(deleteResult.ResultType == ResultType.Success);
        }

        [Test]
        public async Task Dispatch_ShouldDeleteLogsWithSomeLogsNotFound()
        {
            var createLogCommandA = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var createResultA = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommandA);

            var ids = new List<int>
            {
                createResultA.LogId,
                10
            };

            var deleteResult = await CommandDispatcher.Dispatch<DeleteLogsCommand, DeleteLogsResult>(new DeleteLogsCommand(ids));

            Assert.IsTrue((deleteResult.ResultType == ResultType.Success) && deleteResult.LogsNotDeleted.Any());
        }
    }
}
