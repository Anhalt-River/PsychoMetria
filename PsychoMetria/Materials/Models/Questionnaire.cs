using PsychoMetria.Materials.Models.SupportModels;
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

        private List<Answer> all_AnswersList = new List<Answer>();
        private List<AnswerInfluence> all_AnswerInfluencesList = new List<AnswerInfluence>();
        private List<Question> all_QuestionsList = new List<Question>();
        private List<Evaluation> all_EvaluationsList = new List<Evaluation>();
        private List<Scale> all_ScalesList = new List<Scale>();
        private List<ScaleAttach> all_ScaleAttachesList = new List<ScaleAttach>();


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
        /// Методы ответа
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>
        public Answer AddNewAnswer(Answer answer, int questionId)
        {
            answer.Answer_Id = all_AnswersList.Count;
            answer.Question_Id = questionId;
            all_AnswersList.Add(answer);
            return answer;
        }

        public void DeleteAnswer(Answer answer)
        {
            int deleting_index = answer.Answer_Id;
            for (int i = deleting_index + 1; i < all_AnswersList.Count; i++)
            {
                var changed = all_AnswersList.Where(x => x.Answer_Id == i).FirstOrDefault();
                changed.Answer_Id--;
            }
            all_AnswersList.Remove(answer);

            deleteSubordinateInfluences_byAnswer(answer.Answer_Id);
        }
        private void deleteSubordinateAnswers(int questionId)
        {
            var all_subordinate = all_AnswersList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var subordinate in all_subordinate)
            {
                DeleteAnswer(subordinate);
            }
        }

        public Answer EditAnswer(Answer answer)
        {
            var old_answer = all_AnswersList.FirstOrDefault(x => x.Answer_Id == answer.Answer_Id);
            old_answer.Overwrite(answer);
            return old_answer;
        }

        public List<Answer> TakeAllAnswers(int questionId)
        {
            var all_answers = all_AnswersList.Where(x => x.Question_Id == questionId).ToList();
            return all_answers;
        }




        /// <summary>
        /// Методы влияний ответов
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public AnswerInfluence AddNewAnswerInfluence(AnswerInfluence answerInfluence, int answerId)
        {
            answerInfluence.AnswerInfluence_Id = all_AnswerInfluencesList.Count;
            answerInfluence.Answer_Id = answerId;
            all_AnswerInfluencesList.Add(answerInfluence);
            return answerInfluence;
        }

        public void DeleteAnswerInfluence(AnswerInfluence answerInfluence)
        {
            int deleting_index = answerInfluence.AnswerInfluence_Id;
            for (int i = deleting_index + 1; i < all_AnswerInfluencesList.Count; i++)
            {
                var changed = all_AnswerInfluencesList.Where(x => x.AnswerInfluence_Id == i).FirstOrDefault();
                changed.AnswerInfluence_Id--;
            }
            all_AnswerInfluencesList.Remove(answerInfluence);
        }

        private void deleteSubordinateInfluences_byAnswer(int answerId)
        {
            var all_subordinates = all_AnswerInfluencesList.Where(x => x.Answer_Id == answerId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteAnswerInfluence(subordinate);
            }
        }
        private void deleteSubordinateInfluences_byScaleAttach(int scaleAttachId)
        {
            var all_subordinates = all_AnswerInfluencesList.Where(x => x.ScaleAttach_Id == scaleAttachId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteAnswerInfluence(subordinate);
            }
        }

        public AnswerInfluence EditAnswer(AnswerInfluence answerInfluence)
        {
            var old_answerInfluence = all_AnswerInfluencesList.FirstOrDefault(x => x.AnswerInfluence_Id == answerInfluence.AnswerInfluence_Id);
            old_answerInfluence.Overwrite(answerInfluence);
            return old_answerInfluence;
        }

        public List<AnswerInfluence> TakeAllInfluences(int answerId)
        {
            var all_influenceForAnswer = all_AnswerInfluencesList.Where(x => x.Answer_Id == answerId).ToList();
            return all_influenceForAnswer;
        }




        /// <summary>
        /// Методы вопроса
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>    
        public Question AddNewQuestion(Question question)
        {
            question.Question_Id = all_QuestionsList.Count;
            all_QuestionsList.Add(question);
            return question;
        }

        public void DeleteQuestion(Question question)
        {
            int deleting_index = question.Question_Id;
            for (int i = deleting_index + 1; i < all_QuestionsList.Count; i++)
            {
                var changed = all_QuestionsList.Where(x => x.Question_Id == i).FirstOrDefault();
                changed.Question_Id--;
            }
            all_QuestionsList.Remove(question);

            deleteSubordinateAnswers(question.Question_Id);
            deleteSubordinateScaleAttaches(question.Question_Id);
        }

        public Question EditQuestion(Question question)
        {
            var old_question = all_QuestionsList.FirstOrDefault(x => x.Question_Id == question.Question_Id);
            old_question.Overwrite(question);
            return old_question;
        }

        public List<Question> TakeAllQuestions(int questionId)
        {
            return all_QuestionsList;
        }




        /// <summary>
        /// Методы оценок
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public Evaluation AddNewEvaluation(Evaluation evaluation, int scaleId)
        {
            evaluation.Evaluation_Id = all_EvaluationsList.Count;
            evaluation.Scale_Id = scaleId;
            all_EvaluationsList.Add(evaluation);
            return evaluation;
        }

        public void DeleteEvaluation(Evaluation evaluation)
        {
            int deleting_index = evaluation.Evaluation_Id;
            for (int i = deleting_index + 1; i < all_EvaluationsList.Count; i++)
            {
                var changed = all_EvaluationsList.Where(x => x.Evaluation_Id == i).FirstOrDefault();
                changed.Evaluation_Id--;
            }
            all_EvaluationsList.Remove(evaluation);
        }

        private void deleteSubordinateEvaluations(int scaleId)
        {
            var all_subordinates = all_EvaluationsList.Where(x => x.Scale_Id == scaleId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteEvaluation(subordinate);
            }
        }

        public Evaluation EditEvaluation(Evaluation evaluation)
        {
            var old_evaluation = all_EvaluationsList.FirstOrDefault(x => x.Evaluation_Id == evaluation.Evaluation_Id);
            old_evaluation.Overwrite(evaluation);
            return old_evaluation;
        }

        public List<Evaluation> TakeAllEvaluations(int scaleId)
        {
            var all_evaluationForScale = all_EvaluationsList.Where(x => x.Scale_Id == scaleId).ToList();
            return all_evaluationForScale;
        }





        /// <summary>
        /// Методы прикрепления шкалы
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public SupScaleAttach AddNewScaleAttach(SupScaleAttach sup_scaleAttach, int questionId)
        {
            ScaleAttach scaleAttach = new ScaleAttach();
            scaleAttach.Attach_Id = all_ScaleAttachesList.Count;
            scaleAttach.Scale_Id = sup_scaleAttach.Scale.Scale_Id;
            scaleAttach.Question_Id = questionId;
            all_ScaleAttachesList.Add(scaleAttach);

            sup_scaleAttach.ScaleAttach.Attach_Id = scaleAttach.Attach_Id;
            return sup_scaleAttach;
        }

        public void DeleteScaleAttach(int scaleAttach_Id)
        {
            for (int i = scaleAttach_Id + 1; i < all_ScaleAttachesList.Count; i++)
            {
                var changed = all_ScaleAttachesList.Where(x => x.Attach_Id == i).FirstOrDefault();
                changed.Attach_Id--;
            }
            var deleting_attach = all_ScaleAttachesList.FirstOrDefault(x => x.Attach_Id == scaleAttach_Id);
            all_ScaleAttachesList.Remove(deleting_attach);
            deleteSubordinateInfluences_byScaleAttach(scaleAttach_Id);
        }

        private void deleteSubordinateScaleAttaches(int questionId)
        {
            var all_subordinates = all_ScaleAttachesList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteScaleAttach(subordinate.Attach_Id);
            }
        }

        public List<SupScaleAttach> TakeAllScaleAttach(int questionId)
        {
            List<SupScaleAttach> supScaleAttaches = new List<SupScaleAttach>();

            var all_scaleAttaches = all_ScaleAttachesList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var scaleAttach in all_scaleAttaches)
            {
                var scale = all_ScalesList.FirstOrDefault(x => x.Scale_Id == scaleAttach.Scale_Id);
                SupScaleAttach supScaleAttach = new SupScaleAttach(scale, scaleAttach, TakeAllEvaluations(scale.Scale_Id));
                supScaleAttaches.Add(supScaleAttach);
            }
            return supScaleAttaches;
        }

        public List<SupScaleAttach> TakeAllScaleAttach2_NonAttached(int questionId)
        {
            List<SupScaleAttach> supNonAttachedScales = new List<SupScaleAttach>();

            var all_scales = all_ScalesList.ToList();
            foreach (var scale in all_scales)
            {
                var search_attach = all_ScaleAttachesList.Where(x => x.Scale_Id == scale.Scale_Id && x.Question_Id == questionId).ToList();
                if (search_attach == null)
                {
                    SupScaleAttach supScaleAttach = new SupScaleAttach(scale,
                        new ScaleAttach(questionId, scale.Scale_Id), TakeAllEvaluations(scale.Scale_Id));
                    supNonAttachedScales.Add(supScaleAttach);
                }
            }
            return supNonAttachedScales;
        }






        /// <summary>
        /// Методы шкал
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
    }
}
