using System;
using FluentValidation;
using MyApi.BookOperations.GetBookDetail;

namespace myApi.BookOperations.GetBookDetail;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }

}
