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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnglStud
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            List<User> users = db.Users.ToList();
            string str = "";
            foreach (User user in users)
                str += "Login: " + user.Login + " | ";

            exampleText.Text = str;
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();
            string rePass = textBoxPassRe.Password.Trim();
            string email = textBoxEmail.Text.ToLower().Trim();



            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Not corect";
                textBoxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                textBoxPass.ToolTip = "Not corect";
                textBoxPass.Background = Brushes.DarkRed;
            }
            else if (pass != rePass)
            {
                textBoxPassRe.ToolTip = "Not corect";
                textBoxPassRe.Background = Brushes.DarkRed;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Not corect";
                textBoxEmail.Background = Brushes.DarkRed;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;

                textBoxPass.ToolTip = "";
                textBoxPass.Background = Brushes.Transparent;

                textBoxPassRe.ToolTip = "";
                textBoxPassRe.Background = Brushes.Transparent;

                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;

                MessageBox.Show("Its Ok!");
                User user = new User(login, email, pass);

                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
