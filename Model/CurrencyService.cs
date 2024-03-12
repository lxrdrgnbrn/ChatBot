using System.Net; // Для работы с протоколами Интернета
using System.Xml.Linq; // Для работы с XML

/// <summary>
/// Класс CurrencyService предназначен для получения курсов валют.
/// </summary>
public class CurrencyService
{
    /// <summary>
    /// Метод GetCurrencyRates возвращает строку с актуальными курсами валют.
    /// </summary>
    /// <returns>Строка с актуальными курсами валют.</returns>
    public string GetCurrencyRates()
    {
        using (var webClient = new WebClient())
        {
            // URL API Центробанка РФ для получения курсов валют в формате XML.
            var url = "http://www.cbr.ru/scripts/XML_daily.asp";

            // Загрузка ответа от сервера в виде строки XML.
            var xml = webClient.DownloadString(url);

            // Преобразование строки XML в XDocument для удобной работы с данными.
            var xdoc = XDocument.Parse(xml);

            // Получение элементов XML для USD, EUR и CNY.
            var usd = xdoc.Root.Elements("Valute").FirstOrDefault(v => v.Element("CharCode").Value == "USD");
            var eur = xdoc.Root.Elements("Valute").FirstOrDefault(v => v.Element("CharCode").Value == "EUR");
            var cny = xdoc.Root.Elements("Valute").FirstOrDefault(v => v.Element("CharCode").Value == "CNY");

            // Проверка наличия необходимых данных в ответе.
            if (usd == null || eur == null || cny == null)
                // Возврат сообщения об ошибке, если данные не найдены.
                return "Не удалось получить данные о курсе валют.";

            // Получение курсов валют из элементов XML.
            var usdRate = usd.Element("Value").Value;
            var eurRate = eur.Element("Value").Value;
            var cnyRate = cny.Element("Value").Value;

            // Формирование и возврат строки с актуальными курсами валют.
            return
                $"Вот актуальный курс валют на сегодня:\nДоллар = {usdRate} RUB\nЕвро = {eurRate} RUB\nЮань = {cnyRate} RUB";
        }
    }
}