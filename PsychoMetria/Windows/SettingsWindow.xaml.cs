using PsychoMetria.Pages;
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
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            if (!App.IsSettingsOpened)
            {
                App.IsSettingsOpened = true;
                this.Owner = App.Current.MainWindow;
                this.Show();
                backgroundFlicker();
            }
            else
            {
                this.Close();
            }
        }

        private async void backgroundFlicker()
        {
            await opacityChanger();
        }

        private async Task opacityChanger()
        {
            double counter = 1;
            double modifier = 0.02;
            while (App.IsSettingsOpened)
            {
                await Task.Delay(100);
                counter -= modifier;
                BackPanel.Opacity = counter;
                if(counter == 1)
                {
                    modifier = 0.02;
                }
                else if (counter < 0.5)
                {
                    modifier = -0.02;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.IsSettingsOpened = false;
            App.Current.MainWindow.Activate();
        }

        private void ReloadMainPage()
        {
            var mainPage = (((MainWindow)Application.Current.MainWindow).MainFrame.Content as MainPage);
            if (mainPage != null)
            {
                mainPage.BasicLoader();
            }
        }

        private void LoaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PsychoMetria.Properties.Settings.Default.IsLoadCanceled = false;
            PsychoMetria.Properties.Settings.Default.Save();
        }

        private void LoaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PsychoMetria.Properties.Settings.Default.IsLoadCanceled = true;
            PsychoMetria.Properties.Settings.Default.Save();
        }

        private void LoaderCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (PsychoMetria.Properties.Settings.Default.IsLoadCanceled)   //Если загрузка запрещена
            {
                LoaderCheckBox.IsChecked = false;
            }
            else
            {
                LoaderCheckBox.IsChecked = true;
            }
        }

        private void DevelopHideCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PsychoMetria.Properties.Settings.Default.IsDeveloperHidden = true;
            PsychoMetria.Properties.Settings.Default.Save();
            ReloadMainPage();
        }

        private void DevelopHideCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PsychoMetria.Properties.Settings.Default.IsDeveloperHidden = false;
            PsychoMetria.Properties.Settings.Default.Save();
            ReloadMainPage();
        }

        private void DevelopHideCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (PsychoMetria.Properties.Settings.Default.IsDeveloperHidden)   //Если режим разработчика спрятан
            {
                DevelopHideCheckBox.IsChecked = true;
            }
            else
            {
                DevelopHideCheckBox.IsChecked = false;
            }
        }
    }
}
