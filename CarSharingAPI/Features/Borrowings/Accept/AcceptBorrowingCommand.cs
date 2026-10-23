using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Borrowings.Accept;

public record AcceptBorrowingCommand(int Id) : IRequest<Result>;
