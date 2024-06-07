using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria.Materials.Models
{
    public class Scale
    {
        public int Scale_Id { get; set; }
        private string _scale_title { get; set; }
        public string Scale_Title
        {
            get
            {
                return _scale_title;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _scale_title = value;
                }
            }
        }
        private string _scale_description { get; set; }
        public string Scale_Description
        {
            get
            {
                return _scale_description;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _scale_description = value;
                }
            }
        }

        public Scale() { }

        public Scale(string scale_title, string scale_description)
        {
            Scale_Title = scale_title;
            Scale_Description = scale_description;
        }

        public void Overwrite(string scale_title, string scale_description)
        {
            Scale_Title = scale_title;
            Scale_Description = scale_description;
        }

        public string Encode()
        {
            string code = ($"{Scale_Id}\\{Scale_Title}\\{Scale_Description}");
            return code;
        }

        public void Decode(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                Scale_Id = Convert.ToInt32(array[0]);
                Scale_Title = array[1];
                Scale_Description = array[2];
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
