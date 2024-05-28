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
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private bool _isProjectStart;
        private int _opacityDelayTimer = 70;
        private string _title = "Добро пожаловать!";
        public StartPage(bool isProjectStart)
        {
            InitializeComponent();
            basicLoader(isProjectStart);
        }

        private void basicLoader(bool isProjectStart)
        {
            _isProjectStart = isProjectStart;
            if (isProjectStart)
            {
                panelStart();
            }
            else
            {
                _opacityDelayTimer = 30;
                _title = "До связи!";
                panelEnd();
            }
            TitleBlock.Text = _title;
        }

        private async void panelStart()
        {
            await opacityChanger(LeftSecondPanel, RightSecondPanel);
            await opacityChanger(LeftThirstPanel, RightThirstPanel);
            await opacityChanger(TopFirstPanel, DownFirstPanel);
            await opacityChanger(MainPanel, MainPanel);
            await startNavigation();
        }
        private async void panelEnd()
        {
            LeftSecondPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            RightSecondPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            TopFirstPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            DownFirstPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            LeftThirstPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            RightThirstPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;
            MainPanel.Style = Application.Current.Resources["StartPagePanel_end"] as Style;

            await opacityChanger(MainPanel, MainPanel);
            await opacityChanger(TopFirstPanel, DownFirstPanel);
            await opacityChanger(LeftThirstPanel, RightThirstPanel);
            await opacityChanger(LeftSecondPanel, RightSecondPanel);

            await endNavigation();
        }

        private async Task opacityChanger(StackPanel firstPanel, StackPanel secondPanel)
        {
            double counter = 0;
            if (_isProjectStart)
            {
                counter = 1.1;
                while (counter > 0)
                {
                    await Task.Delay(_opacityDelayTimer);
                    counter -= 0.1;
                    firstPanel.Opacity = counter;
                    secondPanel.Opacity = counter;
                }
            }
            else
            {
                while (counter < 1)
                {
                    await Task.Delay(_opacityDelayTimer);
                    counter += 0.1;
                    firstPanel.Opacity = counter;
                    secondPanel.Opacity = counter;
                }
            }
        }

        private async Task startNavigation()
        {
            await Task.Delay(70);
            if (!App.IsWindowClosing)
            {
                NavigationService.Navigate(new StartPage(true));
            }
        }
        private async Task endNavigation()
        {
            await Task.Delay(70);
            ((MainWindow)Application.Current.MainWindow).WindowFinalClosing();
        }
    }
}
