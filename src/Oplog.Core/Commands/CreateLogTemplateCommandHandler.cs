﻿using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class CreateLogTemplateCommandHandler : ICommandHandler<CreateLogTemplateCommand, CreateLogTemplateResult>
    {
        private readonly ILogTemplateRepository _templateRepository;
        public CreateLogTemplateCommandHandler(ILogTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }
        public async Task<CreateLogTemplateResult> Handle(CreateLogTemplateCommand command)
        {
            var result = new CreateLogTemplateResult();
            var newLogTemplate = new LogTemplate
            {
                Name = command.Name,
                LogTypeId = command.LogTypeId,
                OperationAreaId = command.OperationAreaId,
                Text = command.Text,
                Author = command.Author,
                Unit = command.Unit,
                Subtype = command.Subtype,
                IsCritical = command.IsCritical,
                CreatedBy = command.CreatedBy,
                CreatedDate = DateTime.Now
            };

            await _templateRepository.Insert(newLogTemplate);
            await _templateRepository.Save();
            return result.LogTemplateCreated();
        }
    }
}
