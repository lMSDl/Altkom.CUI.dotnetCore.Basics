using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Basics.IServices;
using Core.Basics.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Basics.Services
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        public AuthenticateService(HttpClient httpClient) : base(httpClient) {
        }

        protected override string Path => "api/authenticate";

        public async Task<string> Authenticate(string username, string password)
        {
            try {
                var response = await Client.PostAsJsonAsync(Path, new User{Username = username, Password = password});
                response.EnsureSuccessStatusCode();

                var token =  await response.Content.ReadAsStringAsync();
                return JObject.Parse(token).Property("result").Value.ToString();

            }
            catch {
                return null;
            }
        }
    }
}
