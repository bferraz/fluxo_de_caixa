using System;
namespace Core.Bus.Messages
{
    public class UserMessage : Message
    {
        public UserMessage(Guid id, string email, string cpf)
        {
            Id = id;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }
}
