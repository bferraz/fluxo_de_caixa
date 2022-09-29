using API.Caixa.Domain.Repositories.Base;
using System;

namespace API.Caixa.Domain.Entities
{
    public class Account : Entity
    {
        public double Value { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
