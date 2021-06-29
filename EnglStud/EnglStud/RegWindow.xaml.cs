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
                textBoxLogin.Background = Brushes.Red;
            }
            else
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.Background = Brushes.Transparent;
            }
            if (pass.Length < 5)
            {
                textBoxPass.ToolTip = "Not corect";
                textBoxPass.Background = Brushes.Red;
            }
            else
            {
                textBoxPass.ToolTip = null;
                textBoxPass.Background = Brushes.Transparent;
            }
            if (pass != rePass)
            {
                textBoxPassRe.ToolTip = "Not corect";
                textBoxPassRe.Background = Brushes.Red;
            }
            else
            {
                textBoxPassRe.ToolTip = null;
                textBoxPassRe.Background = Brushes.Transparent;
            }
            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Not corect";
                textBoxEmail.Background = Brushes.Red;
            }
            else
            {
                textBoxEmail.ToolTip = null;
                textBoxEmail.Background = Brushes.Transparent;
            }
            if (textBoxLogin.ToolTip == null && textBoxPass.ToolTip == null && textBoxPassRe.ToolTip == null && textBoxEmail.ToolTip == null)
            {
                User user = new User(0, login, pass, email);
                string JsonString = System.Text.Json.JsonSerializer.Serialize(user);

                MessageBox.Show(JsonString);
                
                TcpConnection connection = new TcpConnection(JsonString);

                Thread ThreadConnection = new Thread(new ThreadStart(connection.SendToServer));
                ThreadConnection.Start();
                //connection.SendToServer(JsonString);
            }
        }
    }
}
