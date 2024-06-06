using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
