using PsychoMetria.Materials.Models;
using PsychoMetria.Materials.Models.SupportModels;
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
        public Questionnaire OpenedQuestionnaire = new Questionnaire();
        private bool _isCreatingPage = true;
        public CreationPage() //Если идет создание теста с нуля
        {
            InitializeComponent();
            BasicLoader();
        }

        private List<EstimateType> all_estimateTypes;
        private void BasicLoader()
        {
            MainInfoPanel.Visibility = Visibility.Collapsed;
            ScalesPanel.Visibility = Visibility.Collapsed;
            QuestionsPanel.Visibility = Visibility.Collapsed;

            all_estimateTypes = OpenedQuestionnaire.TakeAllEstimateTypes();
            EstimateBox.ItemsSource = all_estimateTypes;
        }

        public CreationPage(Questionnaire questionnaire) //Если идет создание теста с нуля
        {
            InitializeComponent();
            BasicLoader();

            OpenedQuestionnaire = questionnaire;
            EditLoader();
        }

        private void EditLoader()
        {
            CreateQuestionnaireBlock.Text = "Сохранить изменения";
            DeleteQuestionnaireBut.Visibility = Visibility.Visible;

            NameBox.Text = OpenedQuestionnaire.Name;
            _isNameNormal = true;

            if (OpenedQuestionnaire.EstimateType != null)
            {
                EstimateBox.SelectedItem = all_estimateTypes.FirstOrDefault(x=> x.Estimate_Id == OpenedQuestionnaire.EstimateType.Estimate_Id);
                _isEstimateNormal = true;
            }
            DescriptionBox.Text = OpenedQuestionnaire.Description;

            RefreshScaleList();
            RefreshQuestionList();

            if (OpenedQuestionnaire.IsMixedQuestions)
            {
                MixUpQuestionsCheckBox.IsChecked = true;
            }
            else
            {
                MixUpQuestionsCheckBox.IsChecked = false;
            }

            _isCreatingPage = false;

            OpenedQuestionnaire.DecodeFile();
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

        private void CleanAllBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить все данные в тесте?", "Полная очистка данных",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            
            if (_isCreatingPage)
            {
                NavigationService.Navigate(new CreationPage());
            }
            else
            {
                OpenedQuestionnaire.ClearAll();
                NavigationService.Navigate(new CreationPage(OpenedQuestionnaire));
            }
        }

        private void searchAndCloseWindow(string window_name)
        {
            var owned_windows = System.Windows.Application.Current.MainWindow.OwnedWindows;
            foreach (var window in owned_windows)
            {
                var real_window = window as Window;
                if (real_window.Name == window_name)
                {
                    real_window.Close();
                }
            }
        }

        private void CreateQuestionnaireBut_Click(object sender, RoutedEventArgs e)
        {
            if (!MainInfoCheckout())
            {
                MessageBox.Show("Сохранение не произведено из-за отсутствия надлежащего заполнения полей", "Ошибка при попытке сохранении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var estimateType = EstimateBox.SelectedItem as EstimateType;
            var mixUpStatus = Convert.ToBoolean(MixUpQuestionsCheckBox.IsChecked);
            OpenedQuestionnaire.EditQuestionnaire(NameBox.Text, DescriptionBox.Text, estimateType.Estimate_Title, mixUpStatus);
            OpenedQuestionnaire.EncodeToData();

            MessageBox.Show("Сохранение теста прошло успешно!", "Успешное сохранение опроса",
                MessageBoxButton.OK, MessageBoxImage.Information);

            CreateQuestionnaireBlock.Text = "Сохранить изменения";
            DeleteQuestionnaireBut.Visibility = Visibility.Visible;
        }

        private void DeleteQuestionnaireBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить данный тест?", "Удаление теста",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            OpenedQuestionnaire.Delete();
            NavigationService.GoBack();

            MessageBox.Show("Удаление теста прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool MainInfoCheckout()
        {
            if (_isNameNormal)
            {
                nameBoxCheckout();
            }
            if (_isEstimateNormal)
            {
                estimateBoxCheckout();
            }
            if (_isDescriptionNormal)
            {
                descriptionBoxCheckout();
            }

            if (!_isNameNormal || !_isEstimateNormal || !_isDescriptionNormal)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ОБЩАЯ ИНФОРМАЦИЯ
        /// Методы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            nameBoxCheckout();
        }

        private bool _isNameNormal = false;
        private void nameBoxCheckout()
        {
            if (NameBox.Text.Length > 100)
            {
                MessageBox.Show("Заданное название теста превышает размер в 100 символов!", "Ошибка в имени",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }
            if (NameBox.Text.Contains('\\') || NameBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное название теста содержит в себе недопустимые символы!", "Ошибка в имени",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }

            _isNameNormal = true;
        }

        private void EstimateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            estimateBoxCheckout();
        }

        private bool _isEstimateNormal = false;
        private void estimateBoxCheckout()
        {
            var estimateType = EstimateBox.SelectedItem as EstimateType;
            OpenedQuestionnaire.ChooseEstimateType(estimateType);
            _isEstimateNormal = true;
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            descriptionBoxCheckout();
        }

        private bool _isDescriptionNormal = true;
        private void descriptionBoxCheckout()
        {
            if (DescriptionBox.Text.Contains('\\') || DescriptionBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное описание теста содержит в себе недопустимые символы!", "Ошибка в описании",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isDescriptionNormal = false;
                DescriptionBox.Text = "";
                return;
            }

            _isDescriptionNormal = true;
        }




        /// <summary>
        /// ПРЕДМЕТ ТЕСТИРОВАНИЯ
        /// Методы шкал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshScaleList()
        {
            ScalesList.ItemsSource = null;
            ScalesList.ItemsSource = OpenedQuestionnaire.TakeAllScales();
        }

        private void AddNewScaleBut_Click(object sender, RoutedEventArgs e)
        {
            var created_scale = OpenedQuestionnaire.AddNewScale();
            searchAndCloseWindow("OpenedScaleWindow");
            ScaleWindow scaleWindow = new ScaleWindow(created_scale);
            RefreshScaleList();
        }
        private void EditScaleBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Scale;
            searchAndCloseWindow("OpenedScaleWindow");
            ScaleWindow scaleWindow = new ScaleWindow(selected_item, "");
            RefreshScaleList();
        }
        private void DeleteScaleBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Scale;
            searchAndCloseWindow("OpenedScaleWindow");
            OpenedQuestionnaire.DeleteScale(selected_item);
            RefreshScaleList();

            MessageBox.Show("Удаление шкалы прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// ВОПРОСЫ ТЕСТИРОВАНИЯ
        /// Базовые методы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        public void RefreshQuestionList()
        {
            QuestionList.ItemsSource = null;
            QuestionList.ItemsSource = OpenedQuestionnaire.TakeAllQuestions();
        }

        private void AddNewQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var created_question = OpenedQuestionnaire.AddNewQuestion();
            searchAndCloseWindow("OpenedQuestionWindow");
            QuestionWindow questionWindow = new QuestionWindow(created_question);
            RefreshQuestionList();
        }

        private void EditQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Question;
            searchAndCloseWindow("OpenedQuestionWindow");
            QuestionWindow questionWindow = new QuestionWindow(selected_item, "");
            RefreshQuestionList();
        }

        private void DeleteQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Question;
            searchAndCloseWindow("OpenedQuestionWindow");
            OpenedQuestionnaire.DeleteQuestion(selected_item);
            RefreshQuestionList();

            MessageBox.Show("Удаление вопроса прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// Методы перемешки <summary>
        /// 
        /// </summary>
        private void UpQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Question;
            searchAndCloseWindow("OpenedQuestionWindow");
            OpenedQuestionnaire.MoveQuestion(selected_item, "Up");
            RefreshQuestionList();
        }
        private void DownQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Question;
            searchAndCloseWindow("OpenedQuestionWindow");
            OpenedQuestionnaire.MoveQuestion(selected_item, "Down");
            RefreshQuestionList();
        }

        private void MixUpQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {                                                   
            OpenedQuestionnaire.IsMixedQuestions = true;
            App.Current.Resources["DefaultStackPanel"] = App.Current.Resources["UnvisibleStackPanel"];
        }

        private void MixUpQuestionsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OpenedQuestionnaire.IsMixedQuestions = false;
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
