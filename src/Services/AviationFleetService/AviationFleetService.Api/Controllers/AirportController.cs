using AviationFleetService.Api.Abstractions;
using AviationFleetService.Api.Contracts.Airports;
using AviationFleetService.Application.Airports.Commands.CreateAirport;
using AviationFleetService.Application.Airports.Commands.DeleteAirport;
using AviationFleetService.Application.Airports.Commands.UpdateAirport;
using AviationFleetService.Application.Airports.Queries.GetAirportById;
using AviationFleetService.Application.Airports.Queries.ListAirports;
using AviationFleetService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviationFleetService.Api.Controllers
{
    [Route("api/airports")]
    [ApiController]
    public class AirportController(ISender sender) : ApiController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> ListAirports(CancellationToken cancellationToken)
        {
            var query = new ListAirportsQuery();
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                airports => Ok(airports.ConvertAll(ToDto)),
                HandleFailure);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAirportById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAirportByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                airport => Ok(ToDto(airport)),
                HandleFailure);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAirport([FromBody] RegisterAirportRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAirportCommand(
                request.Name,
                request.Country,
                request.City,
                request.IsInternational);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
               airport => CreatedAtAction(
                    nameof(GetAirportById),
                    new { airport.Id },
                    Ok(airport)),
               HandleFailure);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAirport(Guid id, UpdateAirportRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateAirportCommand(
                id,
                request.Name,
                request.Country,
                request.City,
                request.IsInternational);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                 _ => NoContent(),
                 HandleFailure);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAirport(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteAirportCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        private static AirportResponse ToDto(Airport airport) => new(
           airport.Id,
           airport.Name,
           airport.Country,
           airport.City,
           airport.IsInternational);
    }
}
