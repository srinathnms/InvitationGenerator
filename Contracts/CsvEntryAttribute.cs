namespace InvitationGenerator.Contracts
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CsvEntryAttribute : Attribute
    {
        public string HeaderName { get; set; }
    }
}
