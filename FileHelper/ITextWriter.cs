namespace InvitationGenerator.FileHelper
{
    using Contracts;

    public interface ITextWriter
    {
        Status Generate<T>(string templatePath, string destinationPath, T data);

        void IsFileExists(string path);
    }
}
