using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
