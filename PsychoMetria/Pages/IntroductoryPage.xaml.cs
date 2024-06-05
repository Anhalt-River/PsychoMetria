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
    /// Логика взаимодействия для IntroductoryPage.xaml
    /// </summary>
    public partial class IntroductoryPage : Page
    {
        public IntroductoryPage()
        {
            InitializeComponent();
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void StartTestBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
