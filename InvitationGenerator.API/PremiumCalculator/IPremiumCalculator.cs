namespace InvitationGenerator.API.PremiumCalculator
{
    public interface IPremiumCalculator
    {
        decimal CalculateCreditCharge(decimal annualPremium);
        decimal CalculateTotalPremium(decimal annualPremium);
        decimal CalculateAverageMonthlyPremium(decimal annualPremium, int months);
        decimal CalculateInitialMonthlyPaymentAmount(decimal annualPremium, int months);
        decimal CalculateOtherMonthlyPaymentsAmount(decimal annualPremium, int months);
    }
}
