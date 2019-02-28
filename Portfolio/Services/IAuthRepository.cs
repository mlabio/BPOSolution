using BPOSolution.Models;
using System.Threading.Tasks;

namespace BPOSolution.Services
{
    public interface IAuthRepository
    {
        Task<Admin> Register(Admin admin, string password);
        Task<Admin> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);

    }
}
