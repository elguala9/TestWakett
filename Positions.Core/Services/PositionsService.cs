using Positions.Core.DTO;
using Positions.Infrastracture.Models.Positions;

public class PositionsService : IPositionsService
{
    private readonly IPositionRepository _repo;

    public PositionsService(IPositionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Position>> GetAllAsync() =>
        await _repo.GetAllAsync();

    public async Task<Position?> GetByIdAsync(Guid id) =>
        await _repo.GetByIdAsync(id);

    public async Task<Position> CreateAsync(Position request)
    {
        request.Id = Guid.NewGuid();
        request.Openedat = DateTime.UtcNow;
        request.Isclosed = false;
        await _repo.AddAsync(request);
        return request;
    }

    public async Task<bool> CloseAsync(Guid id) =>
        await _repo.CloseAsync(id);

    public async Task RecalculatePositionsAsync(RateChangeNotification rateChange)
    {
        var positions = await _repo.GetBySymbolAsync(rateChange.Symbol);

        foreach (var position in positions)
        {
            var pnl = position.Quantity * (rateChange.NewRate - position.Initialrate) * position.Side;
            var valuation = new PositionValuation
            {
                Id = Guid.NewGuid(),
                Positionid = position.Id,
                Currentrate = rateChange.NewRate,
                Profitloss = pnl,
                Calculatedat = DateTime.UtcNow
            };
            await _repo.AddValuationAsync(valuation);
        }
    }
}
