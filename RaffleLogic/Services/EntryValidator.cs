using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaffleLogic.Models;

namespace RaffleLogic.Services
{
    public class EntryValidator
    {
        public bool ValidateEntry(IQueryable<SoldProduct> products,
            IQueryable<RaffleEntry> entries, RaffleEntry entry)
        {
            if (products.Any(p => p.SerialNumber == entry.SoldProduct.SerialNumber))
            {
                if (entries.Count(e => e.SoldProduct.SerialNumber == entry.SoldProduct.SerialNumber) < 2)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
