using System.ComponentModel;

// Пространство имен для модели чат-бота
namespace ChatBot.Model;

/// <summary>
/// Класс MessageData представляет данные сообщения в чате.
/// </summary>
public class MessageData : INotifyPropertyChanged
{
    // Приватные поля класса
    private string _nick; // Никнейм отправителя сообщения
    private string _text; // Текст сообщения
    private DateTime _time; // Время отправки сообщения
    private bool _isUserMessage; // Флаг, указывающий, является ли сообщение сообщением пользователя
    private string _imageUrl; // URL изображения, прикрепленного к сообщению

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public MessageData()
    {
        _nick = "";
        _text = "";
        _time = DateTime.Now;
        _isUserMessage = false;
        _imageUrl = null;
    }

    /// <summary>
    /// Свойство Nick представляет никнейм отправителя сообщения.
    /// </summary>
    public string Nick
    {
        get => _nick;
        set
        {
            _nick = value;
            OnPropertyChanged("Nick");
        }
    }

    /// <summary>
    /// Свойство Text представляет текст сообщения.
    /// </summary>
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged("Text");
        }
    }

    /// <summary>
    /// Свойство Time представляет время отправки сообщения.
    /// </summary>
    public DateTime Time
    {
        get => _time;
        set
        {
            _time = value;
            OnPropertyChanged("Time");
        }
    }

    /// <summary>
    /// Свойство IsUserMessage представляет флаг, указывающий, является ли сообщение сообщением пользователя.
    /// </summary>
    public bool IsUserMessage
    {
        get => _isUserMessage;
        set
        {
            _isUserMessage = value;
            OnPropertyChanged("IsUserMessage");
        }
    }

    /// <summary>
    /// Свойство ImageUrl представляет URL изображения, прикрепленного к сообщению.
    /// </summary>
    public string ImageUrl
    {
        get => _imageUrl;
        set
        {
            _imageUrl = value;
            OnPropertyChanged("ImageUrl");
        }
    }

    /// <summary>
    /// Событие, которое вызывается при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Метод для вызова события PropertyChanged.
    /// </summary>
    /// <param name="propertyName">Имя свойства, которое изменилось.</param>
    protected virtual void OnPropertyChanged(string propertyName)

    {
        // Эта строка кода вызывает событие PropertyChanged, если оно не равно null.
        // Событие PropertyChanged информирует систему об изменении свойства объекта, что в свою очередь может вызвать обновление пользовательского интерфейса.
        // В данном случае, событие вызывается для свойства с именем, переданным в параметре "propertyName".
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}