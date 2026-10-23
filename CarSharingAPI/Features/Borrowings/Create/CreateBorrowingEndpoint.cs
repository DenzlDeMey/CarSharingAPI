using CarSharingAPI.Common;
using FluentValidation;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Create;

public static class CreateBorrowingEndpoint
{
    public static RouteGroupBuilder MapCreateBorrowing(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
            CreateBorrowingCommand command,
            IValidator<CreateBorrowingCommand> validator,
            IMediator mediator) =>
        {
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: id => Results.Ok(id),
                onFailure: error => Results.BadRequest(error)
            );
        });

        return group;
    }
}
