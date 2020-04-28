using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaffleLogic.Models;

namespace RaffleLogic.Services
{
    public interface IEntryValidator
    {
        bool ValidateEntry(IQueryable<SoldProduct> products,
            IQueryable<RaffleEntry> entries, RaffleEntry entry);
    }
}
