namespace InvitationGenerator.FileHelper.Tests
{
    using System;
    using System.IO;

    using Contracts;
    using FileHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CsvReaderTest
    {
        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsPresentThenReturnCustomerList()
        {
            // Arrange
            var customerDataFilePath = Path.Combine(Environment.CurrentDirectory, "Customer.csv");
            var streamReader = new StreamReader(customerDataFilePath);
            var csvReader = new CsvReader();

            // Act
            var result = csvReader.Parse<Customer>(streamReader.BaseStream);

            // Assert   
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerCsvIsWrongThenReturnFileNotFoundException()
        {
            // Arrange
            var csvReader = new CsvReader();

            // Act
            Action actual = () => csvReader.Parse<Customer>(null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(actual);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsNullThenReturnArgumentNullException()
        {
            // Arrange
            var customerNullDataFilePath = Path.Combine(Environment.CurrentDirectory, "CustomerNoData.csv");
            var streamReader = new StreamReader(customerNullDataFilePath);
            var csvReader = new CsvReader();

            // Act
            Action actual = () => csvReader.Parse<Customer>(streamReader.BaseStream);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(actual);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsNullThenReturnInvalidDataException()
        {
            // Arrange
            var customerNullDataFilePath = Path.Combine(Environment.CurrentDirectory, "InvalidData.txt");
            var streamReader = new StreamReader(customerNullDataFilePath);
            var csvReader = new CsvReader();

            // Act
            Action actual = () => csvReader.Parse<Customer>(streamReader.BaseStream);

            // Assert
            Assert.ThrowsException<InvalidDataException>(actual);
        }
    }
}
