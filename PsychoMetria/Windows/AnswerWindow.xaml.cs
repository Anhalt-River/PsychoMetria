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
    /// Логика взаимодействия для AnswerWindow.xaml
    /// </summary>
    public partial class AnswerWindow : Window
    {
        public AnswerWindow()
        {
            InitializeComponent();
            BasicLoader();
        }
        public AnswerWindow(Answer answer)
        {
            InitializeComponent();
            DeleteAnswerBut.Visibility = Visibility.Collapsed;
            BasicLoader();
        }

        private void BasicLoader()
        {
            if (!App.IsAnswerOpened)
            {
                App.IsAnswerOpened = true;
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
            App.IsAnswerOpened = false;
            this.Owner.Activate();
        }

        private void AnswerTitleBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AnswerProgressBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
