using System;
using Clients.Application.DTOs;
using FluentValidation;

namespace Clients.Api.Validations
{
    public class ClientDtoValidation : AbstractValidator<ClientDto>
    {
        public ClientDtoValidation()
        {
            RuleFor(client => client.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150)
                .WithErrorCode("1001");

            RuleFor(client => client.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(35)
                .WithErrorCode("1002");

            RuleFor(client => client.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150)
                .WithErrorCode("1003");

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
