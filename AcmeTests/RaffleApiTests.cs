using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

using RaffleLogic.Models;
using RaffleLogic.Services;
using AcmeRaffle.Controllers;
using AcmeRaffle.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AcmeTests
{
    public class RaffleApiTests
    {
        private IEntryValidator getMockValidator(bool returns)
        {
            var mock = new Mock<IEntryValidator>();
            mock.Setup(validator => validator.ValidateEntry(
                It.IsAny<IQueryable<SoldProduct>>(),
                It.IsAny<IQueryable<RaffleEntry>>(),
                It.IsAny<RaffleEntry>())
            ).Returns(returns);
            return mock.Object;
        }

        /// <summary>
        /// Creates a valid raffle entry
        /// </summary>
        /// <returns>RaffleEntry</returns>
        private RaffleEntry getEntry()
        {
            RaffleEntry entry = new RaffleEntry
            {
                FirstName = "Arthur",
                LastName = "Dent",
                Email = "Arthur@hitchiker.com",
                Age = 30,
                SoldProduct = new SoldProduct
                {
                    SerialNumber = new Guid()
                }
            };
            return entry;
        }

        private RaffleDbContext getDb(string name)
        {
            DbContextOptionsBuilder<RaffleDbContext> optionsBuilder = 
                new DbContextOptionsBuilder<RaffleDbContext>()
                    .UseInMemoryDatabase(databaseName: name);

            return new RaffleDbContext(optionsBuilder.Options);
        }

        [Fact]
        public async void BadModelReturns400()
        {
            RaffleApiController controller = new RaffleApiController(
                getDb("badModel"),
                getMockValidator(true));

            controller.ModelState.AddModelError("FirstName", "Must not be null.");

            IStatusCodeActionResult result = await controller.PostRaffleEntry(new RaffleEntry());
            StatusCodeResult statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);
        }

        [Fact]
        public async void ValidatorIsUsed()
        {
            RaffleApiController controller = new RaffleApiController(
                getDb("ValidatorTest"),
                new EntryValidator());

            RaffleEntry entry = getEntry();

            IStatusCodeActionResult result = await controller.PostRaffleEntry(entry);
            StatusCodeResult statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);
        }

        [Fact]
        public async void EntryAddedToDB()
        {
            RaffleDbContext context = getDb("DBTest");
            RaffleApiController controller = new RaffleApiController(
                context,
                getMockValidator(true));

            await controller.PostRaffleEntry(new RaffleEntry());
            Assert.Equal(1, await context.Entries.CountAsync());
        }

        [Fact]
        public async void NoDuplicateSoldProduct()
        {
            Guid serial = Guid.NewGuid();
            SoldProduct product = new SoldProduct{SerialNumber = serial};

            RaffleDbContext context = getDb("NoDupe");
            context.SoldProducts.Add(product);
            await context.SaveChangesAsync();

            RaffleEntry entry = getEntry();
            entry.SoldProduct.SerialNumber = serial;

            RaffleApiController controller = new RaffleApiController(
                context,
                new EntryValidator());

            IStatusCodeActionResult result = await controller.PostRaffleEntry(entry);
            StatusCodeResult statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusResult.StatusCode);

            Assert.Equal(1, await context.SoldProducts.CountAsync());
        }
    }
}
