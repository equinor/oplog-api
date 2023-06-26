using Microsoft.AspNetCore.Http;
using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class DeleteCustomFilterCommandHandler : ICommandHandler<DeleteCustomFilterCommand, DeleteCustomFilterResult>
    {
        private readonly ICustomFilterRepository _customFilterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCustomFilterCommandHandler(ICustomFilterRepository customFilterRepository, IHttpContextAccessor httpContextAccessor)
        {
            _customFilterRepository = customFilterRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeleteCustomFilterResult> Handle(DeleteCustomFilterCommand command)
        {
            var customFilter = await _customFilterRepository.GetById(command.FilterId);

            var result = new DeleteCustomFilterResult();
            if (customFilter == null)
            {
                return result.CustomFilterNotFound();
            }

            var user = _httpContextAccessor.HttpContext?.User;
            var isAdmin = user?.Claims.Any(c => c.Value == "Permission.Admin") ?? false;

            if (customFilter.IsGlobalFilter && !isAdmin)
            {
                return result.GlobalFilterDeleteNotAllowed();
            }

            _customFilterRepository.Delete(customFilter);
            await _customFilterRepository.Save();
            return result.CustomFilterDeleted();
        }
    }
}