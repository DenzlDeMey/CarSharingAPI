using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Reject;

public record RejectBorrowingCommand(int Id) : IRequest<Result>;