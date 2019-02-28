using BPOSolution.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace BPOSolution.Helper
{
    public class SmtpHelper
    {
        public static IConfiguration _configuration { get; set; }
        public async Task SendEmailAsync(string Name, string SenderEmail, string ReceiverEmail, BPOClient client, string Subject)
        {
            var template = "./HtmlTemplate/EmailTemplate.html";

            var Content = "A message received from a potential BPO client.<br>"
                + "Name: <b>" + client.FullName + "</b><br>"
                + "Company: " + client.CompanyName + "<br>"
                + "Email: " + client.Email + "<br>"
                + "Phone: " + client.ContactNumber + "<br>"
                + "Message: " + client.Message+ "<br>"
                ;
            
            var emailMessage = new MimeMessage
            {
                From = {
                    new MailboxAddress("Dev Partners", SenderEmail)
                },
                To = {
                    new MailboxAddress("Recipient", ReceiverEmail)
                },
                Subject = Subject,
                Body = new BodyBuilder() {
                    HtmlBody = File.ReadAllText(template).Replace("<Fullname>", Name)
                                                             .Replace("<Subject>", Subject)
                                                             .Replace("<Content>", Content)                        
                }.ToMessageBody()
            };

            await SmtpAsync(emailMessage);
        }
        public static async Task SmtpAsync(MimeMessage emailMessage)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable).ConfigureAwait(false);  
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_configuration.GetSection("AdminEmailCredentials:SenderEmail").Value, _configuration.GetSection("AdminEmailCredentials:SenderPassword").Value);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            };
        }
    }
}
