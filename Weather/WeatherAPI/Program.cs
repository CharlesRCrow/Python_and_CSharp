// See https://aka.ms/new-console-template for more information
using System.Net.Cache;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAPI.Forecast;
using WeatherAPI.ForecastHourly;

namespace WeatherAPI
{
    class Program
    { 
        static async Task Main(string[] args)
        {                       
            Program program = new Program();
            await program.GetTodoItems();
        }

        private async Task GetTodoItems()
        {
            try
            {
                // get grid id, x and y for forecast               
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.weather.gov/points/29.9007241,-95.5888448");
                
                ProductInfoHeaderValue header = new ProductInfoHeaderValue("WeatherCrow", "1.0");
                ProductInfoHeaderValue comment = new ProductInfoHeaderValue("(+http://www.crowweather.com/WeatherCrow.html)");

                client.DefaultRequestHeaders.UserAgent.Add(header);
                client.DefaultRequestHeaders.UserAgent.Add(comment);

                HttpResponseMessage httpResponseMessage = await client.SendAsync(request);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                //Console.WriteLine(response);


                JObject root = JObject.Parse(response);

                var token = root.SelectToken("properties");
                var xCord = token.SelectToken("gridX");
                var  yCord = token.SelectToken("gridY");
                var  gridID = token.SelectToken("gridId");
                //Console.WriteLine($"{xCord} , {yCord}, {gridID}");  

                HttpRequestMessage forecastRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.weather.gov/gridpoints/{gridID}/{xCord},{yCord}/forecast");
                HttpResponseMessage httpResponseForecast = await client.SendAsync(forecastRequest); 

                string forcastResponse = await httpResponseForecast.Content.ReadAsStringAsync();
                //Console.WriteLine(forcastResponse); 

                JObject forecast = (JObject)JObject.Parse(forcastResponse)["properties"];;
                //var forecastToken = forecast.SelectToken("properties");
                JArray dailyForecast = (JArray) forecast["periods"];      
                //Console.WriteLine(dailyForecast[1]);
                
                Console.WriteLine("Temp \t WindSpeed  \t Forecast");
                foreach (var day in dailyForecast)
                {
                    var dailyTemp = day["temperature"];
                    var dailyName = day["name"];
                    var dailyWindSpeed = day["windSpeed"];
                    var dailyWindDirection = day["windDirection"];
                    var dailyHumidity = day["relativeHumidity"];
                    var detailedForecast = day["detailedForecast"];
                    //Console.WriteLine($"{dailyName} | {dailyTemp}");
                    Console.WriteLine($"{dailyTemp} \t {dailyWindSpeed} \t {dailyWindDirection} \t {dailyName} : {detailedForecast}");
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

