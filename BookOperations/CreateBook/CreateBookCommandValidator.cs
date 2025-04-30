using FluentValidation;
using MyApi.BookOperations.CreateBook;

namespace myApi.BookOperations.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {   
        //rule'u şunun için çalıştır
        //GenreId nin 0 dan büyük olmalı
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        //pagecount 0 dan büyük olmalı
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}
