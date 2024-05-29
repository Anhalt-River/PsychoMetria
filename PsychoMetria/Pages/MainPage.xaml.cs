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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PsychoMetria.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            UserMenuBoard.Width = 0;
            DeveloperMenuBoard.Width = 0;
        }

        private void QuestionnaireStartBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserToolKitBut_Click(object sender, RoutedEventArgs e)
        {
            UserToolKitMethod();
        }

        private bool IsUserToggle;
        private void UserToolKitMethod()
        {
            if (IsDeveloperToggle)
            {
                DeveloperToolKitMethod();
            }

            DoubleAnimation da = new DoubleAnimation();
            if (!IsUserToggle)
            {
                da.To = 190;
                da.Duration = TimeSpan.FromSeconds(1);
                UserMenuBoard.BeginAnimation(WidthProperty, da);
                IsUserToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(1);
                UserMenuBoard.BeginAnimation(WidthProperty, da);
                IsUserToggle = false;
            }
        }

        private void DeveloperToolKitBut_Click(object sender, RoutedEventArgs e)
        {
            DeveloperToolKitMethod();
        }

        private bool IsDeveloperToggle;
        private void DeveloperToolKitMethod()
        {
            if (IsUserToggle)
            {
                UserToolKitMethod();
            }

            DoubleAnimation da = new DoubleAnimation();
            if (!IsDeveloperToggle)
            {
                da.To = 190;
                da.Duration = TimeSpan.FromSeconds(1);
                DeveloperMenuBoard.BeginAnimation(WidthProperty, da);
                IsDeveloperToggle = true;
            }
            else
            {
                da.To = 0;
                da.Duration = TimeSpan.FromSeconds(1);
                DeveloperMenuBoard.BeginAnimation(WidthProperty, da);
                IsDeveloperToggle = false;
            }
        }
    }
}
