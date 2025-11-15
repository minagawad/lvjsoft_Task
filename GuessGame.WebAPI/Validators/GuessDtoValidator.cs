using FluentValidation;

namespace GuessGame.WebAPI.Validators;

public class GuessDtoValidator : AbstractValidator<GuessDto>
{
    public GuessDtoValidator()
    {
        RuleFor(x => x.Guess).InclusiveBetween(1, 100);
    }
}