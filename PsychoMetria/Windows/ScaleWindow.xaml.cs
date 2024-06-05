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
        public ScaleWindow()
        {
            InitializeComponent();
            BasicLoader();
        }
        private void BasicLoader()
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
            }
            else
            {
                this.Close();
            }
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

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Методы для списка оценок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public bool RangeCheckIn(int CheckValue) //Принимается проверяемое значение
        {
            return true;
        }
        private void EditEvaluationBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEvaluationBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewEvaluationBut_Click(object sender, RoutedEventArgs e)
        {

        }
        


        /// <summary>
        /// Глобальные кнопки: отмена и сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SaveScaleBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelScaleBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
