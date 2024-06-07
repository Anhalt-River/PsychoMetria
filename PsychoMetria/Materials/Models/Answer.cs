using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria.Materials.Models
{
    public class Answer
    {
        public int Answer_Id { get; set; }
        private string _answer_text { get; set; }
        public string Answer_Text 
        {
            get
            {
                return _answer_text;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _answer_text = value;
                }
            }
        }
        public int Question_Id { get; set; } = -1;

        public void Overwrite(string answer_text, int questionId)
        {
            Answer_Text = answer_text;
            Question_Id = questionId;
        }

        public string Encode()
        {
            string code = ($"{Answer_Id}\\{Answer_Text}\\{Question_Id}");
            return code;
        }

        public void Decode(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                Answer_Id = Convert.ToInt32(array[0]);
                Answer_Text = array[1];
                Question_Id = Convert.ToInt32(array[2]);
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
