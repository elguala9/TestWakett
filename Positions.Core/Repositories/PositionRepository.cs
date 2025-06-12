using Positions.Infrastracture.Models.Positions;
using Microsoft.EntityFrameworkCore;

public class PositionRepository : IPositionRepository
{
    private readonly PositionsContext _context;

    public PositionRepository(PositionsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Position>> GetAllAsync()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(Guid id)
    {
        return await _context.Positions
            .Include(p => p.PositionValuations)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CloseAsync(Guid id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null) return false;

        position.Isclosed = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Position>> GetBySymbolAsync(string symbol)
    {
        return await _context.Positions
            .Where(p => p.Instrumentid == symbol && !p.Isclosed)
            .ToListAsync();
    }

    public async Task AddValuationAsync(PositionValuation valuation)
    {
        _context.PositionValuations.Add(valuation);
        await _context.SaveChangesAsync();
    }
}
