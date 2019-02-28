using BPOSolution.Entity;
using BPOSolution.Helper;
using BPOSolution.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BPOSolution.Services
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpHelper _mail;
        private IConfiguration _configuration;
        private DataContext _context;
        

        public SendEmail(SmtpHelper send, IConfiguration configuration, DataContext context){
            _mail = send;
            _configuration = configuration;
            _context = context;
        }  
    
        public async Task<BPOClient> Send(BPOClient ClientVM)
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            using (var newContext = new DataContext(dbContextOptions.Options)) //Separate this context's instance from the request
            {
                var client = newContext.BPOClient.FirstOrDefault(c => c.Id == ClientVM.Id);
                
                if (client == null) throw new NullReferenceException();

                try
                {

                    await _mail.SendEmailAsync("Roland",
                        _configuration.GetSection("AdminEmailCredentials:SenderEmail").Value,
                        _configuration.GetSection("AdminEmailCredentials:ReceiverEmail").Value,
                            ClientVM, "New BPO Client");

                    client.IsSent = 1;
                    newContext.Update(client);

                }
                catch (Exception ex)
                {
                    client.Message = ex.Message + "\n " + client.Message;
                    client.IsSent = 0;
                }
                finally
                {
                    newContext.SaveChanges();
                }

                return null;
            }
        }
    }
}
