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

using PsychoMetria.Materials.Models;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для QuestionnairePage.xaml
    /// </summary>
    public partial class QuestionnairePage : Page
    {
        private Questionnaire _openedQuestionnaire;
        private Testing _workingTest; 
        public QuestionnairePage(Questionnaire questionnaire) //Передать идентификаторы теста
        {
            InitializeComponent();
            _openedQuestionnaire = questionnaire;
            BasicLoader();
        }

        private void BasicLoader()
        {
            _workingTest = new Testing(_openedQuestionnaire.QuestionsCount(), _openedQuestionnaire);
            QuestionnaireBlock.Text = _openedQuestionnaire.Name;
            QuestionnaireTotal.Text = _openedQuestionnaire.QuestionsCount();
            /*if (_openedQuestionnaire.EstimateType.Estimate_Id == 1)
            {
                QuestionnaireBlock.Text = _openedQuestionnaire.Name;
                QuestionnaireTotal.Text = _openedQuestionnaire.QuestionsCount();
            }
            else
            {
                QuestionProgressPanel.Orientation = Orientation.Horizontal;
                QuestionProgressPanel.HorizontalAlignment = HorizontalAlignment.Center;
                QuestionProgressPanel.VerticalAlignment = VerticalAlignment.Center;
                QuestionnairePercent.Visibility = Visibility.Visible;
                QuestionnaireTotal.Visibility = Visibility.Collapsed;
                AdjacentPlackBlock.Visibility = Visibility.Collapsed;
            }*/

            if (_workingTest.QuestionCount == 0)
            {
                MessageBox.Show("По всей видимости, вы открыли сломанный/неработающий тест!", "Ошибка при получении результатов теста", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            nextQuestion();
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }


        /// <summary>
        /// Методы непосредственного тестирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnswerBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Answer;
            _workingTest.TakeAnswer(selected_item);
            nextQuestion();
        }

        private void nextQuestion()
        {
            var take_package = _workingTest.NextQuestion();

            if (take_package == null) //Тесты кончились
            {
                NavigationService.Navigate(new ResultsPage(_workingTest));
                return;
            }

            if (take_package.Item1.Question_Title != null)
            {
                QuestionBlock.Text = take_package.Item1.Question_Title + ".\n" + take_package.Item1.Question_Text;
            }
            else
            {
                QuestionBlock.Text = take_package.Item1.Question_Text;
            }

            AnswersList.ItemsSource = null;
            AnswersList.ItemsSource = take_package.Item2;
            QuestionnairePassed.Text = _workingTest.QuestionProgress.ToString();
        }
    }
}
