using CarSharingAPI.Common;
using CarSharingAPI.Data;
using CarSharingAPI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarSharingAPI.Features.Borrowings.Accept;

public class AcceptBorrowingHandler : IRequestHandler<AcceptBorrowingCommand, Result>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AcceptBorrowingHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(AcceptBorrowingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var borrowing = await _context.Borrowings
            .Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (borrowing == null)
            return Result.Failure(new Error("Borrowing.NotFound", "Aanvraag niet gevonden."));


        if (borrowing.Car!.OwnerId != userId)
            return Result.Failure(new Error("Borrowing.NotOwner", "Je bent niet de eigenaar van deze auto."));

        borrowing.Status = BorrowingStatus.Accepted;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
