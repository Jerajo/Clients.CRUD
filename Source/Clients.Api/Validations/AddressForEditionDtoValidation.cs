using Clients.Application.DTOs;
using FluentValidation;

namespace Clients.Api.Validations
{
    public class AddressForEditionDtoValidation : AbstractValidator<AddressForEditionDto>
    {
        public AddressForEditionDtoValidation()
        {
            RuleFor(address => address.AddressLineOne)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250)
                .WithErrorCode("1005");

            RuleFor(address => address.AddressLineTwo)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250)
                .WithErrorCode("1006");

            RuleFor(address => address.City)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithErrorCode("1007");

            RuleFor(address => address.State)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithErrorCode("1008");

            RuleFor(address => address.Country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithErrorCode("1009");

            RuleFor(address => address.PostalCode)
                .NotNull()
                .InclusiveBetween(10000u, 99999u)
                .WithErrorCode("1010");
        }
    }
}
