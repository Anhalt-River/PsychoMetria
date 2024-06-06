using PsychoMetria.Materials.Models;
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

                TitleBox.Text = question.Question_Title;
                DescriptionBox.Text = question.Question_Text;
                //ТИП ВОПРОСА НЕ УСТАНОВЛЕН
            }
            else
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsScalesOpened = false;
            App.IsEvaluationOpened = false;
            App.Current.MainWindow.Activate();
        }

        private void MainInfoBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Visible;
            ScaleUnderline.Visibility = Visibility.Collapsed;
            AnswerUnderline.Visibility = Visibility.Collapsed;

            MainInfoPanel.Visibility = Visibility.Visible;
            ScalePanel.Visibility = Visibility.Collapsed;
            AnswerPanel.Visibility = Visibility.Collapsed;
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

        private void AnswersBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Collapsed;
            ScaleUnderline.Visibility = Visibility.Collapsed;
            AnswerUnderline.Visibility = Visibility.Visible;

            MainInfoPanel.Visibility = Visibility.Visible;
            ScalePanel.Visibility = Visibility.Collapsed;
            AnswerPanel.Visibility = Visibility.Collapsed;
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TitleBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void QuestionTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
        /// <summary>
        /// Методы для списка ответов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewAnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Глобальные кнопки: отмена и сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelQuestionBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AttachNewScale_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Функции прикрепления и открепления шкал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToAttachedStatusBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FromAttachedStatusBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
