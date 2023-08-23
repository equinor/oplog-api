using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries
{
    public class OperationAreasQueries : IOperationAreasQueries
    {
        private readonly IOperationsAreasRepository _areasRepository;
        public OperationAreasQueries(IOperationsAreasRepository areasRepository)
        {
            _areasRepository = areasRepository;
        }
        public async Task<List<GetAllAreasResult>> GetAllAreas()
        {
            var areas = await _areasRepository.GetAllAreas();

            if (areas == null)
            {
                return null;
            }

            var results = new List<GetAllAreasResult>();

            foreach (var area in areas)
            {
                results.Add(new GetAllAreasResult(area.Id, area.Name, area.Description));
            }

            return results;
        }

        public async Task<List<GetAllAreasResult>> GetActiveAreas()
        {
            var areas = await _areasRepository.GetActiveAreas();

            if (areas == null)
            {
                return null;
            }

            var results = new List<GetAllAreasResult>();

            foreach (var area in areas)
            {
                var units = new List<UnitResult>();
                foreach (var unit in area.Units)
                {
                    units.Add(new UnitResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
                }

                results.Add(new GetAllAreasResult(area.Id, area.Name, area.Description, units));
            }

            return results;
        }
    }
}
