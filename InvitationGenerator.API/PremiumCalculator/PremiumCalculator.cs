namespace InvitationGenerator.API.PremiumCalculator
{
    public class PremiumCalculator : IPremiumCalculator
    {
        private const int CreditChargePercentage = 5;
        private const int CurrencyDecimals = 2;

        public decimal CalculateCreditCharge(decimal annualPremium)
        {
            var creditCharge = (CreditChargePercentage * annualPremium) / 100;
            return creditCharge;
        }

        public decimal CalculateTotalPremium(decimal annualPremium)
        {
            var creditCharge = this.CalculateCreditCharge(annualPremium);
            var totalPremium = annualPremium + creditCharge;
            return totalPremium.ToFixed(CurrencyDecimals);
        }

        public decimal CalculateAverageMonthlyPremium(decimal annualPremium, int months)
        {
            var totalPremium = this.CalculateTotalPremium(annualPremium);
            var averageMonthlyPremium = GetAveragePaymentsAmount(totalPremium, months);
            return averageMonthlyPremium;

            decimal GetAveragePaymentsAmount(decimal totalPremium, int months)
            {
                var averageMonthlyPremium = totalPremium / months;

                return averageMonthlyPremium.ToFixed(3);
            }
        }

        public decimal CalculateInitialMonthlyPaymentAmount(decimal annualPremium, int months)
        {
            var averageMonthlyPremium = this.CalculateAverageMonthlyPremium(annualPremium, months);
            var remaining = this.GetRemainingPaymentsAmount(averageMonthlyPremium);
            if (remaining == 0)
            {
                return averageMonthlyPremium;
            }
            var initialMonthlyPaymentsAmount = (remaining * 11) + averageMonthlyPremium;
            return initialMonthlyPaymentsAmount.ToFixed(CurrencyDecimals);
        }

        public decimal CalculateOtherMonthlyPaymentsAmount(decimal annualPremium, int months)
        {
            var averageMonthlyPremium = this.CalculateAverageMonthlyPremium(annualPremium, months);
            var remaining = this.GetRemainingPaymentsAmount(averageMonthlyPremium);
            if (remaining == 0)
            {
                return averageMonthlyPremium;
            }
            var otherMonthlyPaymentsAmount = averageMonthlyPremium - remaining;
            return otherMonthlyPaymentsAmount.ToFixed(CurrencyDecimals);
        }

        private decimal GetRemainingPaymentsAmount(decimal averageMonthlyPremium)
        {
            var remaining = ((averageMonthlyPremium * 100) % 1) / 100;

            return remaining;
        }
    }
}
