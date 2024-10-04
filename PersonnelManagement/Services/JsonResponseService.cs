using PersonnelManagement.DTO;

namespace PersonnelManagement.Service
{
    public class JsonResponseService
    {
        //
        // Account
        //
        public dynamic LoginSuccessResponse(AccountDTO account, string token)
        {
            return new SuccessResponseDTO<dynamic>
            {
                Status = 200,
                Title = "Login successfully.",
                Results = [
                    new
                    {
                        account.Id,
                        token,
                        account.Email,
                        account.EmployeeName,
                    }
                ]
            };
        }
        public dynamic LoginNotMatchResponse()
        {
            return new MessageResponseDTO
            {
                Title = "Can't login.",
                Status = 401,
                Messages = ["Password or account is incorrect."]
            };
        }
        public dynamic BadMessageResponse(string title, string[] messages)
        {
            return new MessageResponseDTO
            {
                Title = title,
                Status = 400,
                Messages = messages
            };
        }
        public dynamic OkMessageResponse(string title, string[] messages)
        {
            return new MessageResponseDTO
            {
                Title = title,
                Status = 200,
                Messages = messages
            };
        }
        public dynamic OkListAccountResponse(string title, AccountDTO[] accountDTOs)
        {
            return new SuccessResponseDTO<AccountDTO>
            {
                Title = title,
                Status = 200,
                Results = accountDTOs
            };
        }
    }
}
