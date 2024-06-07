using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        public static bool IsQuestionOpened = false;
        public static bool IsAnswerOpened = false;
        public static string AppDataPath = $"{AppDomain.CurrentDomain.BaseDirectory}Data";

        public static void CreateAppDataFolder()
        {
            try
            {
                if (Directory.Exists(AppDataPath))
                {
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(AppDataPath);
            }
            catch (Exception) { }
        }
    }
}
