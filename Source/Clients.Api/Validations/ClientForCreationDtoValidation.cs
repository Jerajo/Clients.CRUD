using System;

using FluentValidation;

using Clients.Core.Contracts;
using Clients.Core.Entities;
using Clients.Application.DTOs;

namespace Clients.Api.Validations
{
    public class ClientForCreationDtoValidation : AbstractValidator<ClientForCreationDto>
    {
        public ClientForCreationDtoValidation(IRepository<Client> repository)
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

            RuleFor(client => client.UserName)
                .Must(userName => !repository.Any(c => c.UserName == userName))
                .WithErrorCode("1012");

            RuleFor(client => client.Email)
                .Must(email => !repository.Any(c => c.Email == email))
                .WithErrorCode("1013");
        }
    }
}
