using Newtonsoft.Json;
using RestSharp;
using SysDatecScanApp.Models;
using System;
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
        private const string MainWebServiceUrl = "http://localhost:44352";
        //Crear un modelo para para cada url de servicio
        private const string LoginWebServiceUrl = "https://localhost:44352/api/UserDetailCredentialsAPI/Login/";
        private const string ApiBaseAddress = "http://localhost:44352/UserDetailCredentialsAPI/Login/";

        private string content;
        private ObservableCollection<responseLogin> _user;
       

        private HttpClient Service()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:44352")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }





        [Obsolete]
        public async Task<bool> checkLogin(string username, string password)
        {

            var parametros = $"username=" + username + "&" + "password=" + password;
            string url = LoginWebServiceUrl + "?username='" + username + "'&" + "password='" + password + "'";

            string postData = parametros;

            

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream responseStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
            string pageContent = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();

            
            return false;
           
       }
    }
}
