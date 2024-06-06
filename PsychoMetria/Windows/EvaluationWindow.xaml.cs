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
        public EvaluationWindow()
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            if (!App.IsEvaluationOpened)
            {
                App.IsEvaluationOpened = true;
                this.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsEvaluationOpened = false;
            this.Owner.Activate();
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Функции для работы с диапазоном значений оценок
        /// </summary>
        private void RangeChecout()
        {

        }

        private void StartRangeBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void EndRangeBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SaveEvaluationBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEvaluationBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
