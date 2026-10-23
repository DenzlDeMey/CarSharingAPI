using CarSharingAPI.Common;
using CarSharingAPI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarSharingAPI.Features.Borrowings.GetByType;

public class GetByTypeHandler : IRequestHandler<GetByTypeQuery, Result<List<BorrowingDto>>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetByTypeHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<BorrowingDto>>> Handle(GetByTypeQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var query = _context.Borrowings.Include(b => b.Car).AsQueryable();

        if (request.Type == "outgoing")
        {
            query = query.Where(b => b.BorrowerId == userId);
        }
        else if (request.Type == "incomming")
        {
            query = query.Where(b => b.Car!.OwnerId == userId);
        }
        else
        {
            return Result<List<BorrowingDto>>.Failure(
                new Error("Borrowing.InvalidType", "Type moet 'incomming' of 'outgoing' zijn."));
        }

        var borrowings = await query
            .Select(b => new BorrowingDto(
                b.Id,
                b.StartDateTime,
                b.EndDateTime,
                (int)b.Status,
                b.CarId,
                b.Message,
                b.BorrowerId
            ))
            .ToListAsync(cancellationToken);

        return Result<List<BorrowingDto>>.Succes(borrowings);
    }
}
