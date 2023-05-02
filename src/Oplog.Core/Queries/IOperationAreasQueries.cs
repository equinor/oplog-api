namespace Oplog.Core.Queries
{
    public interface IOperationAreasQueries
    {
        Task<List<GetAllAreasResult>> GetAllAreas();
    }
}
