using RestSharp;
using SysDatecScanApp.Models;
using SysDatecScanApp.RestAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysDatecScanApp.ServicesHandler
{
    public class LoginService
    {
        RestClient<UserDetailCredentials> _restClient = new RestClient<UserDetailCredentials>();

        public async Task<bool> CheckLoginIfExists(string username, string password)
        {
            var check = await _restClient.AuthenticateUser(username, password);

            return check;
        }
    }
}
