using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AcmeRaffle.Models
{
    public class RaffleEntry
    {
        public int RaffleEntryId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int SoldProductId { get; set; }
        public SoldProduct SoldProduct { get; set; }
    }
}
