using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;

namespace ContactAppXamarin.Services
{
    public interface IHttpClientUris
    {
        HttpClient ContactApi();
        
    }
    public class HttpClientUris : IHttpClientUris
    {
        public HttpClient ContactApi()
        {
            // Crear el cliente HTTP con la configuración para ignorar la validación del certificado SSL
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            HttpClient _http = new HttpClient(handler);
            _http.BaseAddress = new Uri("https://192.168.0.6:7071/");
            return _http;
        }
    }
}
