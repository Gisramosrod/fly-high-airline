using AviationFleetService.Api.Abstractions;
using AviationFleetService.Api.Contracts.Planes;
using AviationFleetService.Api.Contracts.PlaneServices;
using AviationFleetService.Api.Contracts.Seats;
using AviationFleetService.Api.Contracts.Seats.Enums;
using AviationFleetService.Application.Planes.Commands.AddPlaneServiceToPlane;
using AviationFleetService.Application.Planes.Commands.CreatePlane;
using AviationFleetService.Application.Planes.Commands.DeletePlane;
using AviationFleetService.Application.Planes.Commands.RemovePlaneServiceFromPlane;
using AviationFleetService.Application.Planes.Commands.UpdatePlane;
using AviationFleetService.Application.Planes.Queries.GetPlaneById;
using AviationFleetService.Application.Planes.Queries.ListPlanes;
using AviationFleetService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviationFleetService.Api.Controllers
{
    [Route("api/planes")]
    [ApiController]
    public class PlaneController(ISender sender) : ApiController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> ListPlanes(CancellationToken cancellationToken)
        {
            var query = new ListPlanesQuery();
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                planes => Ok(planes.ConvertAll(ToDto)),
                HandleFailure);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlaneById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPlaneByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                plane => Ok(ToDto(plane)),
                HandleFailure);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPlane([FromBody] RegisterPlaneRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePlaneCommand(
                request.Number,
                request.Model,
                request.Manufacturer);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
               plane => CreatedAtAction(
                    nameof(GetPlaneById),
                    new { plane.Id },
                    Ok(plane)),
               HandleFailure);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlane(Guid id, [FromBody] UpdatePlaneRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdatePlaneCommand(
                id,
                request.Number,
                request.Model,
                request.Manufacturer);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                 _ => NoContent(),
                 HandleFailure);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlane(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeletePlaneCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpPost("planeServices/{planeServiceId:guid}")]
        public async Task<IActionResult> AddPlaneServiceToPlane(Guid id,Guid planeServiceId, CancellationToken cancellationToken)
        {
            var command = new AddPlaneServiceToPlaneCommand(id, planeServiceId);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpDelete("planeServices/{planeServiceId:guid}")]
        public async Task<IActionResult> RemovePlaneServiceFromPlane(Guid id, Guid planeServiceId, CancellationToken cancellationToken)
        {
            var command = new RemovePlaneServiceFromPlaneCommand(id, planeServiceId);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        private static PlaneResponse ToDto(Plane plane) => new(
            plane.Id,
            plane.Number,
            plane.Model,
            plane.Seats.ToList()
            .Select(x => new SeatResponse(
                x.Id, x.SeatNumber,
                EnumConverter.ToDtoSeatClass(x.SeatClass),
                EnumConverter.ToDtoSeatType(x.SeatType)))
            .ToList(),
            plane.PlaneServices.ToList()
            .Select(x => new PlaneServiceReponse(
                x.Id,
                x.Name,
                x.Description)).ToList());
    }
}
