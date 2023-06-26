using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class CreateCustomFilterCommandHandler : ICommandHandler<CreateCustomFilterCommand>
    {
        private readonly ICustomFilterRepository _customFilterRepository;
        public CreateCustomFilterCommandHandler(ICustomFilterRepository customFilterRepository)
        {
            _customFilterRepository = customFilterRepository;
        }
        public async Task Handle(CreateCustomFilterCommand command)
        {
            //Add validation
            var customFilterItems = new List<CustomFilterItem>();
            if (command.FilterItems.Any())
            {
                foreach (var item in command.FilterItems)
                {
                    customFilterItems.Add(new CustomFilterItem()
                    {
                        FilterId = item.FilterId,
                        CategoryId = item.CategoryId,
                    });
                }
            }

            var isGlobalFilter = command.IsGlobalFilter != null && command.IsGlobalFilter.Value;
            var customFilter = new CustomFilter { Name = command.Name, CreatedBy = command.CreatedBy, IsGlobalFilter = isGlobalFilter, SearchText = command.SearchText, CustomFilterItems = customFilterItems };
            await _customFilterRepository.Insert(customFilter);
            await _customFilterRepository.Save();
        }
    }
}
