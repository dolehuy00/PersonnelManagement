﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MovieAppApi.Service
{
    public class SMTPService
    {
        public async Task SendPasswordResetEmail(string emailAddress, int randomCode)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("huydo24082002@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Yêu cầu lấy lại mật khẩu";

            var builder = new BodyBuilder();
            builder.TextBody = $"Mã xác nhận của bạn là: {randomCode}\nMã có hiệu lực trong 5 phút";
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("huydo24082002@gmail.com", ""); // Thay thế bằng địa chỉ email và mật khẩu ứng dụng của bạn
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
