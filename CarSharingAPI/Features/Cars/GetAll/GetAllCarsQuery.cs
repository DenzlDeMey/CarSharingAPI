using CarSharingAPI.Common;
using MediatR;

namespace CarSharingAPI.Features.Cars.GetAll;

public record GetAllCarsQuery() : IRequest<Result<List<CarDto>>>;
public record CarDto(
    int Id,
    string Brand,
    string Model,
    string Description,
    int Year,
    int Seats,
    int FuelType,
    int Transmission,
    string OwnerId
 );


