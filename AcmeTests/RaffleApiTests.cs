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
        public async void EnsureBadModelIsCaught()
        {
            RaffleApiController controller = new RaffleApiController(
                getDb("badModel"),
                getMockValidator(true));

            RaffleEntry entry = getEntry();
            entry.FirstName = "";

            IStatusCodeActionResult result = await controller.PostRaffleEntry(entry);
            StatusCodeResult statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);
        }
    }
}
