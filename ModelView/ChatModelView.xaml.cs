using System.Windows;
using System.Windows.Input;
using ChatBot.Model;

namespace ChatBot.ModelView;

public partial class ChatView : Window
{
    private Model.ChatBotService _chatBotService;
    private string _userName;
    private MessageHistoryService _messageHistoryService;

    public ChatView(LoginView mainWindow)
    {
        InitializeComponent();
        _messageHistoryService = new MessageHistoryService();
        _chatBotService = new Model.ChatBotService();
        _userName = mainWindow.UserName;
        _chatBotService.UserName = _userName;
        _messageHistoryService.LoadHistory(MessageList, _userName);
        var startMessageData = new MessageData
        {
            Nick = "Ассистент",
            Text = _chatBotService.GetGreeting(),
            Time = DateTime.Now,
            IsUserMessage = false,
            ImageUrl = _chatBotService.GetImageUrl(MessageBox.Text)
        };
        MessageList.Items.Add(startMessageData);
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void BtnMinimize_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        _messageHistoryService.SaveHistory(MessageList, _userName);
        Application.Current.Shutdown();
    }

    private void MessageBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) SendMessage();
    }

    private void BtnSend_OnClick(object sender, RoutedEventArgs e)
    {
        SendMessage();
    }

    private void SendMessage()
    {
        var newMessageData = new MessageData
        {
            Nick = _userName,
            Text = MessageBox.Text,
            Time = DateTime.Now,
            IsUserMessage = true
        };
        var botMessageData = new MessageData
        {
            Nick = "Ассистент",
            Text = _chatBotService.ReceiveMessage(MessageBox.Text),
            Time = DateTime.Now,
            IsUserMessage = false,
            ImageUrl = _chatBotService.GetImageUrl(MessageBox.Text)
        };
        MessageList.Items.Add(newMessageData);
        MessageList.Items.Add(botMessageData);
        MessageBox.Clear();
        // Прокрутка до последнего элемента
        if (MessageList.Items.Count > 0) MessageList.ScrollIntoView(MessageList.Items[MessageList.Items.Count - 1]);
    }
}