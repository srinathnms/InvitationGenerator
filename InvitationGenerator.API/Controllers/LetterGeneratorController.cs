namespace InvitationGenerator.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Contracts;
    using static Contracts.Constants;
    using FileHelper;
    using InvitationGenerator.API.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/[controller]")]
    public class LetterGeneratorController : ControllerBase
    {
        private readonly ICsvReader csvReader;
        private readonly IPremiumBuilder<Customer> customerBuilder;
        private readonly ITextWriter textWriter;
        private readonly ILogger<LetterGeneratorController> logger;

        public LetterGeneratorController(ICsvReader csvReader,
            IPremiumBuilder<Customer> customerBuilder,
            ITextWriter textWriter,
            ILogger<LetterGeneratorController> logger)
        {
            this.csvReader = csvReader;
            this.customerBuilder = customerBuilder;
            this.textWriter = textWriter;
            this.logger = logger;
        }

        [HttpPost]
        public List<Status> Post(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var customers = csvReader.Parse<Customer>(fileStream);
            if (customers == null || customers.Count == 0)
            {
                throw new NullReferenceException(CustomerShouldNotBeNull);
            }

            customers.ForEach(GenerateInvitationLetter);

            var status = customers.Select(customer => customer.Status).ToList();
            return status;

            void GenerateInvitationLetter(Customer customer)
            {
                logger.LogInformation($"{InvitationLetterUpdateMessage} {customer.Title} {customer.FirstName}");
                customerBuilder
                   .Set(customer)
                   .CalculateCreditCharge()
                   .CalculateTotalPremium()
                   .CalculateAverageMonthlyPremium()
                   .CalculateInitialMonthlyPaymentAmount()
                   .CalculateOtherMonthlyPaymentsAmount();

                var letterFileName = $"{customer.ID}{customer.FirstName}.txt";
                var invitationLetterPath = Path.Combine(Environment.CurrentDirectory, RenewalInvitationLettersFolderName, letterFileName);

                customer.Status = this.textWriter.Generate<Customer>(RenewalInvitationLetterTemplateFilePath, invitationLetterPath, customer);
                customer.Status.Descriptor = $"{customer.Title} {customer.FirstName}";
            }
        }
    }
}
