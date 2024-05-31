using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PsychoMetria.Materials.Models;
using PsychoMetria.Windows;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        /// <summary>
        /// Режимы пользования:
        /// 1 - Режим пользователя
        /// 2 - Режим разработчика
        /// </summary>
        private int _roleMode = 1; 
        public MainPage()
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            UserMenuBoard.Width = 0;
            DeveloperMenuBoard.Width = 0;
            ModeMessageBoard.Height = 0;
        }

        private void QuestionnaireStartBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QuestionnairePage());
        }

        private void ListLoader()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data" + "\\Test.txt";
            //MessageBox.Show($"{path}");

            Questionnaire questionnaire = new Questionnaire(path);
            List<Questionnaire> questionnaires = new List<Questionnaire>();
            questionnaires.Add(questionnaire);

            QuestionnaireList.ItemsSource = null;
            QuestionnaireList.ItemsSource = questionnaires;
        }

        private void UserToolKitBut_Click(object sender, RoutedEventArgs e)
        {
            userToolKitMethod();
        }

        private bool isUserToggle;
        private void userToolKitMethod()
        {
            if (isDeveloperToggle)
            {
                developerToolKitMethod();
            }

            DoubleAnimation da = new DoubleAnimation();
            if (!isUserToggle)
            {
                da.To = 190;
                da.Duration = TimeSpan.FromSeconds(1);
                UserMenuBoard.BeginAnimation(WidthProperty, da);
                isUserToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(1);
                UserMenuBoard.BeginAnimation(WidthProperty, da);
                isUserToggle = false;
            }
        }

        private void DeveloperToolKitBut_Click(object sender, RoutedEventArgs e)
        {
            developerToolKitMethod();
        }

        private bool isDeveloperToggle;
        private void developerToolKitMethod()
        {
            if (isUserToggle)
            {
                userToolKitMethod();
            }

            DoubleAnimation da = new DoubleAnimation();
            if (!isDeveloperToggle)
            {
                da.To = 190;
                da.Duration = TimeSpan.FromSeconds(1);
                DeveloperMenuBoard.BeginAnimation(WidthProperty, da);
                isDeveloperToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(1);
                DeveloperMenuBoard.BeginAnimation(WidthProperty, da);
                isDeveloperToggle = false;
            }
        }

        private void AddNewQuestionnaireBut_Click(object sender, RoutedEventArgs e)
        {
            //Найти новый файл, скопировать его в папку установленных тестов
            ListLoader();
        }

        private void SettingsBut_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
        }

        private void DeveloperModeBut_Click(object sender, RoutedEventArgs e)
        {
            _roleMode = 2;

            Style style = new Style
            {
                TargetType = typeof(TextBlock)
            };

            style.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.White));

            App.Current.Resources["MyStyle"] = style;

            roleMessageMethod();
        }

        private void UserModeBut_Click(object sender, RoutedEventArgs e)
        {
            _roleMode = 1;
            roleMessageMethod();
        }

        private async void roleMessageMethod()
        {
            if (_roleMode == 1)
            {
                ModeBlock.Text = "Режим пользователя";
            }
            else
            {
                ModeBlock.Text = "Режим разработчика";
            }
            await messageChanger();
            await messageChanger();
        }

        private bool isMessageToggle;
        private async Task messageChanger()
        {
            DoubleAnimation da = new DoubleAnimation();
            if (!isMessageToggle)
            {
                da.To = 70;
                da.Duration = TimeSpan.FromSeconds(0.3);
                ModeMessageBoard.BeginAnimation(HeightProperty, da);
                isMessageToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(0.3);
                ModeMessageBoard.BeginAnimation(HeightProperty, da);
                isMessageToggle = false;
            }
            await Task.Delay(1000);
        }
    }
}
