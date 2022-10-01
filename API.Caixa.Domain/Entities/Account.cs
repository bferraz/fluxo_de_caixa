using API.Caixa.Domain.Exceptions;
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

        public void UpdateValue(Entry entry)
        {
            switch (entry.Type)
            {
                case Enums.EntryType.Credit:
                    Credit(entry.Value);
                    break;
                case Enums.EntryType.Debit:
                    Debit(entry.Value);
                    break;
            }
        }

        private void Credit(double value)
        {
            if (value > 0)
                this.Value += value;
            else
                throw new InvalidCreditException();
        }

        private void Debit(double value)
        {
            if (value < 0)
                throw new InvalidDebitException();
            else if (this.Value >= value)
                Value -= value;
            else
                throw new InsuficientFoundsException();

        }
    }
}
