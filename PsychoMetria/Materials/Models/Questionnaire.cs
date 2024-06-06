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
        /// ANSWER
        /// Методы ответа
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>
        public void AddNewAnswer(int questionId)
        {
            Answer answer = new Answer();
            answer.Answer_Id = all_AnswersList.Count;
            answer.Question_Id = questionId;
            all_AnswersList.Add(answer);
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

        public void EditAnswer(int answerId, string answer_text, int questionId)
        {
            var old_answer = all_AnswersList.FirstOrDefault(x => x.Answer_Id == answerId);
            old_answer.Overwrite(answer_text, questionId);
        }

        public List<Answer> TakeAllAnswers(int questionId)
        {
            var all_answers = all_AnswersList.Where(x => x.Question_Id == questionId).ToList();
            return all_answers;
        }




        /// <summary>
        /// INFLUENCE
        /// Методы влияний ответов
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        private AnswerInfluence addNewAnswerInfluence(AnswerInfluence answerInfluence, int answerId)
        {
            answerInfluence.AnswerInfluence_Id = all_AnswerInfluencesList.Count;
            answerInfluence.Answer_Id = answerId;
            all_AnswerInfluencesList.Add(answerInfluence);
            return answerInfluence;
        }

        public void DeleteAnswerInfluence(AnswerInfluence answerInfluence)
        {
            if (answerInfluence.AnswerInfluence_Id == null)
            {
                return;
            }

            int deleting_index = Convert.ToInt32(answerInfluence.AnswerInfluence_Id);
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

        public void EditAnswer(AnswerInfluence answerInfluence, int answerId)
        {
            var old_answerInfluence = all_AnswerInfluencesList.FirstOrDefault(x => x.AnswerInfluence_Id == answerInfluence.AnswerInfluence_Id);
            if (old_answerInfluence == null) //Происходит создание влияния для ответа
            {
                addNewAnswerInfluence(answerInfluence, answerId);
            }
            else
            {
                old_answerInfluence.Overwrite(answerInfluence);
            }
        }

        public List<AnswerInfluence> TakeAllInfluences(Answer answer)
        {
            List<AnswerInfluence> all_influenceForAnswer = new List<AnswerInfluence>();

            var scaleAttaches = all_ScaleAttachesList.Where(x=> x.Question_Id == answer.Question_Id).ToList();
            foreach (var scaleAttach in scaleAttaches) 
            {
                var search_influence = all_AnswerInfluencesList.FirstOrDefault(x=> x.ScaleAttach_Id == scaleAttach.Attach_Id 
                    && x.Answer_Id == answer.Answer_Id);
                if (search_influence == null)
                {
                    AnswerInfluence answerInfluence = new AnswerInfluence(answer.Answer_Id, scaleAttach.Attach_Id);
                    all_influenceForAnswer.Add(answerInfluence);
                }
                else
                {
                    all_influenceForAnswer.Add(search_influence);
                }
            }

            return all_influenceForAnswer;
        }




        /// <summary>
        /// QUESTION
        /// Методы вопроса
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>    
        public void AddNewQuestion()
        {
            Question question = new Question();
            question.Question_Id = all_QuestionsList.Count;
            all_QuestionsList.Add(question);
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
            deleteSubordinateScaleAttaches_byQuestion(question.Question_Id);
        }
        private void deleteAllQuestions()
        {
            foreach (var question in all_QuestionsList)
            {
                DeleteQuestion(question);
            }
        }

        public void EditQuestion(int questionId, string question_title, string question_text, int questionType)
        {
            var old_question = all_QuestionsList.FirstOrDefault(x => x.Question_Id == questionId);
            old_question.Overwrite(question_title, question_text, questionType);
        }

        public List<Question> TakeAllQuestions()
        {
            return all_QuestionsList;
        }




        /// <summary>
        /// EVALUATION
        /// Методы оценок
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public void AddNewEvaluation(int scaleId)
        {
            Evaluation evaluation = new Evaluation();
            evaluation.Evaluation_Id = all_EvaluationsList.Count;
            evaluation.Scale_Id = scaleId;
            all_EvaluationsList.Add(evaluation);
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

        public void EditEvaluation(int evaluationId, string evaluation_title, string evaluation_description, int start_range, int end_range)
        {
            var old_evaluation = all_EvaluationsList.FirstOrDefault(x => x.Evaluation_Id == evaluationId);
            old_evaluation.Overwrite(evaluation_title, evaluation_description, start_range, end_range);
        }

        public List<Evaluation> TakeAllEvaluations(int scaleId)
        {
            var all_evaluationForScale = all_EvaluationsList.Where(x => x.Scale_Id == scaleId).ToList();
            return all_evaluationForScale;
        }





        /// <summary>
        /// SCALE_ATTACH
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

        private void deleteSubordinateScaleAttaches_byQuestion(int questionId)
        {
            var all_subordinates = all_ScaleAttachesList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteScaleAttach(subordinate.Attach_Id);
            }
        }
        private void deleteSubordinateScaleAttaches_byScale(int scaleId)
        {
            var all_subordinates = all_ScaleAttachesList.Where(x => x.Scale_Id == scaleId).ToList();
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
        /// SCALE
        /// Методы шкал
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>    
        public Scale AddNewScale(Scale scale)
        {
            scale.Scale_Id = all_ScalesList.Count;
            all_ScalesList.Add(scale);
            return scale;
        }

        public void DeleteScale(Scale scale)
        {
            int deleting_index = scale.Scale_Id;
            for (int i = deleting_index + 1; i < all_ScalesList.Count; i++)
            {
                var changed = all_ScalesList.Where(x => x.Scale_Id == i).FirstOrDefault();
                changed.Scale_Id--;
            }
            all_ScalesList.Remove(scale);

            deleteSubordinateScaleAttaches_byScale(scale.Scale_Id);
            deleteSubordinateEvaluations(scale.Scale_Id);
        }
        private void deleteAllScales()
        {
            foreach (var scale in all_ScalesList)
            {
                DeleteScale(scale);
            }
        }

        public Scale EditScale(Scale scale)
        {
            var old_scale = all_ScalesList.FirstOrDefault(x => x.Scale_Id == scale.Scale_Id);
            old_scale.Overwrite(scale);
            return old_scale;
        }

        public List<Scale> TakeAllScales()
        {
            return all_ScalesList;
        }
    }
}
