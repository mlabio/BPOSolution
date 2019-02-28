using BPOSolution.Models;
using BPOSolution.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BPOSolution.Services
{
    public interface IBPOClientRegistration
    {
        Task<BPOClient> Register([FromBody] BPOClientViewModel ClientVM);
    }
}
