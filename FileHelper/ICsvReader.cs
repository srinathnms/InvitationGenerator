namespace InvitationGenerator.FileHelper
{
    using System.Collections.Generic;
    using System.IO;

    public interface ICsvReader
    {
        List<T> Parse<T>(Stream stream) where T : new();
    }
}
