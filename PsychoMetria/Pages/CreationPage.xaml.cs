using PsychoMetria.Windows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreationPage.xaml
    /// </summary>
    public partial class CreationPage : Page
    {
        public CreationPage()
        {
            InitializeComponent();
            BasicLoader();
            List<string> KZA = new List<string>();
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsdssssdsdsdsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssdsdsdsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            KZA.Add("sdsdsds");
            ScalesList.ItemsSource = KZA;
            QuestionsList.ItemsSource = KZA;
        }

        private void BasicLoader()
        {
            MainInfoPanel.Visibility = Visibility.Collapsed;
            ScalesPanel.Visibility = Visibility.Collapsed;
            QuestionsPanel.Visibility = Visibility.Collapsed;
        }


        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            var owned_windows = System.Windows.Application.Current.MainWindow.OwnedWindows;
            foreach (var window in owned_windows)
            {
                (window as Window).Close();
            }
            NavigationService.Navigate(new MainPage());
        }

        private void CreateQuestionnaireBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ScaleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CleanAllBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Функции для Шкал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void AddNewScaleBut_Click(object sender, RoutedEventArgs e)
        {
            ScaleWindow scaleWindow = new ScaleWindow();
        }
        private void EditScaleBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteScaleBut_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Перемешать вопросы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool _isMixUp = false;
        private void MixUpQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _isMixUp = true;

            App.Current.Resources["DefaultStackPanel"] = App.Current.Resources["UnvisibleStackPanel"];
        }

        private void MixUpQuestionsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _isMixUp = false;
            App.Current.Resources["DefaultStackPanel"] = App.Current.Resources["VisibleStackPanel"];
        }

        /// <summary>
        /// Режимы скрытия и раскрытия информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool _isMainInfoOpen = false;
        private void MainInfoVisibilityBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isMainInfoOpen)
            {
                MainInfoPanel.Visibility = Visibility.Collapsed;
                _isMainInfoOpen = false;
                MainInfoVisibilityText.Text = "(+)";
                MainInfoVisibilityBut.ToolTip = "Раскрыть основную информацию";
            }
            else
            {
                MainInfoPanel.Visibility = Visibility.Visible;
                _isMainInfoOpen = true;
                MainInfoVisibilityText.Text = "(-)";
                MainInfoVisibilityBut.ToolTip = "Скрыть основную информацию";
            }
        }

        bool _isScalesOpen = false;
        private void ScalesVisibilityBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isScalesOpen)
            {
                ScalesPanel.Visibility = Visibility.Collapsed;
                _isScalesOpen = false;
                ScalesVisibilityText.Text = "(+)";
                ScalesVisibilityBut.ToolTip = "Раскрыть информацию о предмете тестирования";
            }
            else
            {
                ScalesPanel.Visibility = Visibility.Visible;
                _isScalesOpen = true;
                ScalesVisibilityText.Text = "(-)";
                ScalesVisibilityBut.ToolTip = "Скрыть информацию о предмете тестирования";
            }
        }

        bool _isQuestionsOpen = false;
        private void QuestionsVisibilityBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isQuestionsOpen)
            {
                QuestionsPanel.Visibility = Visibility.Collapsed;
                _isQuestionsOpen = false;
                QuestionsVisibilityText.Text = "(+)";
                QuestionsVisibilityBut.ToolTip = "Раскрыть информацию о вопросах тестирования";
            }
            else
            {
                QuestionsPanel.Visibility = Visibility.Visible;
                _isQuestionsOpen = true;
                QuestionsVisibilityText.Text = "(-)";
                QuestionsVisibilityBut.ToolTip = "Скрыть информацию о вопросах тестирования";
            }
        }
    }
}
