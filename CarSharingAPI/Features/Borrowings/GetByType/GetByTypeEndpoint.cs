using CarSharingAPI.Common;
using MediatR;


namespace CarSharingAPI.Features.Borrowings.GetByType;

public static class GetByTypeEndpoint
{
    public static RouteGroupBuilder MapGetByType(this RouteGroupBuilder group)
    {
        group.MapGet("{type}", async (string type, IMediator mediator) =>
        {
            var query = new GetByTypeQuery(type);
            var result = await mediator.Send(query);

            return result.Match(
                onSuccess: borrowings => Results.Ok(borrowings),
                onFailure: error => Results.BadRequest(error)
            );
        });

        return group;
    }
}