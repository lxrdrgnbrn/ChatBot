using System.IO;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace ChatBot.Model;

/// <summary>
/// Сервис для работы с историей сообщений.
/// </summary>
public class MessageHistoryService
{
    /// <summary>
    /// Сохраняет историю сообщений в файл.
    /// </summary>
    /// <param name="listBox">ListBox, содержащий сообщения.</param>
    /// <param name="fileName">Имя файла, в который будет сохранена история.</param>
    public void SaveHistory(ListBox listBox, string fileName)
    {
        List<MessageData> messages = new();
        foreach (MessageData message in listBox.Items) messages.Add(message);

        var json = JsonConvert.SerializeObject(messages, Formatting.Indented);
        File.WriteAllText(fileName, json);
    }

    /// <summary>
    /// Загружает историю сообщений из файла.
    /// </summary>
    /// <param name="listBox">ListBox, в который будут загружены сообщения.</param>
    /// <param name="fileName">Имя файла, из которого будет загружена история.</param>
    public void LoadHistory(ListBox listBox, string fileName)
    {
        if (!File.Exists(fileName)) return;
        var json = File.ReadAllText(fileName);
        List<MessageData> messages = JsonConvert.DeserializeObject<List<MessageData>>(json);

        listBox.Items.Clear();
        foreach (var message in messages) listBox.Items.Add(message);
    }
}