using PsychoMetria.Materials.Models;
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
    /// Логика взаимодействия для ScaleWindow.xaml
    /// </summary>
    public partial class ScaleWindow : Window
    {
        private Scale _taked_scale;
        public ScaleWindow(Scale scale)
        {
            InitializeComponent();
            BasicLoader(scale);
        }
        public ScaleWindow(Scale scale, string empty)
        {
            InitializeComponent();
            BasicLoader(scale);
            EditLoader();
        }
        private void BasicLoader(Scale scale)
        {
            if (!App.IsScalesOpened)
            {
                App.IsScalesOpened = true;
                this.Owner = App.Current.MainWindow;
                this.Show();

                MainInfoUnderline.Visibility = Visibility.Visible;
                EvaluationUnderline.Visibility = Visibility.Collapsed;
                MainInfoPanel.Visibility = Visibility.Visible;
                EvaluationPanel.Visibility = Visibility.Collapsed;

                _taked_scale = scale;
                NameBox.Text = _taked_scale.Scale_Title;
                DescriptionBox.Text = _taked_scale.Scale_Description;
            }
            else
            {
                this.Close();
            }
        }

        private void EditLoader()
        {
            _isNameNormal = true;
            _isDescriptionNormal = true;
            RefreshEvaluationList();
        }

        private void MainInfoBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Visible;
            EvaluationUnderline.Visibility = Visibility.Collapsed;

            MainInfoPanel.Visibility = Visibility.Visible;
            EvaluationPanel.Visibility = Visibility.Collapsed;
        }

        private void EvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            MainInfoUnderline.Visibility = Visibility.Collapsed;
            EvaluationUnderline.Visibility = Visibility.Visible;

            MainInfoPanel.Visibility = Visibility.Collapsed;
            EvaluationPanel.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsScalesOpened = false;
            App.IsEvaluationOpened = false;
            App.Current.MainWindow.Activate();
        }

        private bool _isNameNormal = false;
        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length > 100)
            {
                MessageBox.Show("Заданное название шкалы превышает размер в 100 символов!", "Ошибка в имени",
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
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            if (!take_page.OpenedQuestionnaire.NameCheckoutScale(NameBox.Text))
            {
                MessageBox.Show("Указанное название теста повторяет название других шкал в тесте!", "Ошибка в имени",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }

            _isNameNormal = true;
        }

        private bool _isDescriptionNormal = true;
        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionBox.Text.Contains('\\') || DescriptionBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное описание шкалы содержит в себе недопустимые символы!", "Ошибка в описании",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isDescriptionNormal = false;
                DescriptionBox.Text = "";
                return;
            }

            _isDescriptionNormal = true;
        }



        /// <summary>
        /// Методы для списка оценок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        public void RefreshEvaluationList()
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            EvaluationList.ItemsSource = take_page.OpenedQuestionnaire.TakeAllEvaluations(_taked_scale.Scale_Id);
        }

        private void AddNewEvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var created_evaluation = take_page.OpenedQuestionnaire.AddNewEvaluation(_taked_scale.Scale_Id);
            EvaluationWindow evaluationWindow = new EvaluationWindow(created_evaluation, _taked_scale.Scale_Id);
            RefreshEvaluationList();
        }
        private void EditEvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = (sender as Button).DataContext as Evaluation;
            EvaluationWindow evaluationWindow = new EvaluationWindow(selected_item, _taked_scale.Scale_Id);
            RefreshEvaluationList();
        }
        private void DeleteEvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            var selected_item = (sender as Button).DataContext as Evaluation;
            take_page.OpenedQuestionnaire.DeleteEvaluation(selected_item);
            RefreshEvaluationList();

            MessageBox.Show("Удаление оценки прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool RangeCheckIn(int CheckValue) //Принимается проверяемое значение
        {
            return true;
        }
        


        /// <summary>
        /// Глобальные кнопки: отмена и сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SaveScaleBut_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNameNormal || !_isDescriptionNormal)
            {
                MessageBox.Show("Сохранение не произведено из-за отсутствия надлежащего заполнения полей", "Ошибка при попытке сохранении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.EditScale(_taked_scale.Scale_Id, NameBox.Text, DescriptionBox.Text);
            take_page.RefreshScaleList();

            MessageBox.Show("Изменение шкалы прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteScaleBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить данную шкалу?", "Удаление шкалы",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.DeleteScale(_taked_scale);
            take_page.RefreshScaleList();
            this.Close();

            MessageBox.Show("Удаление шкалы прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
