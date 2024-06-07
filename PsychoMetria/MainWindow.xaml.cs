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

namespace PsychoMetria
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BasicLoader();
        }

        private void BasicLoader()
        {
            if (PsychoMetria.Properties.Settings.Default.IsFirstStart)
            {
                App.CreateAppDataFolder();
                PsychoMetria.Properties.Settings.Default.IsFirstStart = false;
                PsychoMetria.Properties.Settings.Default.Save();
            }

            if (!PsychoMetria.Properties.Settings.Default.IsLoadCanceled)
            {
                MainFrame.Content = new Pages.StartPage(true);
            }
            else
            {
                MainFrame.Content = new Pages.MainPage();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PsychoMetria.Properties.Settings.Default.IsLoadCanceled)
            {
                return;
            }
            if (!App.IsWindowClosing)
            {
                App.IsWindowClosing = true;
                MainFrame.Content = new Pages.StartPage(false);
                e.Cancel = true;
            }
        }

        public void WindowFinalClosing()
        {
            this.Close();
        }
    }
}
