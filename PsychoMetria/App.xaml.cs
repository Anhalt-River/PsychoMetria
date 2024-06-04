using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsWindowClosing = false;

        public static bool IsSettingsOpened = false;
        public static bool IsScalesOpened = false;
        public static bool IsEvaluationOpened = false;
    }
}
