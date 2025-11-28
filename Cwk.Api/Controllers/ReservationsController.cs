using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cwk.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDetailsDto>> CreateReservation([FromBody] CreateReservationRequestDto request)
    {
        try
        {
            var reservation = await _reservationService.CreateReservationAsync(request);
            return Ok(reservation);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);

        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("availability/{spaceId}")]
    public async Task<ActionResult<SpaceAvailabilityDto>> CheckAvailability(int spaceId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        var availability = await _reservationService.CheckSpaceAvailabilityAsync(spaceId, startTime, endTime);
        return Ok(availability);
    }

    [HttpGet]
    public async Task<ActionResult<ReservationResponseDto>> GetReservations([FromQuery] ReservationQueryDto query)
    {
        var reservations = await _reservationService.GetReservationsAsync(query);
        return Ok(reservations);
    }

    [HttpPut("{id}/confirm")]
    public async Task<ActionResult> ConfirmReservation(int id)
    {
        var result = await _reservationService.ConfirmReservationAsync(id);
        return result ? Ok() : NotFound();

    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult>CancelReservation(int id) {
        var result = await _reservationService.CancelReservationAsync(id);
        return result ? Ok() : NotFound();
    }

}
