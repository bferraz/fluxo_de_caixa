using API.Caixa.Domain.Enums;
using API.Caixa.Domain.Repositories.Base;
using System;
using System.Collections.Generic;

namespace API.Caixa.Domain.Entities
{
    public class Entry : Entity
    {
        public EntryType Type { get; set; }
        public double Value { get; set; }
        public Guid UserId { get; set; }

        // EF
        public Entry() { }

        public User User { get; set; }
    }
}
