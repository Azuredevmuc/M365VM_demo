using System.Threading.Tasks;


using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;


using System;
using System.Linq;
using System.Net;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated
{
    public class GetOpenWeatherData
    {
        private readonly ILogger _logger;
        static HttpClient client = new HttpClient();

        public GetOpenWeatherData(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetOpenWeatherData>();
        }

        [Function("GetOpenWeatherData")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            //Get Wether Data of Munich
            string apiKey = Environment.GetEnvironmentVariable("OpenWeatherAPIKey");
            var ApiResponse = await client.GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lat=48.185777668797314&lon=11.56213033944366&exclude=minutely&appid={apiKey}&units=metric");
            var result = await ApiResponse.Content.ReadAsStringAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(result);
            return response;
        }
    }
}