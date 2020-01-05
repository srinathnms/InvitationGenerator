namespace InvitationGenerator.Contracts
{
    public class Status
    {
        public string Descriptor { get; set; }

        public bool IsSuccess { get; set; }

        public string Comments { get; set; }

        public Status(string comments)
        {
            Comments = comments;
        }

        public Status(string comments, bool isSuccess)
        {
            Comments = comments;
            IsSuccess = isSuccess;
        }
    }
}
