using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace IoTHomeAssistant.Blazor.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("IoT home assistant", "home.assistant@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("iothomeassistant2021@gmail.com", "");
                await client.SendAsync(emailMessage);
                
 
                await client.DisconnectAsync(true);
            }
        }
    }
}