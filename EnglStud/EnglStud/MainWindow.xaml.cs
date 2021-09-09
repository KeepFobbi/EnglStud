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
        TcpConnection connection;
        ApplicationContext db;
        List<Word> words;
        List<int> learnedWords = new List<int>();
        Stack<int> studingWords = new Stack<int>();
        Word word;
        int UserId;

        public MainWindow() // for test
        {
            UserId = 1;
            db = new ApplicationContext();
            words = db.Words.ToList();

            Response_Event @event = new Response_Event(UserId);
            string JsonString = System.Text.Json.JsonSerializer.Serialize(@event); // JsonSerializer

            connection = new TcpConnection(JsonString);
            connection.SendToServer();

            foreach (var item in connection.wordsResponse.wordsList) // Words list to 2x List
            {
                if (item.IdKnowWords != 0)
                    learnedWords.Add(item.IdKnowWords);
                if (item.IdWordUnStudy != 0)
                    studingWords.Push(item.IdWordUnStudy);
            }

            InitializeComponent();
        }
        public MainWindow(int UserId) //Release
        { 
            this.UserId = UserId;
            db = new ApplicationContext();
            words = db.Words.ToList();
            InitializeComponent();
        }

        #region Element Work
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

        #endregion

        private void StartTask_MouseUp(object sender, MouseButtonEventArgs e)  // code 37 UI
        {
            CollapseAllElements();
            StartTask_Field.Visibility = Visibility.Visible;

            

            
            word = db.Words.Find(studingWords.Pop()); // engl word for studing

            Exersice_Choose_Translate_Word(word); // call exercise function <-
        }

        #region Ex1 region Word selecte translate
        private void Exersice_Choose_Translate_Word(Word choseWord) // EX 1
        {
            List<TextBlock> textBlocks_ChoWord = Ex_Spell_Word.Children.OfType<TextBlock>().ToList();
            textBlocks_ChoWord.RemoveAt(0);

            Studing_word_TxtBlock_Main.Text = choseWord.WordInEnglish;
            Studing_word_TxtBlock_Main.Tag = choseWord.Translation;
            TestBox.Text = choseWord.Translation;

            int i = 0;
            Random rnd = new Random();
            int randI = rnd.Next(0, textBlocks_ChoWord.Count); ;
            foreach (TextBlock item in textBlocks_ChoWord)
            {
                word = db.Words.Find(rnd.Next(2, words.Count()));
                item.Text = word.Translation;

               
                if (i == randI)
                {
                    item.Text = choseWord.Translation;
                }
                i++;
            }

            List<TextBlock> s = textBlocks_ChoWord.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            
            // to do
        }

        private void Studing_word_TxtBlock_0_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var temp = sender as TextBlock;
            if (Studing_word_TxtBlock_Main.Tag.ToString() == temp.Text)
            {
                MessageBox.Show("+"); // true to do
            }
            else
            {
                // false to do
            }
        }

        private void Studing_word_TxtBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var temp = sender as TextBlock;
            temp.Foreground = Brushes.Red;
        }

        private void Studing_word_TxtBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            var temp = sender as TextBlock;
            temp.Foreground = Brushes.Black;
        }

        #endregion

        #region Ex2 region translate select word

        private void Exersice_Choose_Engl_Word_Test(object sender, RoutedEventArgs e) // TEST
        {
            word = db.Words.Find(studingWords.Pop()); // engl word for studing
            Exersice_Choose_Eng_To_Translate(word);
        }

        private void Exersice_Choose_Eng_To_Translate(Word choseWord) // EX 2
        {
            List<TextBlock> textBlocks_ChoWord = Ex_Spell_Word.Children.OfType<TextBlock>().ToList();
            textBlocks_ChoWord.RemoveAt(0);

            Studing_word_TxtBlock_Main.Text = choseWord.Translation;
            Studing_word_TxtBlock_Main.Tag = choseWord.WordInEnglish;
            TestBox.Text = choseWord.WordInEnglish;

            int i = 0;
            Random rnd = new Random();
            int randI = rnd.Next(0, textBlocks_ChoWord.Count); ;
            foreach (TextBlock item in textBlocks_ChoWord)
            {


                word = db.Words.Find(rnd.Next(2, words.Count()));
                item.Text = word.WordInEnglish;


                if (i == randI)
                {
                    item.Text = choseWord.WordInEnglish;
                }
                i++;
            }

            List<TextBlock> s = textBlocks_ChoWord.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToList();

            // to do
        }

        #endregion

        #region Ex3 region translate -> spell engl word



        #endregion

        private void Exersice_Spell_EnglWord(Word word)
        {
            Studing_word_TxtBlock_Main.Text = word.Translation;

            char[] charArrey = word.WordInEnglish.ToCharArray();

            // to do
        }

        private void Exercise_next_word_Click(object sender, RoutedEventArgs e) // btn next word
        {
            if (studingWords.Count != 0)
            {
                word = db.Words.Find(studingWords.Pop());
                Studing_word_TxtBlock_Main.Text = word.WordInEnglish;
            }
        }

        private void Random_Word_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllElements();
            Choose_Random_Word_Field.Visibility = Visibility.Visible;

            Random rnd = new Random();
            word = db.Words.Find(rnd.Next(2, words.Count()));

            
            Engl_Word_Choose.Text = word.WordInEnglish;
            Translate_Word_Choose.Text = word.Translation;
        }

        private void AddRandom_Word_Click(object sender, RoutedEventArgs e) // code 36
        {
            WordsToServer words = new WordsToServer(0, word.Id, UserId); // + new word
            string JsonString = System.Text.Json.JsonSerializer.Serialize(words); // JsonSerializer

            connection = new TcpConnection(JsonString);
            Thread thread = new Thread(new ThreadStart(connection.SendToServer));
            thread.Start();
        }















        // -----------------------------Deep Code------------------------------- //


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