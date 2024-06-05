using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class Questionnaire
    {    
        public string Name { get; set; }
        public string Description { get; set; }
        public string Row_Code { get; set; }

        public List<Answer> All_Answers = new List<Answer>();
        public List<AnswerInfluence> All_AnswerInfluences = new List<AnswerInfluence>();
        public List<Evaluation> All_Evaluations = new List<Evaluation>();
        public List<Question> All_Questions = new List<Question>();
        public List<Scale> All_Scales = new List<Scale>();
        public List<ScaleAttach> All_ScaleAttaches = new List<ScaleAttach>();


        public Questionnaire(string file_path)
        {
            StreamReader file = new StreamReader(file_path);
            string all_text = file.ReadToEnd();
            char c = '/';
            string[] row_text = all_text.Split(c);
            Name = row_text[0];
            Description = row_text[1];
        }

        /// <summary>
        /// Все методы ответа
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>
        public void AddNewAnswer(Answer answer, int questionId)
        {             
            answer.Answer_Id = All_Answers.Count;
            answer.Question_Id = questionId;
            All_Answers.Add(answer);
        }

        public void DeleteAnswer(Answer answer)
        {
            int deleting_index = answer.Answer_Id;
            for (int i = deleting_index + 1; i < All_Answers.Count; i++)
            {
                var changed = All_Answers.Where(x => x.Answer_Id == i).FirstOrDefault();
                changed.Answer_Id--;
            }
            All_Answers.Remove(answer);

            //ДОБАВИТЬ ДОЧЕРНИЕ УДАЛЕНИЯ ВЛИЯНИЯ
        }
        public void EditAnswer(Answer answer)
        {
            var old_answer = All_Answers.FirstOrDefault(x => x.Answer_Id == answer.Answer_Id);
            old_answer.Overwrite(answer);
        }
    }
}
