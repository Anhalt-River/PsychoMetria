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

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreationPage.xaml
    /// </summary>
    public partial class CreationPage : Page
    {
        public CreationPage()
        {
            InitializeComponent();
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void CreateQuestionnaireBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CleanAllBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ScaleBox_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
