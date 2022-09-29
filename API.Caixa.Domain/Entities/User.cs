using API.Caixa.Domain.Repositories.Base;

namespace API.Caixa.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
}
