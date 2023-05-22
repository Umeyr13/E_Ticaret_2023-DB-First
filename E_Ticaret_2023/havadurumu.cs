using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;



namespace E_Ticaret_2023
{
    public class havadurumu
    {
  

    }
    public class WeatherData
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
    }
    //key_125da1c5248d6b9683ecc95725a6f90c
    //public async Task<WeatherData> GetWeatherData(string apiKey)
    //{
    //    // IP adresini almak için bir HTTP isteği 
    //    string ipUrl = "http://ip-api.com/json";
    //    HttpClient client = new HttpClient();
    //    string ipResponse = await client.GetStringAsync(ipUrl);
    //    dynamic ipData = JsonConvert.DeserializeObject(ipResponse);
    //    string latitude = ipData.lat;
    //    string longitude = ipData.lon;

    //    // Hava durumu verilerini almak için OpenWeatherMap API'sine istek
    //    string weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";
    //    string weatherResponse = await client.GetStringAsync(weatherUrl);
    //    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(weatherResponse);

    //    return weatherData;
    //}
}