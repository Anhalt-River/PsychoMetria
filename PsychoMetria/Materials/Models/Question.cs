using PsychoMetria.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria.Materials.Models
{
    public class Question
    {
        public int Question_Id { get; set; }

        public string _question_title { get; set; }
        public string Question_Title
        {
            get
            {
                return _question_title;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _question_title = value;
                }
            }
        }

        private string _question_text { get; set; }
        public string Question_Text
        {
            get
            {
                return _question_text;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _question_text = value;
                }
            }
        }
        public QuestionType QuestionType{ get; set; }

        public Question(string question_Title, string question_Text, int questionType_Id)
        {
            Question_Title = question_Title;
            Question_Text = question_Text;
            QuestionType = new QuestionType(questionType_Id);
        }

        public Question()
        {
            QuestionType = new QuestionType(1);
        }

        public void Overwrite(string question_title, string question_text, int questionType)
        {
            Question_Title = question_title;
            Question_Text = question_text;
            QuestionType = new QuestionType(questionType);
        }

        public string Encode()
        {
            string code = ($"{Question_Id}\\{Question_Title}\\{Question_Text}\\{QuestionType.QuestionType_Id}");
            return code;
        }

        public void Decode(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                Question_Id = Convert.ToInt32(array[0]);
                Question_Title = array[1];
                Question_Text = array[2];
                QuestionType = new QuestionType(Convert.ToInt32(array[3]));
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
