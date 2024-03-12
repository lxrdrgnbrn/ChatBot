using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatBot.ModelView;

public partial class LoginView : Window
{
    public string UserName { get; set; }

    public LoginView()
    {
        InitializeComponent();
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
        Application.Current.Shutdown();
    }

    private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtUser.Text))
        {
            TxtUser.BorderBrush = Brushes.Tomato;
        }
        else
        {
            UserName = TxtUser.Text;
            var chatWindow = new ChatView(this);
            chatWindow.Show();
            Hide();
        }
    }

    private void TxtUser_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtUser.BorderBrush = Brushes.Black;
    }
}