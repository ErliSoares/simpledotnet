using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project.Domain;
using FluentValidation;

namespace Sample.Project.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
        }
    }
}