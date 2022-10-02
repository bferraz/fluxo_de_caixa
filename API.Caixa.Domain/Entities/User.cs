using API.Caixa.Domain.Repositories.Base;
using Core.Utils;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace API.Caixa.Domain.Entities
{
    public class User : Entity, IAggregationRoot
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        // EF Ctor
        public User() { }

        public List<Entry> Entries { get; set; }        
    }
}
