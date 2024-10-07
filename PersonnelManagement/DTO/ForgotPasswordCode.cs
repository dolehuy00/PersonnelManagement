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
        public bool IsVerified { get; set; } = false;
        public DateTimeOffset DeadTime { get; set; }
    }
}
