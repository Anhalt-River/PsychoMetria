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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для IntroductoryPage.xaml
    /// </summary>
    public partial class IntroductoryPage : Page
    {
        private Questionnaire _taked_questionnaire;
        public IntroductoryPage(Questionnaire questionnaire)
        {
            InitializeComponent();
            _taked_questionnaire = questionnaire;
            BasicLoader();
        }
        
        private void BasicLoader()
        {
            _taked_questionnaire.DecodeFile();

            QuestionnaireBlock.Text = _taked_questionnaire.Name;
            QuestionsCountBlock.Text = _taked_questionnaire.QuestionsCount();
            if (_taked_questionnaire.Description.Length > 0)
            {
                DescriptionBlock.Text = _taked_questionnaire.Description;
            }
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void StartTestBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QuestionnairePage(_taked_questionnaire));
        }
    }
}
