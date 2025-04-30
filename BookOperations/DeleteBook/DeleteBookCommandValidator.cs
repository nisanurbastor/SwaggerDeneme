using System;
using System.Data;
using FluentValidation;
using MyApi.BookOperations.DeleteBook;

namespace myApi.BookOperations.DeleteBook;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command=> command.BookId).GreaterThan(0);
    }
}
