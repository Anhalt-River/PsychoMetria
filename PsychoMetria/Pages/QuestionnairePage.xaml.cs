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

using PsychoMetria.Materials.Models;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для QuestionnairePage.xaml
    /// </summary>
    public partial class QuestionnairePage : Page
    {
        public QuestionnairePage() //Передать идентификаторы теста
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            Answer answer = new Answer();
            List<Answer> answers = new List<Answer>();
            answers.Add(answer);
            answers.Add(answer);
            answers.Add(answer);
            answers.Add(answer);
            answers.Add(answer);
            answers.Add(answer);
            answers.Add(answer);

            AnswersList.ItemsSource = answers;
        }

        private void BackToMainBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void AnswerBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
