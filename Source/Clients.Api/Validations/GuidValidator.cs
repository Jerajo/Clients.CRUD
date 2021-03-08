using System;

using FluentValidation;

namespace Clients.Api.Validations
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(guid => guid)
                .NotEqual(Guid.Empty);
        }
    }
}
