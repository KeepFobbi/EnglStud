using EnglStud.Entities;
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
            db = new ApplicationContext();
            InitializeComponent();
        }

        private void Button_Test1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("qwe");
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AddNewWord_Field.Visibility = Visibility.Visible;
        }

        private void AddRandom_Word_Click(object sender, RoutedEventArgs e)
        {
            List<Word> users = db.Words.ToList();
            string str = "";
            foreach (Word user in users)
            {
                str += "Login: " + user.WordInEnglish + " | ";
                Console.WriteLine(str);
            }
                
        }
    }
}
