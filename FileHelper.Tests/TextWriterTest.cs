namespace InvitationGenerator.FileHelper.Tests
{
    using System;
    using System.IO;

    using Contracts;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class TextWriterTest
    {
        private string destinationFilePath;

        [TestMethod]
        public void GivenTemplatePathDestinationPathDataThenGenerateInvitationLetter()
        {
            // Arrange
            var customer = this.GetCustomerMock((int)DateTime.Now.Ticks);
            var mockTemplatePath = Path.Combine(Environment.CurrentDirectory, "RenewalInvitationLetterTemplate.txt");
            var mockDestinationFile = $"{customer.ID}{customer.FirstName}.txt";
            this.destinationFilePath = Path.Combine(Environment.CurrentDirectory, mockDestinationFile);
            
            var mockLogger = new Mock<ILogger<FileHelper.TextWriter>>();

            var invitationGenerator = new FileHelper.TextWriter(mockLogger.Object);

            // Act
            invitationGenerator.Generate(mockTemplatePath, destinationFilePath, customer);

            // Assert
            Assert.IsTrue(File.Exists(destinationFilePath));
            Assert.IsTrue(File.ReadAllLines(destinationFilePath).Length > 0);
        }

        [TestMethod]
        public void GivenTemplatePathDestinationPathDataAndInvalidTemplatePathIsNullThenThrowFileNotFoundException()
        {
            // Arrange
            var customer = this.GetCustomerMock((int)DateTime.Now.Ticks);
            var mockInvalidTemplatePath = Path.Combine(Environment.CurrentDirectory, "RenewalInvitationLetterTemplate1.txt");
            var mockDestinationFile = $"{customer.ID}{customer.FirstName}.txt";
            this.destinationFilePath = Path.Combine(Environment.CurrentDirectory, mockDestinationFile);
            var mockLogger = new Mock<ILogger<FileHelper.TextWriter>>();

            var invitationGenerator = new FileHelper.TextWriter(mockLogger.Object);

            // Act
            Action actual = () => invitationGenerator.Generate(mockInvalidTemplatePath, mockDestinationFile, customer);

            // Assert
            Assert.ThrowsException<FileNotFoundException>(actual);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(this.destinationFilePath))
            {
                File.Delete(this.destinationFilePath);
            }
        }

        private Customer GetCustomerMock(int id)
        {
            var customer = new Customer() { ID = id, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 };
            return customer;
        }
    }
}
