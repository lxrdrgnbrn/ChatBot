using System.Globalization;
using System.Windows;
using System.Windows.Data;

/// <summary>
/// Конвертер, преобразующий строку в значение Visibility.
/// Если строка пуста или null, возвращает Visibility.Collapsed, иначе - Visibility.Visible.
/// </summary>
public class StringToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Преобразует строку в значение Visibility.
    /// </summary>
    /// <param name="value">Строка для преобразования.</param>
    /// <param name="targetType">Целевой тип преобразования (не используется).</param>
    /// <param name="parameter">Дополнительный параметр (не используется).</param>
    /// <param name="culture">Информация о культуре (не используется).</param>
    /// <returns>Visibility.Collapsed, если строка пуста или null, иначе - Visibility.Visible.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible;
    }

    /// <summary>
    /// Обратное преобразование не поддерживается, будет вызвано исключение NotImplementedException.
    /// </summary>
    /// <param name="value">Значение для обратного преобразования.</param>
    /// <param name="targetType">Целевой тип преобразования.</param>
    /// <param name="parameter">Дополнительный параметр.</param>
    /// <param name="culture">Информация о культуре.</param>
    /// <returns>Не возвращается, так как метод вызывает исключение NotImplementedException.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}