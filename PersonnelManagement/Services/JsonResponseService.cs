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
        public dynamic OkOneAccountResponse(string title, AccountDTO[] accountDTOs)
        {
            return new SuccessResponseDTO<AccountDTO>
            {
                Title = title,
                Results = accountDTOs
            };
        }
        public dynamic OkListAccountResponse(string title,
            ICollection<AccountDTO> accountDTOs, int page, int totalPage, int totalCount)
        {
            return new SuccessResponseDTO<AccountDTO>
            {
                Title = title,
                Results = accountDTOs,
                Page = page,
                TotalPage = totalPage,
                TotalCount = totalCount
            };
        }
    }
}
