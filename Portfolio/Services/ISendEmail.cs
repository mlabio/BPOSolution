using BPOSolution.Models;
using System.Threading.Tasks;

namespace BPOSolution.Services
{
    public interface ISendEmail
    {
        Task<BPOClient> Send(BPOClient client);
    }
}
