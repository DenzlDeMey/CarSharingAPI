using CarSharingAPI.Common;
using CarSharingAPI.Data;
using CarSharingAPI.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CarSharingAPI.Features.Borrowings.Create;

public class CreateBorrowingHandler : IRequestHandler<CreateBorrowingCommand, Result<int>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBorrowingHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<int>> Handle(CreateBorrowingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var car = await _context.Cars.FindAsync(request.CarId);
        if (car == null)
            return Result<int>.Failure(new Error("Car.NotFound", "Auto niet gevonden."));

        if (car.OwnerId == userId)
            return Result<int>.Failure(new Error("Borrowing.OwnCar", "Je kan je eigen auto niet ontlenen."));

        var borrowing = new Borrowing
        {
            CarId = request.CarId,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Message = request.Message,
            BorrowerId = userId!,
            Status = BorrowingStatus.Pending
        };

        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Succes(borrowing.Id);
    }
}