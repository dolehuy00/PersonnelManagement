using PersonnelManagement.Model;

namespace PersonnelManagement.Service
{
    public class JsonResponseService
    {
        //
        // Account
        //
        public dynamic LoginSuccessResponse(Account account, string token)
        {
            return new
            {
                account.Id,
                token,
                account.Email,
                account.Employee.Fullname,
                account.Employee.DateOfBirth,
            };
        }
    }
}
