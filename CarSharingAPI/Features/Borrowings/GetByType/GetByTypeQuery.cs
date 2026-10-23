using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.GetByType;

public record GetByTypeQuery(string Type) : IRequest<Result<List<BorrowingDto>>>;

public record BorrowingDto(
    int Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    int Status,
    int CarId,
    string? Message,
    string BorrowerId
);
