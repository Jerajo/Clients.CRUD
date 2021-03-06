using System;
using Clients.Application.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Clients.Api.Validations
{
    public class ClientForEditionDtoValidation : AbstractValidator<ClientForEditionDto>
    {
        public ClientForEditionDtoValidation(IHttpContextAccessor context)
        {
            RuleFor(client => client.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150)
                .WithErrorCode("1001");

            RuleFor(client => client.BirthDay)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(client => client.MarriageStatus)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Equals('S') || x.Equals('M'))
                .WithErrorCode("1004");
        }
    }
}
