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
    /// Логика взаимодействия для EvaluationWindow.xaml
    /// </summary>
    public partial class EvaluationWindow : Window
    {
        private Evaluation _taked_evaluation;
        private int _taked_scaleId;
        public EvaluationWindow(Evaluation evaluation, int scaleId)
        {
            InitializeComponent();

            _taked_evaluation = evaluation;
            _taked_scaleId = scaleId;
            BasicLoader();
        }

        public EvaluationWindow(Evaluation evaluation, int scaleId, string empty)
        {
            InitializeComponent();

            _taked_evaluation = evaluation;
            _taked_scaleId = scaleId;
            BasicLoader();
            EditLoader();
        }

        private void BasicLoader()
        {
            if (!App.IsEvaluationOpened)
            {
                App.IsEvaluationOpened = true;
                this.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                this.Show();

                NameBox.Text = _taked_evaluation.Evaluation_Title;
                DescriptionBox.Text = _taked_evaluation.Evaluation_Description;
                StartRangeBox.Text = _taked_evaluation.StartRange.ToString();
                EndRangeBox.Text = _taked_evaluation.EndRange.ToString();
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsEvaluationOpened = false;
            this.Owner.Activate();
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            nameCheckout();
        }

        private bool _isNameNormal = false;
        private void nameCheckout()
        {
            if (NameBox.Text.Length > 100)
            {
                MessageBox.Show("Заданное название оценки превышает размер в 150 символов!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }
            if (NameBox.Text.Contains('\\') || NameBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное название оценки содержит в себе недопустимые символы!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            if (!take_page.OpenedQuestionnaire.NameCheckoutEvaluation(NameBox.Text, _taked_evaluation.Evaluation_Id))
            {
                MessageBox.Show("Указанное название оценки повторяет название других оценок в тесте!", "Ошибка в названии",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isNameNormal = false;
                NameBox.Text = "";
                return;
            }

            _isNameNormal = true;
        }
        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            descriptionCheckout();
        }

        private bool _isDescriptionNormal = true;
        private void descriptionCheckout()
        {
            if (DescriptionBox.Text.Contains('\\') || DescriptionBox.Text.Contains('/'))
            {
                MessageBox.Show("Заданное описание оценки содержит в себе недопустимые символы!", "Ошибка в описании",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isDescriptionNormal = false;
                DescriptionBox.Text = "";
                return;
            }

            _isDescriptionNormal = true;
        }

        /// <summary>
        /// Функции для работы с диапазоном значений оценок
        /// </summary>
        private bool _isRangesNormal = false;
        private void RangeChecout()
        {
            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            int startRange = Convert.ToInt32(StartRangeBox.Text);
            int endRange = Convert.ToInt32(EndRangeBox.Text);
            _isRangesNormal = take_page.OpenedQuestionnaire.RangesCheckoutEvaluation(_taked_evaluation, _taked_scaleId, startRange, endRange);
        }

        private void StartRangeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            startRangeCheckout();
        }
        private bool _isStartRangeNormal = true;
        private void startRangeCheckout()
        {
            try
            {
                int start_range = Convert.ToInt32(StartRangeBox.Text);

                if (start_range < 0)
                {
                    MessageBox.Show("Указанное значение начала диапазона не является положительным или нулевым значением!", "Ошибка в начальном значении диапазона",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _isStartRangeNormal = false;
                    StartRangeBox.Text = "0";
                    return;
                }

                if (start_range >= Convert.ToInt32(EndRangeBox.Text))
                {
                    MessageBox.Show("Указанное значение начала диапазона больше или равно концу диапазона!", "Ошибка в начальном значении диапазона",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _isEndRangeNormal = false;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Указанное значение начала диапазона не является целочисленным значением!", "Ошибка в начальном значении диапазона",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isStartRangeNormal = false;
                return;
            }

            _isStartRangeNormal = true;
        }

        private void EndRangeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            endRangeCheckout();
        }
        private bool _isEndRangeNormal = true;
        private void endRangeCheckout()
        {
            try
            {
                int end_range = Convert.ToInt32(EndRangeBox.Text);

                if (end_range < 0)
                {
                    MessageBox.Show("Указанное значение конца диапазона не является положительным значением!", "Ошибка в конечном значении диапазона",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _isEndRangeNormal = false;
                    return;
                }

                if (end_range <= Convert.ToInt32(StartRangeBox.Text))
                {
                    MessageBox.Show("Указанное значение конца диапазона меньше или равно начала диапазона!", "Ошибка в конечном значении диапазона",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _isEndRangeNormal = false;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Указанное значение КОНЦА диапазона не является целочисленным значением!", "Ошибка в конечном значении диапазона",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _isEndRangeNormal = false;
                return;
            }

            _isEndRangeNormal = true;
        }



        /// <summary>
        /// ГЛОБАЛЬНЫЕ МЕТОДЫ
        /// Сохранение и удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SaveEvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            if (_isNameNormal)
            {
                nameCheckout();
            }
            if (_isDescriptionNormal)
            {
                descriptionCheckout();
            }
            if (_isStartRangeNormal)
            {
                startRangeCheckout();
            }
            if (_isEndRangeNormal)
            {
                endRangeCheckout();
            }

            RangeChecout();
            if (!_isRangesNormal)
            {
                MessageBox.Show("Сохранение не произведено из-за того, что указанный диапазон оценки находится в диапазоне другой оценки данной шкалы",
                    "Ошибка при попытке сохранении", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_isNameNormal || !_isDescriptionNormal || !_isStartRangeNormal || !_isEndRangeNormal)
            {
                MessageBox.Show("Сохранение не произведено из-за отсутствия надлежащего заполнения полей", "Ошибка при попытке сохранении",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;

            int startRange = Convert.ToInt32(StartRangeBox.Text);
            int endRange = Convert.ToInt32(EndRangeBox.Text);
            take_page.OpenedQuestionnaire.EditEvaluation(_taked_evaluation.Evaluation_Id, NameBox.Text, DescriptionBox.Text,
                startRange, endRange);
            (this.Owner as ScaleWindow).RefreshEvaluationList();

            MessageBox.Show("Изменение оценки прошло успешно!", "Процесс завершен",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteEvaluationBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью удалить данную оценку?", "Удаление оценки",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            var take_page = (App.Current.MainWindow as MainWindow).MainFrame.Content as CreationPage;
            take_page.OpenedQuestionnaire.DeleteEvaluation(_taked_evaluation);

            (this.Owner as ScaleWindow).RefreshEvaluationList();
            this.Close();
        }
    }
}
