using API.Caixa.Domain.Repositories.Base;
using System.Collections.Generic;

namespace API.Caixa.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }

        // EF
        public User() { }

        public List<Entry> Entries { get; set; }
    }
}
