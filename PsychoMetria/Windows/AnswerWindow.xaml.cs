using PsychoMetria.Materials.Models;
using PsychoMetria.Materials.Models.SupportModels;
using PsychoMetria.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AnswerWindow.xaml
    /// </summary>
    public partial class AnswerWindow : Window
    {
        private Answer _taked_answer;
        private int _taked_questionId;
        public AnswerWindow(Answer answer, int questionId)
        {
            InitializeComponent();
            DeleteAnswerBut.Visibility = Visibility.Collapsed;
            BasicLoader(answer, questionId);
        }

        public AnswerWindow(Answer answer, int questionId, string empty)
        {
            InitializeComponent();
            DeleteAnswerBut.Visibility = Visibility.Collapsed;
            BasicLoader(answer, questionId);
            EditLoader();
        }

        private void BasicLoader(Answer answer, int questionId)
        {
            if (!App.IsAnswerOpened)
            {
                App.IsAnswerOpened = true;
                this.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                this.Show();

                _taked_answer = answer;
                _taked_questionId = questionId;

                AnswerTitleBox.Text = answer.Answer_Text;

                RefreshScaleAttachList();
            }
            else
            {
                this.Close();
            }
        }

        private void EditLoader()
        {
            _isTitleNormal = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsAnswerOpened = false;
            try
            {
                if (this.Owner != null)
                {
                    this.Owner.Activate();
                }
                else
                {
                    Application.Current.MainWindow.Activate();
                }
            }
            catch (Exception)
            {
                Application.Current.MainWindow.Activate();
            }
        }

        private void RefreshScaleAttachList()
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;

            ScalesList.ItemsSource = null;
            ScalesList.ItemsSource = take_page.OpenedQuestionnaire.TakeAllInfluences(_taked_answer);
        }

        /// <summary>
        /// Методы для базовых полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AnswerTitleBox_LostFocus(object sender, RoutedEventArgs e)
        {
            answerTitleCheckout();
        }

        private bool _isTitleNormal = false;
        private void answerTitleCheckout()
        {
            if (AnswerTitleBox.Text.Length > 150)
            {
                MessageBox.Show("Заданное содержание ответа превышает размер в 150 символов!", "Ошибка в ответе",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isTitleNormal = false;
                AnswerTitleBox.Text = "";
                return;
            }
            if (AnswerTitleBox.Text.Contains('\\') || AnswerTitleBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное содержание ответа содержит в себе недопустимые символы!", "Ошибка в ответе",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isTitleNormal = false;
                AnswerTitleBox.Text = "";
                return;
            }

            _isTitleNormal = true;
        }


        /// <summary>
        /// Методы для прикрепленных шкал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnswerProgressBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var item = (sender as TextBox).DataContext as AnswerInfluence;
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;

            try
            {
                int value = Convert.ToInt32(item.Influence);

                if (value == 0)
                {
                    take_page.OpenedQuestionnaire.DeleteAnswerInfluence(item);
                }
                else
                {
                    take_page.OpenedQuestionnaire.EditAnswerInfluence(item, _taked_answer.Answer_Id);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Указанное значение в поле прогресса не является целочисленным значением!", "Ошибка в прогрессе за ответ",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                item.Influence = 0;
            }

            RefreshScaleAttachList();
        }


        /// <summary>
        /// Глобальные методы
        /// Сохранение и удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SaveAnswerBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isTitleNormal)
            {
                answerTitleCheckout();
            }

            if (!_isTitleNormal)
            {
                MessageBox.Show("Сохранение не произведено из-за отсутствия надлежащего заполнения полей", "Ошибка при попытке сохранении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.EditAnswer(_taked_answer.Answer_Id, AnswerTitleBox.Text, _taked_questionId);
            (this.Owner as QuestionWindow).RefreshAnswerList();
            MessageBox.Show("Изменение ответа прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteAnswerBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить данный ответ?", "Удаление ответа",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.DeleteAnswer(_taked_answer);
            (this.Owner as QuestionWindow).RefreshAnswerList();
            this.Close();

            MessageBox.Show("Удаление ответа прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
