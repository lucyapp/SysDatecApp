using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SysDatecScanApp.RestAPIClient
{
    public class RestClientBase
    {
        public static IFormatProvider MainWebServiceUrl { get; private set; }

        //private static async Task<T> PostData<T>(String objString, String parametros)
        //{
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //    string url2 = string.Format(MainWebServiceUrl, parametros);

        //    try
        //    {

        //        url2 = url2.Replace("www", "origen");
        //        HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(url2) };
        //        httpClient.Timeout = TimeSpan.FromMinutes(30);

        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

        //        httpClient.DefaultRequestHeaders.Add("JSON", "postValues");

        //        HttpResponseMessage response = await httpClient.PostAsync(url2, new StringContent(objString, Encoding.UTF8, "application/json"));
        //        var UrlResponse = response.RequestMessage.RequestUri.ToString();
        //        if (!UrlResponse.Contains("api"))
        //        {
        //        }
        //        string responseContent = await response.Content.ReadAsStringAsync();

        //        switch ((int)response.StatusCode)
        //        {
        //            case (int)HttpStatusCode.NotAcceptable:
        //            case 422: throw new HttpRequestException(responseContent);

        //            case (int)HttpStatusCode.BadRequest:
        //            case (int)HttpStatusCode.Created:
        //            case (int)HttpStatusCode.OK:


        //            case (int)HttpStatusCode.Unauthorized:


        //                break;

        //            case 500:



        //            default:
        //                return default(T);
        //        }
        //    }
        //    catch (Exception exception)
        //    {


        //    }
        //}
        //private static async Task<T> PutData<T>(String objString, String parametros)
        //{
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //    string url2 = string.Format(MainWebServiceUrl, parametros);
        //    try
        //    {

        //        url2 = url2.Replace("www", "origen");
        //        HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(url2) };
        //        httpClient.Timeout = TimeSpan.FromMinutes(30);
        //        //httpClient.BaseAddress = new Uri(ApiUrlValeFiel);
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        //        //httpClient.DefaultRequestHeaders.Add("Authorization", Settings.jwtToken);
        //        httpClient.DefaultRequestHeaders.Add("JSON", "putValues");



        //        HttpResponseMessage response = await httpClient.PutAsync(url2, new StringContent(objString, Encoding.UTF8, "application/json"));
        //        var UrlResponse = response.RequestMessage.RequestUri.ToString();
        //        if (!UrlResponse.Contains("api"))
        //        {

        //        }
        //        string responseContent = await response.Content.ReadAsStringAsync();


        //        switch ((int)response.StatusCode)
        //        {
        //            case (int)HttpStatusCode.NotAcceptable:
        //            case 422:
        //                throw new HttpRequestException(responseContent);

        //            case (int)HttpStatusCode.BadRequest: break;
        //            case (int)HttpStatusCode.Created: break;
        //            case (int)HttpStatusCode.OK:
        //                break;

        //            case (int)HttpStatusCode.Unauthorized:
        //                //throw new ErrorTokenExpired();

        //                break;

        //            case 500:
        //                break;

        //            default:
        //                return default(T);
        //        }
        //    }
        //    catch (Exception exception)
        //    {

        //        return default(T);
        //    }
        //}
    }
}