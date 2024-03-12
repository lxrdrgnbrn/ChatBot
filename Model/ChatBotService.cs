using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ChatBot.Model;

/// <summary>
/// Класс ChatBotService представляет собой модель чат-бота.
/// </summary>
public class ChatBotService
{
    private string userName; // Имя пользователя
    private string userMessage; // Сообщение пользователя
    private CalculatorService _calculatorService; // Калькулятор для вычисления математических выражений
    private WeatherService weatherService; // Сервис для получения прогноза погоды
    private CurrencyService currencyService; // Сервис для получения курсов валют
    private ImageSevice _imageSevice; // Сервис для получения изображения из интернета

    /// <summary>
    /// Конструктор класса ChatBotService.
    /// </summary>
    /// <param name="userName">Имя пользователя</param>
    public ChatBotService()
    {
        userName = "";
        userMessage = "";
        _calculatorService = new CalculatorService();
        weatherService = new WeatherService();
        currencyService = new CurrencyService();
        _imageSevice = new ImageSevice();
    }

    public string UserName
    {
        get => userName;
        set => userName = value;
    }


    /// <summary>
    /// Метод для получения сообщения от пользователя и возвращения ответа.
    /// </summary>
    /// <param name="message">Сообщение пользователя</param>
    /// <returns>Ответ бота</returns>
    public string ReceiveMessage(string message)
    {
        userMessage = message.ToLower();
        try
        {
            if (userMessage.Contains("помощь"))
                return GetHelp();
            else if (_calculatorService.IsMathExpression(userMessage))
                return ProcessMathExpression();
            else if (userMessage.Contains("привет") || userMessage.Contains("здравствуй"))
                return GetGreeting();
            else if (userMessage.Contains("найди"))
                return SearchWeb(userMessage);
            else if (userMessage.StartsWith("изображение "))
                return "";
            else if (userMessage.Contains("погода"))
                return GetWeatherForecast();
            else if (userMessage.Contains("курс"))
                return GetCurrencyRates();
            else if (userMessage.Contains("браузер") || userMessage.Contains("яндекс"))
                return OpenBrowser("https://ya.ru/");
            else if (userMessage.Contains("ютуб"))
                return OpenBrowser("https://www.youtube.com/");
            else if (userMessage.Contains("вконтакте"))
                return OpenBrowser("https://vk.com/");
            else if (userMessage.Contains("время") || userMessage.Contains("дата"))
                return GetDateTime();
            else if (userMessage.Contains("консоль"))
                return OpenConsole();
            else if (userMessage.Contains("блокнот"))
                return OpenNotepad();
            else
                return "Я вас не совсем понимаю...\n" +
                       "Чтобы узнать, что я умею, напишите \"помощь\"";
        }
        catch (Exception)
        {
            return "Произошла ошибка.";
        }
    }

    /// <summary>
    /// Метод для обработки математического выражения.
    /// </summary>
    /// <returns>Результат вычисления выражения</returns>
    private string ProcessMathExpression()
    {
        return _calculatorService.EvaluateExpression(userMessage);
    }

    /// <summary>
    /// Метод для получения прогноза погоды.
    /// </summary>
    /// <returns>Прогноз погоды</returns>
    private string GetWeatherForecast()
    {
        return weatherService.GetWeather();
    }

    /// <summary>
    /// Метод для получения курсов валют.
    /// </summary>
    /// <returns>Курсы валют</returns>
    private string GetCurrencyRates()
    {
        return currencyService.GetCurrencyRates();
    }

