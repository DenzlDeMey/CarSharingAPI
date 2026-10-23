using CarSharingAPI.Common;
using CarSharingAPI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarSharingAPI.Features.Cars.GetAll;

public class GetAllCarsHandler : IRequestHandler<GetAllCarsQuery, Result<List<CarDto>>>
{
    private readonly AppDbContext _context;

    public GetAllCarsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CarDto>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _context.Cars
            .Select(c => new CarDto(
                c.Id,
                c.Brand,
                c.Model,
                c.Description,
                c.Year,
                c.Seats,
                (int)c.FuelType,
                (int)c.Transmission,
                c.OwnerId
            ))
            .ToListAsync(cancellationToken);

        return Result<List<CarDto>>.Succes(cars);
    }
}
