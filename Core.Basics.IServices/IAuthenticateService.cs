
namespace Core.Basics.IServices
{
    public interface IAuthenticateService {
        string Authenticate(string username, string password);
    }
}