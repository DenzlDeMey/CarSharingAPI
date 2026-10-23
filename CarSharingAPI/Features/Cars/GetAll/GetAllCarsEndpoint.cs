using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Cars.GetAll;

public static class GetAllCarsEndpoint
{
    public static RouteGroupBuilder MapGetAllCars(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator) =>
        {
            var query = new GetAllCarsQuery();
            var result = await mediator.Send(query);

            return result.Match(
                onSuccess: cars => Results.Ok(cars),
                onFailure: error => Results.BadRequest(error)
            );
        });

        return group;
    }
}
