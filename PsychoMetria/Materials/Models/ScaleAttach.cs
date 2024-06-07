using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria.Materials.Models
{
    public class ScaleAttach
    {
        public int Attach_Id { get; set; }
        public int Question_Id { get; set; }
        public int Scale_Id { get; set; }
        public ScaleAttach() { }

        public ScaleAttach(int questionId, int scaleId) 
        {
            Question_Id = questionId;
            Scale_Id = scaleId;
        }

        public string Encode()
        {
            string code = ($"{Attach_Id}\\{Question_Id}\\{Scale_Id}");
            return code;
        }

        public void Decode(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                Attach_Id = Convert.ToInt32(array[0]);
                Question_Id = Convert.ToInt32(array[1]);
                Scale_Id = Convert.ToInt32(array[2]);
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
