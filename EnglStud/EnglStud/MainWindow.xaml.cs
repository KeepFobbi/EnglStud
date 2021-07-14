using EnglStud.Entities;
using Nancy.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        List<Word> words;
        public MainWindow()
        {
            db = new ApplicationContext();
            words = db.Words.ToList();
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

        private void AddRandom_Word_Click(object sender, RoutedEventArgs e) // to do this
        {
            Random rnd = new Random();
            Word word = db.Words.Find(rnd.Next(2, words.Count()));

            CollapseAllElements();
            Choose_Random_Word_Field.Visibility = Visibility.Visible;
            Engl_Word_Choose.Text = word.WordInEnglish;
            Translate_Word_Choose.Text = word.Translation;
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e) // Test CollapseAllElements();
        {
            CollapseAllElements();
        }

        private void CheckUserWord_Add_Click(object sender, RoutedEventArgs e)
        {
            //string str = EnteredUserWord_TextBox.Text;
            //string kruc = TranslateText1(str);
            //Console.WriteLine(kruc);
        }

        //public string TranslateText(string input)
        //{
        //    TranslationServiceClient client = TranslationServiceClient.Create();
        //    TranslateTextRequest request = new TranslateTextRequest
        //    {
        //        Contents = { "It is raining." },
        //        TargetLanguageCode = "fr-FR",
        //        Parent = new ProjectName(projectId).ToString()
        //    };
        //    TranslateTextResponse response = client.TranslateText(request);
        //    // response.Translations will have one entry, because request.Contents has one entry.
        //    Translation translation = response.Translations[0];
        //    Console.WriteLine($"Detected language: {translation.DetectedLanguageCode}");
        //    Console.WriteLine($"Translated text: {translation.TranslatedText}");
        //}

        //public string TranslateText1(string input)
        //{
        //    // Set the language from/to in the url (or pass it into this function)
        //    string url = String.Format
        //    ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
        //     "en", "es", Uri.EscapeUriString(input));
        //    HttpClient httpClient = new HttpClient();
        //    string result = httpClient.GetStringAsync(url).Result;

        //    // Get all json data
        //    var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);

        //    // Extract just the first array element (This is the only data we are interested in)
        //    var translationItems = jsonData[0];

        //    // Translation Data
        //    string translation = "";

        //    // Loop through the collection extracting the translated objects
        //    foreach (object item in translationItems)
        //    {
        //        // Convert the item array to IEnumerable
        //        IEnumerable translationLineObject = item as IEnumerable;

        //        // Convert the IEnumerable translationLineObject to a IEnumerator
        //        IEnumerator translationLineString = translationLineObject.GetEnumerator();

        //        // Get first object in IEnumerator
        //        translationLineString.MoveNext();

        //        // Save its value (translated text)
        //        translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
        //    }

        //    // Remove first blank character
        //    if (translation.Length > 1) { translation = translation.Substring(1); };

        //    // Return translation
        //    return translation;
        //}
    }
}