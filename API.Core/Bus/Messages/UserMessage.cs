using Core.Utils;
using FluentValidation;
using FluentValidation.Results;
using System;
namespace Core.Bus.Messages
{
    public class UserMessage : Message
    {
        public UserMessage(Guid id, string email, string cpf, string name)
        {
            Id = id;
            Email = email;
            Cpf = cpf;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new RegisterUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class RegisterUserValidation : AbstractValidator<UserMessage>
        {
            public RegisterUserValidation()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("O nome do usuário deve ser informado");

                RuleFor(x => x.Email)
                    .Must(IsValidEmail)
                    .WithMessage("O e-mail informado não é válido");

                RuleFor(x => x.Cpf)
                    .Must(IsValidCpf)
                    .WithMessage("O cpf informado não é válido");
            }

            private static bool IsValidCpf(string cpf)
            {
                return ValidationUtils.ValidarCpf(cpf);
            }

            private static bool IsValidEmail(string email)
            {
                return ValidationUtils.ValidarEmail(email);
            }
        }
    }
}
