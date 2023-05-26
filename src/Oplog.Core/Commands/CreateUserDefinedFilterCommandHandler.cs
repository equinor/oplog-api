using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class CreateUserDefinedFilterCommandHandler : ICommandHandler<CreateUserDefinedFilterCommand>
    {
        private readonly IUserDefinedFilterRepository _userDefinedFilterRepository;
        public CreateUserDefinedFilterCommandHandler(IUserDefinedFilterRepository userDefinedFilterRepository)
        {
            _userDefinedFilterRepository = userDefinedFilterRepository;
        }
        public async Task Handle(CreateUserDefinedFilterCommand command)
        {
            //Add validation
            var userDefinedFilterItems = new List<Oplog.Persistence.Models.UserDefinedFilterItem>();
            if (command.FilterItems.Any())
            {
                foreach (var item in command.FilterItems)
                {
                    userDefinedFilterItems.Add(new Persistence.Models.UserDefinedFilterItem()
                    {
                        FilterId = item.FilterId,
                        CategoryId = item.CategoryId,
                    });
                }
            }
            var userDefinedFilter = new UserDefinedFilter { Name = command.Name, CreatedBy = command.CreatedBy, UserDefinedFilterItems = userDefinedFilterItems };
            await _userDefinedFilterRepository.Insert(userDefinedFilter);
            await _userDefinedFilterRepository.Save();
        }
    }
}
