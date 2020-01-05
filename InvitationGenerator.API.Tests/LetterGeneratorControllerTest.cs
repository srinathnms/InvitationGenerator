namespace InvitationGenerator.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Contracts;
    using FileHelper;
    using InvitationGenerator.API.Builder;
    using InvitationGenerator.API.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class LetterGeneratorControllerTest
    {
        [TestMethod]
        public void GivenCustomerCsvWhenCustomerStreamIsPresentThenGenerateRenewalInvitationLetters()
        {
            // Arrange
            var mockPremiumBuilder = new Mock<IPremiumBuilder<Customer>>();
            var mockCsvReader = new Mock<ICsvReader>();
            var mockTextWriter = new Mock<ITextWriter>();
            var mockLogger = new Mock<ILogger<LetterGeneratorController>>();

            var customers = this.GetCustomersMock();
            mockCsvReader.Setup(x => x.Parse<Customer>(It.IsAny<Stream>())).Returns(customers);
            mockPremiumBuilder.Setup(x => x.Set(It.IsAny<Customer>())).Returns(mockPremiumBuilder.Object);
            mockPremiumBuilder.Setup(x => x.CalculateAverageMonthlyPremium()).Returns(mockPremiumBuilder.Object);
            mockPremiumBuilder.Setup(x => x.CalculateCreditCharge()).Returns(mockPremiumBuilder.Object);
            mockPremiumBuilder.Setup(x => x.CalculateInitialMonthlyPaymentAmount()).Returns(mockPremiumBuilder.Object);
            mockPremiumBuilder.Setup(x => x.CalculateOtherMonthlyPaymentsAmount()).Returns(mockPremiumBuilder.Object);
            mockPremiumBuilder.Setup(x => x.CalculateTotalPremium()).Returns(mockPremiumBuilder.Object);

            var status = new Status("TestComments", true);
            mockTextWriter.Setup(x => x.Generate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Customer>())).Returns(status);
            var fileMock = new Mock<IFormFile>();

            var controller = new LetterGeneratorController(mockCsvReader.Object, mockPremiumBuilder.Object,
                mockTextWriter.Object, mockLogger.Object);

            // Act
            var result = controller.Post(fileMock.Object);

            // Assert
            mockCsvReader.Verify(c => c.Parse<Customer>(It.IsAny<Stream>()), Times.Once);
            Assert.IsTrue(result[0].IsSuccess);
            Assert.AreEqual("TestComments", result[0].Comments);
        }

        [TestMethod]
        public void GivenCustomerCsvWhenCustomerIsNullThenReturnNullReferenceException()
        {
            // Arrange
            var mockPremiumBuilder = new Mock<IPremiumBuilder<Customer>>();
            var mockCsvReader = new Mock<ICsvReader>();
            var mockTextWriter = new Mock<ITextWriter>();
            var mockLogger = new Mock<ILogger<LetterGeneratorController>>();

            mockCsvReader.Setup(x => x.Parse<Customer>(It.IsAny<Stream>()));
            var fileMock = new Mock<IFormFile>();

            var controller = new LetterGeneratorController(mockCsvReader.Object, mockPremiumBuilder.Object,
                mockTextWriter.Object, mockLogger.Object);

            // Act
            Action actual = () => controller.Post(fileMock.Object);

            // Assert
            Assert.ThrowsException<NullReferenceException>(actual);
        }

        private List<Customer> GetCustomersMock()
        {
            var customers = new List<Customer>
            {
                new Customer() { ID = 1, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 }
            };
            return customers;
        }
    }
}
