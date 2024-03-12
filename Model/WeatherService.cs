using System.Net;
using ChatBot.Model;
using Newtonsoft.Json;

public class WeatherService
{
    // Ключ API для доступа к сервису погоды
    private string apiKey = "a195b89fb236a6367c8ee98f5d3c7fbd";

    // ID города для получения погоды
    private string cityid = "2025339";

    /// <summary>
    /// Получает текущую погоду для заданного города.
    /// </summary>
    /// <returns>Строка с информацией о температуре в городе.</returns>
    public string GetWeather()
    {
        // Создаем новый экземпляр WebClient для отправки HTTP-запроса
        using (var webClient = new WebClient())
        {
            // Формируем URL для запроса погоды
            var url =
                $"https://api.openweathermap.org/data/2.5/weather?id={cityid}&appid={apiKey}&units=metric&lang=ru";

            // Отправляем запрос и получаем ответ в формате JSON
            var json = webClient.DownloadString(url);

            // Десериализуем JSON в объект WeatherData
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);

            // Возвращаем строку с информацией о температуре
            return $"Температура в Чите: {weatherData.Main.Temp}°C";
        }
    }
}