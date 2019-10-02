
using System.Threading.Tasks;

namespace Core.Basics.IServices
{
    public interface IAuthenticateService {
        Task<string> Authenticate(string username, string password);
    }
}