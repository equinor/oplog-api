using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class LogTemplateQueries : ILogTemplateQueries
    {
        private readonly ILogTemplateRepository _logTemplateRepository;
        public LogTemplateQueries(ILogTemplateRepository logTemplateRepository)
        {
            _logTemplateRepository = logTemplateRepository;
        }

        public async Task<List<GetAllLogTemplateResult>> GetAll()
        {
            var templates = await _logTemplateRepository.GetAll();

            if (templates == null)
            {
                return null;
            }

            var results = new List<GetAllLogTemplateResult>();
            foreach (var template in templates)
            {
                results.Add(new
                    GetAllLogTemplateResult(template.Id, template.Name, template.LogTypeId, template.OperationAreaId, template.Text, template.Author, template.Unit, template.Subtype, template.IsCritical));
            }

            return results;
        }
    }
}
