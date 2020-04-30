using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaffleLogic.Models;
using Xunit;
using RaffleLogic.Services;

namespace AcmeTests
{
    public class EntryValidatorTests
    {
        private EntryValidator _validator;
        private List<RaffleEntry> _entries;
        private List<SoldProduct> _products;
        private RaffleEntry _entry;

        public EntryValidatorTests()
        {
            _validator = new EntryValidator();
        }

        /// <summary>
        /// Creates a raffle entry with all properties set except for SoldProduct
        /// </summary>
        /// <returns>RaffleEntry</returns>
        private RaffleEntry getEntry()
        {
            RaffleEntry entry = new RaffleEntry
            {
                FirstName = "Arthur",
                LastName = "Dent",
                Email = "Arthur@hitchiker.com",
                Age = 30
            };
            return entry;
        }

        [Fact]
        public void ValidEntry()
        {
            _entries = new List<RaffleEntry>();

            Guid serial = Guid.NewGuid();
            _products = new List<SoldProduct> {new SoldProduct {SerialNumber = serial}};

            _entry = getEntry();
            _entry.SoldProduct = new SoldProduct
            {
                SerialNumber = serial
            };

            bool validEntry = _validator.ValidateEntry(_products.AsQueryable(), _entries.AsQueryable(), _entry);
            Assert.True(validEntry, "A valid entry was rejected.");
        }

        [Fact]
        public void InvalidSerialNumber()
        {
            _entries = new List<RaffleEntry>();

            _products = new List<SoldProduct> {new SoldProduct {SerialNumber = Guid.NewGuid()}};

            _entry = getEntry();
            _entry.SoldProduct = new SoldProduct
            {
                SerialNumber = Guid.NewGuid()
            };

            bool validEntry = _validator.ValidateEntry(_products.AsQueryable(), _entries.AsQueryable(), _entry);
            Assert.False(validEntry, "A non valid serial number got accepted");
        }

        [Fact]
        public void EntryWithOnePreviousEntry()
        {
            _entries = new List<RaffleEntry>();

            Guid serial = Guid.NewGuid();
            SoldProduct soldProduct = new SoldProduct {SerialNumber = serial};
            _products = new List<SoldProduct> { soldProduct };

            RaffleEntry entry1 = getEntry();
            entry1.SoldProduct = soldProduct;
            _entries.Add(entry1);

            _entry = getEntry();
            _entry.SoldProduct = soldProduct;

            bool validEntry = _validator.ValidateEntry(_products.AsQueryable(), _entries.AsQueryable(), _entry);

            Assert.True(validEntry, "Entry got rejected even though there has only been 1 previous entry with the same serial number.");
        }

        [Fact]
        public void EntryWithTwoPreviousEntries()
        {
            _entries = new List<RaffleEntry>();

            Guid serial = Guid.NewGuid();
            SoldProduct soldProduct = new SoldProduct { SerialNumber = serial };
            _products = new List<SoldProduct> { soldProduct };

            RaffleEntry entry1 = getEntry();
            entry1.SoldProduct = soldProduct;

            RaffleEntry entry2 = getEntry();
            entry2.SoldProduct = soldProduct;

            _entries.Add(entry1);
            _entries.Add(entry2);

            _entry = getEntry();
            _entry.SoldProduct = soldProduct;

            bool validEntry = _validator.ValidateEntry(_products.AsQueryable(), _entries.AsQueryable(), _entry);

            Assert.False(validEntry, "Entry got validated with 2 previous entries with the same serial number.");
        }

        [Fact]
        public void EntryGetsNewProductReference()
        {
            _entries = new List<RaffleEntry>();

            Guid serial = Guid.NewGuid();
            SoldProduct targetProduct = new SoldProduct{SerialNumber = serial, SoldProductId = 2};
            _products = new List<SoldProduct>
            {
                new SoldProduct{SerialNumber = Guid.NewGuid(), SoldProductId = 1},
                targetProduct,
                new SoldProduct{SerialNumber = Guid.NewGuid(), SoldProductId = 3},
            };

            _entry = new RaffleEntry();
            _entry.SoldProduct = new SoldProduct{SerialNumber = serial};

            _validator.ValidateEntry(_products.AsQueryable(), _entries.AsQueryable(), _entry);

            Assert.Equal(targetProduct.SoldProductId, _entry.SoldProduct.SoldProductId);
        }
    }
}
