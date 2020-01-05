namespace InvitationGenerator.API.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using InvitationGenerator.API.PremiumCalculator;

    [TestClass]
    public class PremiumCalculatorTest
    {
        [TestMethod]
        public void GivenAnnualPremiumWhenAnnualPremiumIsPresentThenReturnCreditCharge()
        {
            // Arrange
            var annualPremium = 50;
            var premiumCalculator = new PremiumCalculator();

            // Act
            var creditCharge = premiumCalculator.CalculateCreditCharge(annualPremium);

            // Assert
            Assert.AreEqual(2.5M, creditCharge);
        }

        [TestMethod]
        public void GivenAnnualPremiumWhenAnnualPremiumIsPresentThenReturnTotalPremium()
        {
            // Arrange
            var annualPremium = 50;
            var premiumCalculator = new PremiumCalculator();

            // Act
            var totalPremium = premiumCalculator.CalculateTotalPremium(annualPremium);

            // Assert
            Assert.AreEqual(52.5M, totalPremium);
        }

        [TestMethod]
        public void GivenAnnualPremiumAndMonthWhenAnnualPremiumAndMonthIsPresentThenReturnAverageMonthlyPremium()
        {
            // Arrange
            var annualPremium = 50;
            var months = 12;
            var premiumCalculator = new PremiumCalculator();

            // Act
            var averageMonthlyPremium = premiumCalculator.CalculateAverageMonthlyPremium(annualPremium, months);

            // Assert
            Assert.AreEqual(4.375M, averageMonthlyPremium);
        }

        [TestMethod]
        public void GivenAnnualPremiumAndMonthWhenAnnualPremiumAndMonthIsPresentThenReturnInitialMonthlyPaymentAmount()
        {
            // Arrange
            var annualPremium = 50;
            var months = 12;
            var premiumCalculator = new PremiumCalculator();

            // Act
            var initialMonthlyPaymentAmount = premiumCalculator.CalculateInitialMonthlyPaymentAmount(annualPremium, months);

            // Assert
            Assert.AreEqual(4.43M, initialMonthlyPaymentAmount);
        }

        [TestMethod]
        public void GivenAnnualPremiumAndMonthWhenAnnualPremiumAndMonthIsPresentThenReturnOtherMonthlyPaymentsAmount()
        {
            // Arrange
            var annualPremium = 50;
            var months = 12;
            var premiumCalculator = new PremiumCalculator();

            // Act
            var totalPremium = premiumCalculator.CalculateOtherMonthlyPaymentsAmount(annualPremium, months);

            // Assert
            Assert.AreEqual(4.37M, totalPremium);
        }
    }
}
