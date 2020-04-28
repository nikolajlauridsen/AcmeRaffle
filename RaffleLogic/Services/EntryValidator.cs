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
        /*
         * It's suboptimal that this isn't async, however, microsoft states:
         * EF Core doesn't support multiple parallel operations being run on the same context instance.
         * You should always wait for an operation to complete before beginning the next operation.
         * So how to get around this? 
         */
        public bool ValidateEntry(IQueryable<SoldProduct> products,
            IQueryable<RaffleEntry> entries, RaffleEntry entry)
        {
            // Check if the serial number is valid
            if (products.Any(p => p.SerialNumber == entry.SoldProduct.SerialNumber))
            {
                // Set the instance of SoldProduct in the entry to be the one from the database
                // Otherwise the foreign key wont be resolved properly and the database will create a new product row
                // Maybe move this to a separate function?
                SoldProduct purchasedProduct = products.First(p => p.SerialNumber == entry.SoldProduct.SerialNumber);
                entry.SoldProduct = purchasedProduct;
                // Check that less than 2 entries has been submitted for the serial number
                if (entries.Count(e => e.SoldProduct.SerialNumber == entry.SoldProduct.SerialNumber) < 2)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
