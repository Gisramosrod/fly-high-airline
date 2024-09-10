using AviationFleetService.Api.Abstractions;
using AviationFleetService.Api.Contracts.Seats;
using AviationFleetService.Api.Contracts.Seats.Enums;
using AviationFleetService.Application.Seats.Commands.CreateSeat;
using AviationFleetService.Application.Seats.Commands.DeleteSeat;
using AviationFleetService.Application.Seats.Commands.UpdateSeat;
using AviationFleetService.Application.Seats.Queries.GetSeatById;
using AviationFleetService.Application.Seats.Queries.ListSeats;
using AviationFleetService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviationFleetService.Api.Controllers
{
    [Route("api/planes/{planeId:guid}/seats")]
    [ApiController]
    public class SeatController(ISender sender) : ApiController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> ListSeats(Guid planeId, CancellationToken cancellationToken)
        {
            var query = new ListSeatsQuery(planeId);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                seats => Ok(seats.ConvertAll(ToDto)),
                HandleFailure);
        }

        [HttpGet("{seatId:guid}")]
        public async Task<IActionResult> GetSeatById(Guid planeId, Guid seatId, CancellationToken cancellationToken)
        {
            var query = new GetSeatByIdQuery(planeId, seatId);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                seat => Ok(ToDto(seat)),
                HandleFailure);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSeat(Guid planeId, [FromBody] RegisterSeatRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateSeatCommand(
                planeId,
                EnumConverter.ToDomainSeatClass(request.SeatClass),
                EnumConverter.ToDomainSeatType(request.SeatType),
                request.SeatNumber);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
               seat => CreatedAtAction(
                    nameof(GetSeatById),
                    new { PlaneId = planeId, SeatId = seat.Id },
                    Ok(seat)),
               HandleFailure);
        }

        [HttpPut("{seatId:guid}")]
        public async Task<IActionResult> UpdateSeat(Guid planeId, Guid seatId, [FromBody] UpdateSeatRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateSeatCommand(
                planeId,
                seatId,
                EnumConverter.ToDomainSeatClass(request.SeatClass),
                EnumConverter.ToDomainSeatType(request.SeatType),
                request.SeatNumber);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                 _ => NoContent(),
                 HandleFailure);
        }

        [HttpDelete("{seatId:guid}")]
        public async Task<IActionResult> DeleteSeat(Guid planeId, Guid seatId, CancellationToken cancellationToken)
        {
            var command = new DeleteSeatCommand(planeId, seatId);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        private static SeatResponse ToDto(Seat seat) => new(
            seat.Id,
            seat.SeatNumber,
            EnumConverter.ToDtoSeatClass(seat.SeatClass),
            EnumConverter.ToDtoSeatType(seat.SeatType));
    }
}
