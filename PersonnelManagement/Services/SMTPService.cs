using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MovieAppApi.Service
{
    public class SMTPService
    {
        private static string from = "noreply";
        private static string authenEmail = "huydo24082002@gmail.com";
        private static string authenPassword = "zuzsjefblbftfnhd";

        public async Task SendPasswordResetEmail(string emailAddress, int randomCode)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Yêu cầu lấy lại mật khẩu";

            var builder = new BodyBuilder();
            builder.TextBody = $"Mã xác nhận của bạn là: {randomCode}\nMã có hiệu lực trong 5 phút";
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(authenEmail, authenPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public static async Task SendPasswordNewAccountEmail(string emailAddress, string password)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Yêu cầu lấy lại mật khẩu";

            var builder = new BodyBuilder();
            builder.TextBody = $"Mật khẩu ban đầu của bạn là: {password}\nVui lòng đổi thành mật khẩu của bạn ngay lập tức.";
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(authenEmail, authenPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
