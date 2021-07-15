using EnglStud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnglStud
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        TcpConnection connection;
        public RegWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();
            string rePass = textBoxPassRe.Password.Trim();
            string email = textBoxEmail.Text.ToLower().Trim();

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Not corect";
                textBoxLogin.Foreground = Brushes.Red;
            }
            else
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.Foreground = Brushes.Transparent;
            }
            if (pass.Length < 5)
            {
                textBoxPass.ToolTip = "Not corect";
                textBoxPass.Foreground = Brushes.Red;
            }
            else
            {
                textBoxPass.ToolTip = null;
                textBoxPass.Foreground = Brushes.Transparent;
            }
            if (pass != rePass)
            {
                textBoxPassRe.ToolTip = "Not corect";
                textBoxPassRe.Foreground = Brushes.Red;
            }
            else
            {
                textBoxPassRe.ToolTip = null;
                textBoxPassRe.Foreground = Brushes.Transparent;
            }
            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Not corect";
                textBoxEmail.Foreground = Brushes.Red;
            }
            else
            {
                textBoxEmail.ToolTip = null;
                textBoxEmail.Foreground = Brushes.Transparent;
            }
            if (textBoxLogin.ToolTip == null && textBoxPass.ToolTip == null && textBoxPassRe.ToolTip == null && textBoxEmail.ToolTip == null)
            {
                User user = new User(0, login, pass, email);
                string JsonString = System.Text.Json.JsonSerializer.Serialize(user);

                connection = new TcpConnection(JsonString);

                //Thread ThreadConnection = new Thread(new ThreadStart(connection.SendToServer));
                //ThreadConnection.Start();
                connection.SendToServer();
                if (connection.Response.message == "ok")
                {
                    MainWindow main = new MainWindow(connection.Response.Id);
                    main.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Is there something wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBoxLogin.Foreground = Brushes.Red;
                    textBoxPass.Foreground = Brushes.Red;
                    textBoxPassRe.Foreground = Brushes.Red;
                    textBoxEmail.Foreground = Brushes.Red;
                }
            }
        }
    }
}
