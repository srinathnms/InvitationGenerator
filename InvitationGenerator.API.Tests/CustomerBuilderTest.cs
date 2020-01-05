namespace InvitationGenerator.API.Tests
{
    using Contracts;
    using InvitationGenerator.API.Builder;
    using InvitationGenerator.API.PremiumCalculator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CustomerBuilderTest
    {
        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenSetCustomerData()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);

            // Act
            premiumCalculatorBuilder.Set(mockCustomer);

            // Assert
            Assert.AreEqual(50, mockCustomer.AnnualPremium);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsNullThenReturnNull()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            Customer mockCustomer = null;
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);

            // Act
            premiumCalculatorBuilder.Set(mockCustomer);

            // Assert
            Assert.IsNull(mockCustomer);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenReturnCreditCharge()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            mockPremiumCalculator.Setup(c => c.CalculateCreditCharge(It.IsAny<decimal>())).Returns(2.5M);
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);
            premiumCalculatorBuilder.Set(mockCustomer);

            // Act
            premiumCalculatorBuilder.CalculateCreditCharge();

            // Assert
            mockPremiumCalculator.Verify(c => c.CalculateCreditCharge(It.IsAny<decimal>()), Times.Once);
            Assert.AreEqual(2.5M, mockCustomer.CreditCharge);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenReturnTotalPremium()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            mockPremiumCalculator.Setup(c => c.CalculateTotalPremium(It.IsAny<decimal>())).Returns(52.5M);
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);
            premiumCalculatorBuilder.Set(mockCustomer);

            // Act
            premiumCalculatorBuilder.CalculateTotalPremium();

            // Assert
            mockPremiumCalculator.Verify(c => c.CalculateTotalPremium(It.IsAny<decimal>()), Times.Once);
            Assert.AreEqual(52.5M, mockCustomer.TotalPremium);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenReturnAverageMonthlyPremium()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            mockPremiumCalculator.Setup(c => c.CalculateAverageMonthlyPremium(It.IsAny<decimal>(), It.IsAny<int>())).Returns(4.375M);
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);
            premiumCalculatorBuilder.Set(mockCustomer);

            // Act
            premiumCalculatorBuilder.CalculateAverageMonthlyPremium();

            // Assert
            mockPremiumCalculator.Verify(c => c.CalculateAverageMonthlyPremium(It.IsAny<decimal>(), It.IsAny<int>()), Times.Once);
            Assert.AreEqual(4.375M, mockCustomer.AverageMonthlyPremium);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenReturnInitialMonthlyPaymentAmount()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            mockPremiumCalculator.Setup(c => c.CalculateInitialMonthlyPaymentAmount(It.IsAny<decimal>(), It.IsAny<int>())).Returns(4.43M);
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);
            premiumCalculatorBuilder.Set(mockCustomer);

            // Act
            premiumCalculatorBuilder.CalculateInitialMonthlyPaymentAmount();

            // Assert
            mockPremiumCalculator.Verify(c => c.CalculateInitialMonthlyPaymentAmount(It.IsAny<decimal>(), It.IsAny<int>()), Times.Once);
            Assert.AreEqual(4.43M, mockCustomer.InitialMonthlyPaymentAmount);
        }

        [TestMethod]
        public void GivenCustomerWhenCustomerIsPresentThenReturnOtherMonthlyPaymentsAmount()
        {
            // Arrange
            var mockPremiumCalculator = new Mock<IPremiumCalculator>();
            mockPremiumCalculator.Setup(c => c.CalculateOtherMonthlyPaymentsAmount(It.IsAny<decimal>(), It.IsAny<int>())).Returns(4.37M);
            var mockCustomer = this.GetCustomerMock();
            var premiumCalculatorBuilder = new CustomerBuilder(mockPremiumCalculator.Object);
            premiumCalculatorBuilder.Set(mockCustomer);

            // Act
            premiumCalculatorBuilder.CalculateOtherMonthlyPaymentsAmount();

            // Assert
            mockPremiumCalculator.Verify(c => c.CalculateOtherMonthlyPaymentsAmount(It.IsAny<decimal>(), It.IsAny<int>()), Times.Once);
            Assert.AreEqual(4.37M, mockCustomer.OtherMonthlyPaymentsAmount);
        }

        private Customer GetCustomerMock()
        {
            var customer = new Customer() { ID = 1, Title = "Mr", FirstName = "John", Surname = "Smith", ProductName = "Enhanced Cover", PayoutAmount = 190820, AnnualPremium = 50 };
            return customer;
        }
    }

}
