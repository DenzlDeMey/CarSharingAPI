using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Reject;

public static class RejectBorrowingEndpoint
{
    public static RouteGroupBuilder MapRejectBorrowing(this RouteGroupBuilder group)
    {
        group.MapDelete("{id}/reject", async (int id, IMediator mediator) =>
        {
            var command = new RejectBorrowingCommand(id);
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: error => Results.BadRequest(error)
            );
        });

        return group;
    }
}