using PsychoMetria.Materials.Models;
using PsychoMetria.Materials.Models.SupportModels;
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
    /// Логика взаимодействия для ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        private Testing _workingTest;
        public ResultsPage(Testing _test)
        {
            InitializeComponent();

            _workingTest = _test;
            BasicLoader();
        }

        private void BasicLoader()
        {
            TestNameBlock.Text = _workingTest.Taked_Questionnaire.Name;
            DescriptionBlock.Text = _workingTest.Taked_Questionnaire.Description;
            ScaleResultsList.ItemsSource = _workingTest.TakeResults();
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
