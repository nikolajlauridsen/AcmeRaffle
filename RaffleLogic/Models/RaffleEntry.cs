using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RaffleLogic.Models
{
    public class RaffleEntry
    {
        public int RaffleEntryId { get; set; }

        [Required]
        [RegularExpression(@"\D+", ErrorMessage = "Name can't contain numbers.")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"\D+", ErrorMessage = "Name can't contain numbers.")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [Range(18, 200, ErrorMessage = "You must be 18 or older to enter this raffle.")]
        public int Age { get; set; }
        [Required]

        [EmailAddress]
        public string Email { get; set; }

        public int SoldProductId { get; set; }

        [Required]
        public SoldProduct SoldProduct { get; set; }
    }
}
