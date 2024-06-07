using PsychoMetria.Materials.Models;
using PsychoMetria.Materials.Models.SupportModels;
using PsychoMetria.Pages;
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

namespace PsychoMetria.Windows
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private Question _taked_question;
        public QuestionWindow(Question question)
        {
            InitializeComponent();
            BasicLoader(question);
        }

        public QuestionWindow(Question question, string empty)
        {
            InitializeComponent();
            BasicLoader(question);
            EditLoader();
        }

        private void BasicLoader(Question question)
        {
            if (!App.IsQuestionOpened)
            {
                App.IsQuestionOpened = true;
                this.Owner = App.Current.MainWindow;
                this.Show();

                MainInfoUnderline.Visibility = Visibility.Visible;
                ScaleUnderline.Visibility = Visibility.Collapsed;
                AnswerUnderline.Visibility = Visibility.Collapsed;
                MainInfoPanel.Visibility = Visibility.Visible;
                ScalePanel.Visibility = Visibility.Collapsed;
                AnswerPanel.Visibility = Visibility.Collapsed;

                _taked_question = question;
                TitleBox.Text = question.Question_Title;
                DescriptionBox.Text = question.Question_Text;
                ReloadQuestionType();
                refreshScaleList();
                RefreshAnswerList();
            }
            else
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsQuestionOpened = false;
            App.IsAnswerOpened = false;
            App.Current.MainWindow.Activate();
        }

        private void searchAndCloseWindow()
        {
            var owned_windows = this.OwnedWindows;
            foreach (var window in owned_windows)
            {
                var real_window = window as Window;
                if (real_window.Name == "OpenedAnswerWindow")
                {
                    real_window.Close();
                }
            }
        }

        private List<QuestionType> _all_questionTypes = new List<QuestionType>();
        private void ReloadQuestionType()
        {
            var questionTypeExample = new QuestionType();
            _all_questionTypes = questionTypeExample.TakeAllTypes();

            QuestionTypeBox.ItemsSource = _all_questionTypes;
            QuestionTypeBox.SelectedItem = _all_questionTypes.FirstOrDefault(x=> x.QuestionType_Id == _taked_question.QuestionType.QuestionType_Id);

        }

        private void EditLoader()
        {
            _isTitleNormal = true;
            _isDescriptionNormal = true;
        }

        /// <summary>
        /// ОСНОВНАЯ ИНФОРМАЦИЯ
        /// Функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MainInfoBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Visible;
            ScaleUnderline.Visibility = Visibility.Collapsed;
            AnswerUnderline.Visibility = Visibility.Collapsed;

            MainInfoPanel.Visibility = Visibility.Visible;
            ScalePanel.Visibility = Visibility.Collapsed;
            AnswerPanel.Visibility = Visibility.Collapsed;
        }

        private void TitleBox_LostFocus(object sender, RoutedEventArgs e)
        {
            titleCheckout();
        }

        private bool _isTitleNormal = true;
        private void titleCheckout()
        {
            if (TitleBox.Text.Length > 150)
            {
                MessageBox.Show("Заданное название вопроса превышает размер в 150 символов!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isTitleNormal = false;
                TitleBox.Text = "";
                return;
            }
            if (TitleBox.Text.Contains('\\') || TitleBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное название вопроса содержит в себе недопустимые символы!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isTitleNormal = false;
                TitleBox.Text = "";
                return;
            }
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            if (!take_page.OpenedQuestionnaire.NameCheckoutScale(TitleBox.Text, _taked_question.Question_Id))
            {
                MessageBox.Show("Указанное название вопроса повторяет название других вопросов в тесте!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isTitleNormal = false;
                TitleBox.Text = "";
                return;
            }

            _isTitleNormal = true;
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            descriptionCheckout();
        }

        private bool _isDescriptionNormal = false;
        private void descriptionCheckout()
        {
            if (DescriptionBox.Text.Contains('\\') || DescriptionBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное описание вопроса содержит в себе недопустимые символы!", "Ошибка в описании",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isDescriptionNormal = false;
                DescriptionBox.Text = "";
                return;
            }

            _isDescriptionNormal = true;
        }

        private void QuestionTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        /// ШКАЛЫ
        /// Функции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void refreshScaleList()
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            AvailableScaleList.ItemsSource = null;
            AttachedScaleList.ItemsSource = null;

            List<SupScaleAttach> nonAttachedList = new List<SupScaleAttach>();
            nonAttachedList = take_page.OpenedQuestionnaire.TakeAllScaleAttach2_NonAttached(_taked_question.Question_Id);
            AvailableScaleList.ItemsSource = nonAttachedList;


            List<SupScaleAttach> attachedList = new List<SupScaleAttach>();
            attachedList = take_page.OpenedQuestionnaire.TakeAllScaleAttach(_taked_question.Question_Id);
            AttachedScaleList.ItemsSource = attachedList;
        }
        private void ScaleBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Collapsed;
            ScaleUnderline.Visibility = Visibility.Visible;
            AnswerUnderline.Visibility = Visibility.Collapsed;

            MainInfoPanel.Visibility = Visibility.Collapsed;
            ScalePanel.Visibility = Visibility.Visible;
            AnswerPanel.Visibility = Visibility.Collapsed;
        }
        private void ToAttachedStatusBut_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableScaleList.SelectedItem == null)
            {
                MessageBox.Show("Для перенесения шкалы в разряд прикрепленных сперва выберите ее из левого списка!", "Ошибка при прикреплении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var selected_item = AvailableScaleList.SelectedItem as SupScaleAttach;
            take_page.OpenedQuestionnaire.AddNewScaleAttach(selected_item, _taked_question.Question_Id);

            refreshScaleList();
        }

        private void FromAttachedStatusBut_Click(object sender, RoutedEventArgs e)
        {
            if (AttachedScaleList.SelectedItem == null)
            {
                MessageBox.Show("Для открепления шкалы сперва выберите ее из списка прикрепленных вопросов!", "Ошибка при прикреплении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var selected_item = AttachedScaleList.SelectedItem as SupScaleAttach;
            take_page.OpenedQuestionnaire.DeleteScaleAttach(selected_item.ScaleAttach.Attach_Id);

            refreshScaleList();
        }


        /// <summary>
        /// Методы для списка ответов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        public void RefreshAnswerList()
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            AnswerList.ItemsSource = null;
            AnswerList.ItemsSource = take_page.OpenedQuestionnaire.TakeAllAnswers(_taked_question.Question_Id);
        }
        private void AnswersBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Collapsed;
            ScaleUnderline.Visibility = Visibility.Collapsed;
            AnswerUnderline.Visibility = Visibility.Visible;

            MainInfoPanel.Visibility = Visibility.Collapsed;
            ScalePanel.Visibility = Visibility.Collapsed;
            AnswerPanel.Visibility = Visibility.Visible;
        }
        private void AddNewAnswerBut_Click(object sender, RoutedEventArgs e)
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var created_question = take_page.OpenedQuestionnaire.AddNewAnswer(_taked_question.Question_Id);
            AnswerWindow answerWindow = new AnswerWindow(created_question, _taked_question.Question_Id);
            RefreshAnswerList();
        }

        private void EditAnswerBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Answer;
            searchAndCloseWindow();
            AnswerWindow answerWindow = new AnswerWindow(selected_item, _taked_question.Question_Id, "");
            RefreshAnswerList();
        }

        private void DeleteAnswerBut_Click(object sender, RoutedEventArgs e)
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var selected_item = (sender as Button).DataContext as Answer;
            take_page.OpenedQuestionnaire.DeleteAnswer(selected_item);
            RefreshAnswerList();

            MessageBox.Show("Удаление ответа прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Глобальные кнопки: отмена и сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isTitleNormal)
            {
                titleCheckout();
            }
            if (_isDescriptionNormal)
            {
                descriptionCheckout();
            }

            if (!_isTitleNormal || !_isDescriptionNormal)
            {
                MessageBox.Show("Сохранение не произведено из-за отсутствия надлежащего заполнения полей", "Ошибка при попытке сохранении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var selected_questionType = QuestionTypeBox.SelectedItem as QuestionType;
            take_page.OpenedQuestionnaire.EditQuestion(_taked_question.Question_Id, TitleBox.Text, DescriptionBox.Text, selected_questionType.QuestionType_Id);
            take_page.RefreshQuestionList();
            MessageBox.Show("Изменение вопроса прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteQuestionBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить данный вопрос?", "Удаление вопроса",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.DeleteQuestion(_taked_question);
            take_page.RefreshQuestionList();
            this.Close();

            MessageBox.Show("Удаление вопроса прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
