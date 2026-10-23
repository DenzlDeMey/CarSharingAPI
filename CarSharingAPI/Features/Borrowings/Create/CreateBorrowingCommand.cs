using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Create;

public record CreateBorrowingCommand : IRequest<Result<int>>
{
    public int CarId { get; init; }
    public DateTime StartDateTime { get; init; }
    public DateTime EndDateTime { get; init; }
    public string? Message { get; init; }
}
