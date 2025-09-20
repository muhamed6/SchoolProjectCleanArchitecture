using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        #region Fields

        #endregion

        #region Constructors
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Handle Functions
        public async Task<string> SendEmail(string email, string Message)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    //await client.ConnectAsync("smtp.gmail.com", 587);
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    //client.Authenticate("muhamedashraf990@gmail.com", "vvdnyasqbizotgfs");
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", "muhamedashraf990@gmail.com"));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject ="New Submitted Data";
                    //message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion

    }
}
