using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaffleLogic.Models;


namespace AcmeRaffle.Data
{
    public class RaffleDbContext : DbContext
    {
        public RaffleDbContext(DbContextOptions<RaffleDbContext> options) : base(options) { }

        public DbSet<RaffleEntry> Entries { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
    }
}
