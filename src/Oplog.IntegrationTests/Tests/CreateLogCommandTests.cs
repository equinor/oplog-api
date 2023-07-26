﻿using NUnit.Framework;
using Oplog.Core.Commands;

namespace Oplog.IntegrationTests.Tests
{
    public class CreateLogCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldCreateLog()
        {
            var createLogCommand = new CreateLogCommand(logType: 415, subType: 1079, comment: "Test comment", operationsArea: 10000, author: "", unit: 1086, effectiveTime: DateTime.Now, createdBy: "bonm@equinor.com", isCritical: false);
            var result = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);
            Assert.IsTrue(result.ResultType == ResultType.Success);
        }
    }
}