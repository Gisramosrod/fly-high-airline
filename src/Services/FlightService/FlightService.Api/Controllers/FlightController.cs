using FlightService.Api.Abstractions;
using FlightService.Api.Contracts.FlightDetails;
using FlightService.Api.Contracts.Flights;
using FlightService.Api.Contracts.FlightsStatus;
using FlightService.Application.FlightDetails.Commands.UpdateFlightDetail;
using FlightService.Application.Flights.Commands.CreateFlight;
using FlightService.Application.Flights.Commands.DeleteFlight;
using FlightService.Application.Flights.Commands.UpdateFlightPlane;
using FlightService.Application.Flights.Commands.UpdateFlightStatus;
using FlightService.Application.Flights.Queries.GetFlightById;
using FlightService.Application.Flights.Queries.ListFlights;
using FlightService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using DomainFlightDetailType = FlightService.Domain.Enums.FlightDetailType;
using FlightStatus = FlightService.Api.Contracts.FlightsStatus.FlightStatus;

namespace FlightService.Api.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightController(ISender sender) : ApiController(sender) {

        [HttpGet]
        public async Task<IActionResult> ListFlights(CancellationToken cancellationToken) {

            var query = new ListFlightsQuery();
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                flights => Ok(flights.ConvertAll(ToDto)),
                HandleFailure);
        }

        [HttpGet("{flightId:guid}")]
        public async Task<IActionResult> GetFlightById(Guid flightId, CancellationToken cancellationToken) {

            var query = new GetFlightByIdQuery(flightId);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                flight => Ok(ToDto(flight)),
                HandleFailure);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFlight([FromBody] RegisterFlightRequest request, CancellationToken cancellationToken) {

            var command = new CreateFlightCommand(
                 request.DepartureScheduleDate,
                 request.DepatureAirportId,
                 request.DepartureTerminal,
                 request.DepartureGate,
                 request.ArrivalScheduleDate,
                 request.ArrivalAirportId,
                 request.ArrivalTerminal,
                 request.ArrivalGate,
                 request.PlaneId);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
               flight => CreatedAtAction(
                    nameof(GetFlightById),
                    new { flight.Id },
                    Ok(flight)),
               HandleFailure);
        }

        [HttpDelete("{flightId:guid}")]
        public async Task<IActionResult> DeleteFlight(Guid flightId, CancellationToken cancellationToken) {

            var command = new DeleteFlightCommand(flightId);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpPut("{flightId:guid}/status")]
        public async Task<IActionResult> UpdateFlightStatus(Guid flightId, [FromBody] FlightStatus status, CancellationToken cancellationToken) {

            var domainFlightStatus = FlightStatusEnumConverter.ToDomainFlightStatus(status);
            var command = new UpdateFlightStatusCommand(flightId, domainFlightStatus);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpPut("{flightId:guid}/plane")]
        public async Task<IActionResult> UpdateFlightPlane(Guid flightId, Guid planeId, CancellationToken cancellationToken) {

            var command = new UpdateFlightPlaneCommand(flightId, planeId);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpPut("{flightId:guid}/departure-detail")]
        public async Task<IActionResult> UpdateFlightDepartureDetail(Guid flightId, [FromBody] UpdateFlightDetailRequest request, CancellationToken cancellationToken) {

            var command = new UpdateFlightDetailCommand(
                flightId,
                DomainFlightDetailType.Departure,
                request.ScheduleDate,
                request.ActualDate,
                request.AirportId);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        [HttpPut("{flightId:guid}/arrival-detail")]
        public async Task<IActionResult> UpdateFlightArrivalDetail(Guid flightId, [FromBody] UpdateFlightDetailRequest request, CancellationToken cancellationToken) {

            var command = new UpdateFlightDetailCommand(
             flightId,
             DomainFlightDetailType.Arrival,
             request.ScheduleDate,
             request.ActualDate,
             request.AirportId);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        private static FlightResponse ToDto(Flight flight) => new(
            flight.Id,
            flight.Number.Value,
            FlightStatusEnumConverter.ToDtoFlightStatus(flight.Status),
            flight.Duration,
            flight.Plane.Id,
            flight.DepartureDetail.Id,
            flight.ArrivalDetail.Id);
    }
}
