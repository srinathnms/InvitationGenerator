namespace InvitationGenerator.API.Builder
{
    public interface IPremiumBuilder<T>
    {
        IPremiumBuilder<T> Set(T data);
        IPremiumBuilder<T> CalculateCreditCharge();
        IPremiumBuilder<T> CalculateTotalPremium();
        IPremiumBuilder<T> CalculateAverageMonthlyPremium();
        IPremiumBuilder<T> CalculateInitialMonthlyPaymentAmount();
        IPremiumBuilder<T> CalculateOtherMonthlyPaymentsAmount();
    }
}