    /// <summary>
    /// Метод для открытия браузера по умолчанию.
    /// </summary>
    /// <param name="url">URL-адрес, который нужно открыть в браузере</param>
    public string OpenBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            return "Будет сделанно";
        }
        catch
        {
            return "Произошла ошибка";
        }
    }

    /// <summary>
    /// Метод для обработки сообщения пользователя и открытия браузера с поисковым запросом.
    /// </summary>
    /// <param name="message">Сообщение пользователя</param>
    public string SearchWeb(string message)
    {
        const string searchCommand = "найди ";
        try
        {
            if (message.StartsWith(searchCommand))
            {
                var query = message.Substring(searchCommand.Length);
                var url = "https://yandex.ru/search/?text=" + Uri.EscapeDataString(query);
                return OpenBrowser(url);
            }
        }
        catch
        {
            return "Произошла ошибка";
        }

        return "Я вас не совсем понимаю...\n" +
               "Чтобы узнать, что я умею, напишите \"помощь\"";
    }

    /// <summary>
    /// Метод для обработки сообщения пользователя и получения url изображения.
    /// </summary>
    /// <param name="message">Сообщение пользователя</param>
    /// <returns>Url изображения</returns>
    public string GetImageUrl(string message)
    {
        const string imageCommand = "изображение ";
        if (message.StartsWith(imageCommand))
        {
            // Извлекаем запрос из сообщения пользователя
            var query = message.Substring(imageCommand.Length);
            if (query != "")
            {
                // Получаем URL изображения
                var url = _imageSevice.GetImageUrl(query);

                // Возвращаем URL изображения
                return url;
            }
        }

        return null;
    }

    /// <summary>
    /// Метод для приветсвования пользователя.
    /// </summary>
    /// <returns>Текст приветсвования</returns>
    public string GetGreeting()
    {
        return $"Привет {userName}!\n" +
               $"Я ассистент - твой виртуальный помощник!\n" +
               $"Чтобы узнать, что я умею, напишите \"помощь\"";
    }

    /// <summary>
    /// Метод для получения актуальных даты и времени.
    /// </summary>
    /// <returns>Актуальные дата и время</returns>
    private string GetDateTime()
    {
        var dateTime = DateTime.Now.ToString($"dd.MM.yy HH:mm");
        return $"Сейчас {dateTime}";
    }

    /// <summary>
    /// Метод для открытия консоли.
    /// </summary>
    private string OpenConsole()
    {
        try
        {
            Process.Start(new ProcessStartInfo("cmd.exe") { UseShellExecute = true });
            return "Опять работа";
        }
        catch
        {
            return "Произошла ошибка";
        }
    }

    /// <summary>
    /// Метод для открытия блокнота.
    /// </summary>
    private string OpenNotepad()
    {
        try
        {
            Process.Start(new ProcessStartInfo("notepad.exe") { UseShellExecute = true });
            return "Да да";
        }
        catch
        {
            return "Произошла ошибка";
        }
    }

    /// <summary>
    /// Метод для получения справки о возможностях бота.
    /// </summary>
    /// <returns>текст справки о возможностях бота</returns>
    private string GetHelp()
    {
        return "Я умею выполнять следующие задачи:\n\n" +
               "1. Обработка математических выражений: Если ваше сообщение является математическим выражением, \nя могу решить его.\n\n" +
               "2. Открытие веб-сайтов: Если ваше сообщение содержит \"ютуб\" или \"вконтакте\", \nя открою вам эти сайты\n\n" +
               "3. Поиск в Интернете: Если ваше сообщение начинается с \"найди\", \nя выполняю поиск по вашему запросу в Интернете.\n\n" +
               "4. Получение прогноза погоды: Если ваше сообщение содержит \"погода\", \nя выведу вам прогноз погоды в Чите.\n\n" +
               "5. Получение курсов валют: Если ваше сообщение содержит \"курс\", \nя выведу вам актуальные курсы валют (евро, доллар, юань).\n\n" +
               "6. Открытие браузера: Если ваше сообщение содержит \"браузер\" или \"яндекс\", \nя открою вам браузер.\n\n" +
               "7. Получение текущего времени и даты: Если ваше сообщение содержит \"время\" или \"дата\", \nя выведу текущее время и дату.\n\n" +
               "8. Открытие консоли: Если ваше сообщение содержит \"консоль\", \nя открою консоль.\n\n" +
               "9. Открытие блокнота: Если ваше сообщение содержит \"блокнот\", \nя открою блокнот.\n\n" +
               "10. Показ изображения: Если ваше сообщение начинается с \"изображение\", \nя выведу вам изображение по вашему запросую.\n\n" +
               "Так же я сохраняю вашу историю сообщений! \nПоэтому вы можете не боятся потерять что-нибудь важное)";
    }
}