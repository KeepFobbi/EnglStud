﻿using System;
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
        TcpConnection connection;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            Close();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();

            User user = new User(1, login, pass);
            string JsonString = System.Text.Json.JsonSerializer.Serialize(user); // JsonSerializer

            connection = new TcpConnection(JsonString);

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
            }


            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //Close();
        }
    }
}
