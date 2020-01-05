namespace InvitationGenerator.FileHelper
{
    using System.Reflection;

    public class CsvDescriptor
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string HeaderName { get; set; }
        public int Index { get; set; } = -1;

        public CsvDescriptor(PropertyInfo propertyInfo, string attributeName)
        {
            PropertyInfo = propertyInfo;
            HeaderName = attributeName;
        }
    }
}
