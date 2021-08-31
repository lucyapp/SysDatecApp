using Newtonsoft.Json;
using RestSharp;
using SysDatecScanApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SysDatecScanApp.RestAPIClient
{
    public class RestClient<T>
    {
        private string MainWebServiceUrl= "http://10.0.2.2:1039";
        //Crear un modelo para para cada url de servicio
        private const string LoginWebServiceUrl = "http://127.0.0.1:1039/api/UserDetailCredentialsAPI/Login";
        private const string ApiBaseAddress = "http://127.0.0.1:1039/UserDetailCredentialsAPI/Login";


        private HttpClient Service()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(MainWebServiceUrl)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {

           
            string data = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            /*     var httpClient = new HttpClient();

                 var json = JsonConvert.SerializeObject(data);

                 HttpContent httpContent = new StringContent(json);

                 httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                 var result = await httpClient.PostAsync(LoginWebServiceUrl, httpContent);
                 */

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("api-version", "1.0");

            //La siguiente línea es innecesaria:  
            //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");

            var values = new Dictionary<string,string> {
                      {
                        "username",
                        "password"
                      },
                      {
                        username,
                        password
                      }
                    };


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(values);

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://10.0.2.2/api/UserDetailCredentialsAPI/Login/",
              content);

            switch (response.StatusCode)
            {
                case (System.Net.HttpStatusCode.OK):
                    //res_Label.Text = "good";
                    break;

                case (System.Net.HttpStatusCode.BadRequest):
                    //res_Label.Text = "no good";
                    break;

                case (System.Net.HttpStatusCode.Forbidden):
                    //res_Label.Text = "no good, Forbidden";
                    break;

            }

            if (response != null) return true; else return false;
        }

        public UserDetailCredentials GetUserDetails(UserDetailCredentials user)
        {
            string endpoint = this.MainWebServiceUrl + "/users/" + user.Id;
            //string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            //wc.Headers["Authorization"] = access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<UserDetailCredentials>(response);
                //user.access_token = access_token;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /*
        public UserDetailCredentials RegisterUser(string username, string password, string firstname,
            string lastname, string middlename, int age)
        {
            string endpoint = this.MainWebServiceUrl + "/users";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password,
                firstname = firstname,
                lastname = lastname,
                middlename = middlename,
                age = age
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<UserDetailCredentials>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }*/
    }
}
