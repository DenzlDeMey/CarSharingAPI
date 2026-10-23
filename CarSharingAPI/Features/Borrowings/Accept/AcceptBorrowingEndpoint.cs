using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Accept;

public static class AcceptBorrowingEndpoint
{
    public static RouteGroupBuilder MapAcceptBorrowing(this RouteGroupBuilder group)
    {
        group.MapPut("{id}/accept", async (int id, IMediator mediator) =>
        {
            var command = new AcceptBorrowingCommand(id);
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: error => Results.BadRequest(error)
            );
        });

        return group;
    }
}