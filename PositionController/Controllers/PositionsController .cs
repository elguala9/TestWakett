using Microsoft.AspNetCore.Mvc;
using Positions.Core.DTO;
using Positions.Core.Interfaces;
using Positions.Infrastracture.Models.Positions;


[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IPositionsService _positionsService;

    public PositionsController(IPositionsService positionsService)
    {
        _positionsService = positionsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var positions = await _positionsService.GetAllAsync();
        return Ok(positions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pos = await _positionsService.GetByIdAsync(id);
        if (pos == null)
            return NotFound();

        return Ok(pos);
    }

    [HttpPost]
    public async Task<IActionResult> AddPosition([FromBody] PositionCreateRequest request)
    {
        Position position = PositionCreateRequest.ToPosition(request);
        var result = await _positionsService.CreateAsync(position);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Close(Guid id)
    {
        var success = await _positionsService.CloseAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpPost("rate-changed")]
    public async Task<IActionResult> OnRateChanged([FromBody] RateChangeNotification notification)
    {
        await _positionsService.RecalculatePositionsAsync(notification);
        return Ok("Positions recalculated.");
    }
}
