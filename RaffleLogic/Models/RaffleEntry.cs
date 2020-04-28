using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RaffleLogic.Models
{
    public class RaffleEntry
    {
        public int RaffleEntryId { get; set; }

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [Range(18, 200, ErrorMessage = "You must be 18 or older to enter this raffle.")]
        public int Age { get; set; }
        [Required]

        [EmailAddress]
        public string Email { get; set; }

        public int SoldProductId { get; set; }
        public SoldProduct SoldProduct { get; set; }
    }
}
