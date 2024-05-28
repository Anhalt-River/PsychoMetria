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
            MainFrame.Content = new Pages.StartPage(true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
