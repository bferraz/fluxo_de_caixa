using API.Caixa.Domain.Repositories.Base;
using System;
using System.Threading.Tasks.Sources;

namespace API.Caixa.Domain.Entities
{
    public class Account : Entity
    {
        public double Value { get; set; }
        public DateTime LastUpdate { get; set; }

        // EF
        public Account() { }
    }
}
