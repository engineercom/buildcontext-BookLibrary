using BookLibrary.Application.Dtos.BookDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Validations;

public class CreateBookValidator:AbstractValidator<CreateBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title not null")
            .MinimumLength(2).WithMessage("Title is min. 2 characters"); ;
    }
}
