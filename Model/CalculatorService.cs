using MathNet.Symbolics; // Используем пространство имен MathNet.Symbolics для работы с математическими выражениями
using
    System.Text.RegularExpressions; // Используем пространство имен System.Text.RegularExpressions для работы с регулярными выражениями

/// <summary>
/// CalculatorService представляет собой класс для обработки регулярных математических выражений
/// </summary>
public class CalculatorService // Определение класса CalculatorService
{
    /// <summary>
    /// Метод для вычисления математического выражения.
    /// </summary>
    /// <param name="expression">Строка, содержащая математическое выражение для вычисления.</param>
    /// <returns>Строка с результатом вычисления выражения или сообщение об ошибке, если выражение не может быть обработано.</returns>
    public string
        EvaluateExpression(
            string expression) // Определение метода EvaluateExpression, который принимает строку с математическим выражением и возвращает результат его вычисления
    {
        try // Начало блока обработки исключений
        {
            var e = Infix.ParseOrUndefined(expression); // Парсинг входного выражения в формате инфиксной записи
            var result = Evaluate.Evaluate(null, e); // Вычисление результата выражения
            return
                $"{expression} = {((double)result.RealValue).ToString().TrimEnd('0').TrimEnd('.')}"; // Возвращение строки с результатом вычисления
        }
        catch (Exception) // Обработка исключений
        {
            // Если возникло исключение, возвращаем сообщение об ошибке
            return "Я не могу обработать это выражение.";
        }
    }

    /// <summary>
    /// Метод для проверки, является ли входная строка математическим выражением.
    /// </summary>
    /// <param name="expression">Строка, которую необходимо проверить.</param>
    /// <returns>Возвращает true, если строка является математическим выражением, и false в противном случае.</returns>
    public bool
        IsMathExpression(
            string expression) // Определение метода IsMathExpression, который проверяет, является ли входная строка математическим выражением
    {
        // Проверка, содержит ли строка символы, которые могут быть частью математического выражения
        var isMathExpression = Regex.IsMatch(expression,
            @"^[\d\+\-\*\/\(\)\.\s\^]|cos\(\S+\)|sin\(\S+\)|tan\(\S+\)|exp\(\S+\)|ln\(\S+\)$");

        if (isMathExpression) // Если строка содержит символы математического выражения
        {
            // Если строка содержит функции, проверяем, является ли содержимое скобок допустимым математическим выражением
            var functionMatches = Regex.Matches(expression, @"(cos|sin|tan|exp|ln)\((.*?)\)");
            foreach (Match match in functionMatches) // Перебор всех найденных функций
            {
                var functionContent = match.Groups[2].Value; // Получение содержимого скобок функции
                if (!double.TryParse(functionContent, out _)) // Если содержимое скобок не является числом
                {
                    // Если содержимое скобок не является числом, проверяем, является ли оно допустимым математическим выражением
                    var e = Infix.ParseOrUndefined(functionContent);
                    if (e == null) // Если содержимое скобок не является допустимым математическим выражением
                        return false; // Возвращаем false, так как строка не является математическим выражением
                }
            }
        }

        return isMathExpression; // Возвращаем результат проверки, является ли строка математическим выражением
    }
}