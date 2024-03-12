namespace ChatBot.Model;

/// <summary>
/// Класс для хранения данных о погоде.
/// </summary>
public class WeatherData
{
    // Основные данные о погоде
    public Main Main { get; set; }
}

/// <summary>
/// Класс для хранения основных данных о погоде.
/// </summary>
public class Main
{
    // Температура
    public float Temp { get; set; }
}