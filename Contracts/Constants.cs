namespace InvitationGenerator.Contracts
{
    public class Constants
    {
        public const string DateFormat = "dd/MM/yyyy";
        public const string RenewalInvitationLettersFolderName = "Renewal Invitation Letters";
        public const string RenewalInvitationLetterTemplateFilePath = @"Template/RenewalInvitationLetterTemplate.txt";
        public const string NoDataFound = "No data found";
        public const string InValidData = "Invalid data";
        public const string CustomerShouldNotBeNull = "Customer should not be null.";
        public const string InvitationLetterCustomerNullError = "Renewal invitation letter cannot be generated as customer is null.";
        public const string InvitationLetterInvalidCustomerPremiumError = "Renewal invitation letter cannot be generated as Premium value is invalid.";
        public const string InvitationLetterUpdateMessage = "Started Updating the customer details for";
        public const string InvitationLetterExistsMessage = "Renewal Invitation letter already exists for the customer.";
        public const string InvitationLetterSuccessMessage = "Renewal Invitation letter has been generated.";
    }
}
