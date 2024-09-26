﻿namespace MovieAppApi.DTO
{
    public class ForgotPasswordConfirmCode
    {
        public ForgotPasswordConfirmCode(int codeNumber, DateTimeOffset deadTime)
        {
            CodeNumber = codeNumber;
            DeadTime = deadTime;
        }

        public int CodeNumber { get; set; }
        public DateTimeOffset DeadTime { get; set; }
    }
}
