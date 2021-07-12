using EnglStud.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
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

        public void CollapseAllElements()
        {
            foreach (StackPanel stack in MainFeild.Children)
                stack.Visibility = Visibility.Collapsed;
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

        private void AddNewWord_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapseAllElements();
            AddNewWord_Field.Visibility = Visibility.Visible;
        }

        private void StartTask_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapseAllElements();
            StartTask_Field.Visibility = Visibility.Visible;
        }

        private void AddRandom_Word_Click(object sender, RoutedEventArgs e) // to do
        {
            Random rnd = new Random();
            List<Word> words = db.Words.ToList();
            Word word = db.Words.Find(rnd.Next(2, words.Count()));

            CollapseAllElements();
            Choose_Random_Word_Field.Visibility = Visibility.Visible;
            Engl_Word_Choose.Text = word.WordInEnglish;
            Translate_Word_Choose.Text = word.Translation;
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e) // Test
        {
            CollapseAllElements();
        }
    }
}
