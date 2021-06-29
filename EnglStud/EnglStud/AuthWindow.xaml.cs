using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EnglStud.Entities;
using Newtonsoft.Json;

namespace EnglStud
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            TcpConnection tcpConnection = new TcpConnection(null);
            tcpConnection.SendToServer();
        }

        private void Button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            Close();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            


            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //Close();
        }
    }
}
