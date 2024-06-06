using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class QuestionType
    {
        public int QuestionType_Id { get; set; }
        public string QuestionType_Title { get; set; }

        /// <summary>
        /// Выбор одного ответа - 1 Id
        /// Выбор нескольких ответов - 2 Id
        /// </summary>
        public QuestionType(int id)
        {
            if (id == 1)
            {
                QuestionType_Title = "Выбор одного ответа";
            }
            else if (id == 2)
            {
                QuestionType_Title = "Выбор нескольких ответов";
            }
        }

        public List<QuestionType> TakeAllTypes()
        {
            List<QuestionType> result = new List<QuestionType>();
            QuestionType questionType;

            questionType = new QuestionType(1);
            result.Add(questionType);
            questionType = new QuestionType(2);
            result.Add(questionType);

            return result;
        }
        public QuestionType() {}
    }
}
