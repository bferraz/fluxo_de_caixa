using API.Caixa.Domain.Enums;
using API.Caixa.Domain.Repositories.Base;
using System;

namespace API.Caixa.Domain.Entities
{
    public class Entry : Entity
    {
        public EntryType Type { get; set; }
        public int AccountId { get; set; } 
        public double Value { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        // EF Ctor
        public Entry() { }

        public User User { get; set; }
        public Account Account { get; set; }
    }
}
