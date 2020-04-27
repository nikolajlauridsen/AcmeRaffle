using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeRaffle.Models;
using System.IO;

namespace AcmeRaffle.Data
{
    public static class DbInitializer
    {
        public static void InitializeRaffle(RaffleDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.SoldProducts.Any())
            {
                SoldProduct[] products = new SoldProduct[100];
                for (int i = 0; i < 100; i++)
                {
                    products[i] = new SoldProduct { SerialNumber = Guid.NewGuid() };
                }

                context.SoldProducts.AddRange(products);
                context.SaveChanges();

                using (StreamWriter writer = File.CreateText("SerialNumbers.txt"))
                {
                    foreach (SoldProduct product in products)
                    {
                        writer.WriteLine(product.SerialNumber.ToString());
                    }
                }
            }

        }
    }
}
