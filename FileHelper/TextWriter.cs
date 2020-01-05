namespace InvitationGenerator.FileHelper
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using Microsoft.Extensions.Logging;
    using Contracts;
    using static Contracts.Constants;

    public class TextWriter : ITextWriter
    {
        private readonly ILogger<TextWriter> logger;

        public TextWriter(ILogger<TextWriter> logger)
        {
            this.logger = logger;
        }

        public Status Generate<T>(string templatePath, string destinationPath, T data)
        {
            var templateFilePath = Path.Combine(Environment.CurrentDirectory, templatePath);
            this.IsFileExists(templateFilePath);

            var template = File.ReadAllText(templateFilePath);

            if (!File.Exists(destinationPath))
            {
                using (var streamWriter = new StreamWriter(destinationPath))
                {
                    var fileContent = UpdateTemplate();
                    streamWriter.Write(fileContent);

                    logger.LogInformation(InvitationLetterSuccessMessage);
                    return new Status(InvitationLetterSuccessMessage, true);
                }
            }

            logger.LogInformation(InvitationLetterExistsMessage);
            return new Status(InvitationLetterExistsMessage);

            string UpdateTemplate()
            {
                var pattern = "@[A-Za-z]+[@]{1}";

                var result = Regex.Replace(template, pattern, (match) =>
                 data
                   .GetType()
                   .GetProperty(match.Value.Replace("@",""))
                   .GetValue(data)
                   .ToString()
                );

                return result;
            }
        }

        public void IsFileExists(string path)
        {
            var isFileExists = File.Exists(path);
            if (!isFileExists)
            {
                var file = Path.GetFileName(path);
                throw new FileNotFoundException($"File({file}) doesn't exists in the path {path}");
            }
        }
    }
}
