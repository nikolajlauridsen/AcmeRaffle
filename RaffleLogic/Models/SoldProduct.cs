using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RaffleLogic.Models
{
    public class SoldProduct
    {
        public int SoldProductId { get; set; }

        [DisplayName("Serial number")]
        public Guid SerialNumber { get; set; }
    }
}
