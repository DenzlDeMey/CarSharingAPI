using FluentValidation;

namespace CarSharingAPI.Features.Borrowings.Create;

public class CreateBorrowingValidator : AbstractValidator<CreateBorrowingCommand>
{
    public CreateBorrowingValidator()
    {
        RuleFor(x => x.CarId)
            .GreaterThan(0)
            .WithMessage("Een geldig voertuig is verplicht.");

        RuleFor(x => x.StartDateTime)
            .GreaterThan(DateTime.Now)
            .WithMessage("Startdatum moet na vandaag vallen.");

        RuleFor(x => x.EndDateTime)
            .GreaterThan(x => x.StartDateTime)
            .WithMessage("Einddatum moet na de startdatum vallen.");
    }
}
