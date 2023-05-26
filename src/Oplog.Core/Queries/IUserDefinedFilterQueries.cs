namespace Oplog.Core.Queries
{
    public interface IUserDefinedFilterQueries
    {
        Task<List<GetUserDefinedFiltersByCreatedUserResult>> GetByCreatedUser(string createdBy);
    }
}