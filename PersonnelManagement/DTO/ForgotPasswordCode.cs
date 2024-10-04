namespace PersonnelManagement.DTO
{
    public class ForgotPasswordCode
    {
        public ForgotPasswordCode(int codeNumber, DateTimeOffset deadTime)
        {
            CodeNumber = codeNumber;
            DeadTime = deadTime;
        }

        public int CodeNumber { get; set; }
        public DateTimeOffset DeadTime { get; set; }
    }
}
