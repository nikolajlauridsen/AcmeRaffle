using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using RaffleLogic.Models;

namespace AcmeTests
{
    public class ModelTests
    {

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

        [Fact]
        public void TestValidModel()
        {
            RaffleEntry entry = getEntry();
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.True(isValid, "Valid model did not pass");
        }

        [Fact]
        public void TestFirstNameRequired()
        {
            RaffleEntry entry = getEntry();
            entry.FirstName = "";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "FirstName must be required");
        }

        [Fact]
        public void TestFirstContainsNoNumber()
        {
            RaffleEntry entry = getEntry();
            entry.FirstName = "L31f";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "FirstName must not contain numbers");
        }

        [Fact]
        public void TestLastNameRequired()
        {
            RaffleEntry entry = getEntry();
            entry.LastName = "";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "LastName must be required");
        }

        [Fact]
        public void TestLastContainsNoNumber()
        {
            RaffleEntry entry = getEntry();
            entry.FirstName = "3g3sen";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "LastName must not contain numbers");
        }

        [Fact]
        public void TestEmailRequired()
        {
            RaffleEntry entry = getEntry();
            entry.Email = "";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "Email must be required");
        }

        [Fact]
        public void TestInvalidEmail()
        {
            RaffleEntry entry = getEntry();
            entry.Email = "invalid";
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "Invalid email must not be accepted");
        }

        [Fact]
        public void TestAgeLessThanMinimum()
        {
            RaffleEntry entry = getEntry();
            entry.Age = 17;
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "Age must be required to be over 18");
        }

        [Fact]
        public void TestAgeExactlyMinimum()
        {
            RaffleEntry entry = getEntry();
            entry.Age = 18;
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.True(isValid, "Exact age must pass");
        }

        [Fact]
        public void TestSoldProductRequired()
        {
            RaffleEntry entry = getEntry();
            entry.SoldProduct = null;
            List<ValidationResult> result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            Assert.False(isValid, "SoldProduct must be required");
        }
    }
}
