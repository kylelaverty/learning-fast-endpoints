namespace Learning.FastEndpoionts.Features.Book.GetBook;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("The Book Id must be greater than 0.");
    }
}