using System.Net; // Для работы с протоколами Интернета

// Определение пространства имен ChatBotService.Model
namespace ChatBot.Model;

/// <summary>
/// Класс ImageService предназначен для получения URL-адресов изображений через API Pixabay.
/// </summary>
public class ImageSevice
{
    /// <summary>
    /// Метод GetImageUrl возвращает URL-адрес изображения на основе запроса.
    /// </summary>
    /// <param name="query">Строка запроса для поиска изображения.</param>
    /// <returns>URL-адрес изображения или null, если изображение не найдено.</returns>
    public string GetImageUrl(string query)
    {
        // Ключ API для доступа к сервису Pixabay
        var apiKey = "42819958-8302ddb9654ca065869e10cb8";

        // Формирование URL-адреса запроса к API Pixabay
        var requestUrl = $"https://pixabay.com/api/?key={apiKey}&q={Uri.EscapeDataString(query)}&image_type=photo";

        // Использование WebClient для отправки запроса и получения ответа
        using (var webClient = new WebClient())
        {
            // Загрузка ответа от сервера в виде строки
            var content = webClient.DownloadString(requestUrl);

            // Преобразование строки ответа в динамический объект JSON
            dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            // Проверка наличия результатов в ответе
            if (jsonResponse.hits.Count > 0)
                // Возврат URL-адреса первого изображения в результатах
                return jsonResponse.hits[0].webformatURL;
        }

        // Возврат null, если изображение не найдено
        return null;
    }
}