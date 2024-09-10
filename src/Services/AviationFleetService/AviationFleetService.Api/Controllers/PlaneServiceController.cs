using AviationFleetService.Api.Abstractions;
using AviationFleetService.Api.Contracts.PlaneServices;
using AviationFleetService.Application.PlaneServices.Commands.CreatePlaneService;
using AviationFleetService.Application.PlaneServices.Commands.DeletePlaneService;
using AviationFleetService.Application.PlaneServices.Commands.UpdatePlaneService;
using AviationFleetService.Application.PlaneServices.Queries.GetPlaneServiceById;
using AviationFleetService.Application.PlaneServices.Queries.ListPlaneServices;
using AviationFleetService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviationFleetService.Api.Controllers
{
    [Route("api/planeServices")]
    [ApiController]
    public class PlaneServiceController(ISender sender) : ApiController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> ListPlaneServices(CancellationToken cancellationToken)
        {
            var query = new ListPlaneServicesQuery();
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                planeServices => Ok(planeServices.ConvertAll(ToDto)),
                HandleFailure);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlaneServiceById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPlaneServiceByIdQuery(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                planeService => Ok(ToDto(planeService)),
                HandleFailure);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPlaneService([FromBody] RegisterPlaneServiceRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePlaneServiceCommand(request.Name, request.Description);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
               planeService => CreatedAtAction(
                    nameof(GetPlaneServiceById),
                    new { planeService.Id },
                    Ok(planeService)),
               HandleFailure);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlaneService(Guid id, [FromBody] UpdatePlaneServiceRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdatePlaneServiceCommand(id, request.Name, request.Description);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                 _ => NoContent(),
                 HandleFailure);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlaneService(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeletePlaneServiceCommand(id);
            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                HandleFailure);
        }

        private static PlaneServiceReponse ToDto(PlaneService planeservice) => new(
            planeservice.Id,
            planeservice.Name,
            planeservice.Description);
    }
}
