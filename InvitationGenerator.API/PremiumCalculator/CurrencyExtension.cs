namespace InvitationGenerator.API.PremiumCalculator
{
    using System;

    public static class CurrencyExtension
    {
        public static decimal ToFixed(this decimal value, int decimals)
        {
            return Math.Round(value, decimals);
        }
    }
}
