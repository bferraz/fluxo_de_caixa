using API.Caixa.Domain.Repositories.Base;
using System;
using System.Collections.Generic;

namespace API.Caixa.Domain.Entities
{
    public class Account : Entity, IAggregationRoot
    {
        public double Value { get; set; }
        public DateTime LastUpdate { get; set; }

        // EF
        public Account() { }
        public List<Entry> Entries { get; set; }
    }
}
