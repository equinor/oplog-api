using NUnit.Framework;
using Oplog.Core.Commands.LogTemplates;
using Oplog.Core.Enums;

namespace Oplog.IntegrationTests.Tests.LogTemplates
{
    public class UpdateLogTemplateCommandTests : TestBase
    {
        [Test]
        public async Task Dispatch_ShouldUpdateLog()
        {
            var createLogTemplateCommand = new CreateLogTemplateCommand
                                                    ("Test template", null, null, null, "test template", null, null, null, "bonm@equinor.com");
            var result = await CommandDispatcher.Dispatch<CreateLogTemplateCommand, CreateLogTemplateResult>(createLogTemplateCommand);

            var updateLogTemplateCommand = new UpdateLogTemplateCommand
                                                    (result.LogTemplateId, "Test template", null, null, null, "test template", null, null, null, "bonm@equinor.com");

            var updateResult = await CommandDispatcher.Dispatch<UpdateLogTemplateCommand, UpdateLogTemplateResult>(updateLogTemplateCommand);
            Assert.IsTrue(updateResult.ResultType == ResultType.Success);
        }

        [Test]
        public async Task Dispatch_ShouldReturnLogNotFound()
        {
            var updateLogTemplateCommand = new UpdateLogTemplateCommand
                                                   (id: 5, "Test template", null, null, null, "test template", null, null, null, "bonm@equinor.com");

            var updateResult = await CommandDispatcher.Dispatch<UpdateLogTemplateCommand, UpdateLogTemplateResult>(updateLogTemplateCommand);
            Assert.IsTrue(updateResult.ResultType == ResultType.NotFound);
        }
    }
}
