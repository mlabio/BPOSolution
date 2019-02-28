using AutoMapper;
using BPOSolution.Entity;
using BPOSolution.Models;
using BPOSolution.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BPOSolution.Services
{
    public class BPOClientRegistration : IBPOClientRegistration
    {
        private readonly DataContext _context;
        private readonly IMapper _map;

        public BPOClientRegistration(DataContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }
        
        public async Task<BPOClient> Register([FromBody] BPOClientViewModel ClientVM)
        {
            var client = _map?.Map<Models.BPOClient>(ClientVM);
            client.DateSubmitted = System.DateTime.Now.AddHours(8);

            if (client == null) return null;

            

            await _context.BPOClient.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }
    }
}
