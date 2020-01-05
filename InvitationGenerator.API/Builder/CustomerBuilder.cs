namespace InvitationGenerator.API.Builder
{
    using Contracts;
    using InvitationGenerator.API.PremiumCalculator;

    public class CustomerBuilder : IPremiumBuilder<Customer>
    {
        private readonly IPremiumCalculator premiumCalculator;
        private Customer customer;

        public CustomerBuilder(IPremiumCalculator premiumCalculator)
        {
            this.premiumCalculator = premiumCalculator;
        }

        public IPremiumBuilder<Customer> Set(Customer customer)
        {
            this.customer = customer;
            return this;
        }

        public IPremiumBuilder<Customer> CalculateCreditCharge()
        {
            this.customer.CreditCharge = this.premiumCalculator.CalculateCreditCharge(this.customer.AnnualPremium);
            return this;
        }

        public IPremiumBuilder<Customer> CalculateTotalPremium()
        {
            this.customer.TotalPremium = this.premiumCalculator.CalculateTotalPremium(this.customer.AnnualPremium);
            return this;
        }

        public IPremiumBuilder<Customer> CalculateAverageMonthlyPremium()
        {
            this.customer.AverageMonthlyPremium = this.premiumCalculator.CalculateAverageMonthlyPremium(this.customer.AnnualPremium, 12);
            return this;
        }

        public IPremiumBuilder<Customer> CalculateInitialMonthlyPaymentAmount()
        {
            this.customer.InitialMonthlyPaymentAmount = this.premiumCalculator.CalculateInitialMonthlyPaymentAmount(this.customer.AnnualPremium, 12);
            return this;
        }

        public IPremiumBuilder<Customer> CalculateOtherMonthlyPaymentsAmount()
        {
            this.customer.OtherMonthlyPaymentsAmount = this.premiumCalculator.CalculateOtherMonthlyPaymentsAmount(this.customer.AnnualPremium, 12);
            return this;
        }
    }
}
