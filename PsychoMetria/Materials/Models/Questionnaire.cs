using PsychoMetria.Materials.Models.SupportModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PsychoMetria.Materials.Models
{
    public class Questionnaire
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private string _rowCode { get; set; }

        private List<Answer> _all_AnswersList = new List<Answer>();
        private List<AnswerInfluence> _all_AnswerInfluencesList = new List<AnswerInfluence>();
        private List<Question> _all_QuestionsList = new List<Question>();
        private List<Evaluation> _all_EvaluationsList = new List<Evaluation>();
        private List<ScaleAttach> _all_ScaleAttachesList = new List<ScaleAttach>();
        private List<Scale> _all_ScalesList = new List<Scale>();

        public EstimateType EstimateType;
        public bool IsMixedQuestions = false;

        public Questionnaire() //Создать новый тест, пока что без данных в папке Data
        {
        }

        public Questionnaire(string name) //Загрузить базовую информацию опроса
        {
            Name = name;
            SearchFile();

            string separator = ($"{'/'}{'\\'}");
            string[] row_parts = _rowCode.Split(new string[] { separator }, StringSplitOptions.None);

            //ОБЩАЯ ИНФОРМАЦИЯ                                                        
            string mainInfo = row_parts[0];
            decodeMainInfo(mainInfo);
        }

        public void DecodeFile()
        {
            decoder();
        }

        private void SearchFile()
        {
            string path = App.AppDataPath + $"\\{Name}";

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    _rowCode = sr.ReadToEnd();
                }
            }

            catch (Exception) { }
        }

        private void decoder()
        {
            string separator = ($"{'/'}{'\\'}");
            string[] row_parts = _rowCode.Split(new string[] { separator }, StringSplitOptions.None);

            //ОБЩАЯ ИНФОРМАЦИЯ                                                        
            string mainInfo = row_parts[0];
            decodeMainInfo(mainInfo);

            //ОТВЕТЫ                                                              
            string answers = row_parts[1];
            decodeAllAnswers(answers);

            //ВЛИЯНИЯ ОТВЕТОВ                                                      
            string influences = row_parts[2];
            decodeAllInfluences(influences);

            //ВОПРОСЫ                                                   
            string questions = row_parts[3];
            decodeAllQuestions(questions);

            //ОЦЕНКИ                                                   
            string evaluations = row_parts[4];
            decodeAllEvaluations(evaluations);

            //ПРИКРЕПЛЕННЫЕ ШКАЛЫ                                               
            string scaleAttaches = row_parts[5];
            decodeAllScaleAttaches(scaleAttaches);

            //ШКАЛЫ                                                                
            string scales = row_parts[6];
            decodeAllScales(scales);
        }


        public void EncodeToData()
        {
            App.CreateAppDataFolder();
            encoder();
        }

        private void encoder() //Закодировать все данные
        {
            string code = "";
            //ОБЩАЯ ИНФОРМАЦИЯ
            code += $"{Name}\\{Description}\\{IsMixedQuestions}\\{EstimateType.Estimate_Title}";
            code += "/\\";

            //ОТВЕТЫ
            code += encodeAllAnswers();
            code += "/\\";

            //ВЛИЯНИЯ ОТВЕТОВ
            code += encodeAllInfluences();
            code += "/\\";

            //ВОПРОСЫ
            code += encodeAllQuestions();
            code += "/\\";

            //ОЦЕНКИ
            code += encodeAllEvaluations();
            code += "/\\";

            //ПРИКРЕПЛЕННЫЕ ШКАЛЫ
            code += encodeAllScaleAttaches();
            code += "/\\";

            //ШКАЛЫ
            code += encodeAllScales();

            try
            {
                string path = App.AppDataPath + $"\\{Name}.ptm";

                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(code);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception) { MessageBox.Show("Произошел сбой при попытке сохранить код!", "Ошибка сохранения кода", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        /// <summary>
        /// ОБЩАЯ ИНФОРМАЦИЯ
        /// Базовые методы для взаимодействия с тестом
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>

        public void ChooseEstimateType(EstimateType estimateType)
        {
            EstimateType = estimateType;
        }

        public List<EstimateType> TakeAllEstimateTypes()
        {
            List<EstimateType> estimateTypes = new List<EstimateType>();
            estimateTypes.Add(new EstimateType("Балльная"));
            estimateTypes.Add(new EstimateType("Процентная"));

            return estimateTypes;
        }

        public void EditQuestionnaire(string name, string description, string estimateType, bool isMixUpQuestions)
        {
            Name = name;
            Description = description;
            EstimateType.EstimateChange(estimateType);
            IsMixedQuestions = isMixUpQuestions;
        }

        public void ClearAll()
        {
            Name = "";
            Description = "";
            _all_AnswersList = new List<Answer>();
            _all_AnswerInfluencesList = new List<AnswerInfluence>();
            _all_QuestionsList = new List<Question>();
            _all_EvaluationsList = new List<Evaluation>();
            _all_ScalesList = new List<Scale>();
            _all_ScaleAttachesList = new List<ScaleAttach>();

            EstimateType = null;
            IsMixedQuestions = false;
        }

        public void Delete()
        {
            try
            {
                string path = App.AppDataPath + $"\\{Name}.ptm";

                File.Delete(path);
            }
            catch (Exception) { MessageBox.Show("Произошел сбой при попытке удалить тест!", "Ошибка удаления теста", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void decodeMainInfo(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                Name = array[0];
                Description = array[1];
                IsMixedQuestions = Convert.ToBoolean(array[2]);
                EstimateType = new EstimateType(array[3]);
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }


        /// <summary>
        /// ANSWER
        /// Методы ответа
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>
        public Answer AddNewAnswer(int questionId)
        {
            Answer answer = new Answer();
            answer.Answer_Id = _all_AnswersList.Count;
            answer.Question_Id = questionId;
            _all_AnswersList.Add(answer);

            return answer;
        }

        public void DeleteAnswer(Answer answer)
        {
            int deleting_index = answer.Answer_Id;
            for (int i = deleting_index + 1; i < _all_AnswersList.Count; i++)
            {
                var changed = _all_AnswersList.Where(x => x.Answer_Id == i).FirstOrDefault();
                changed.Answer_Id--;
            }
            _all_AnswersList.Remove(answer);

            deleteSubordinateInfluences_byAnswer(answer.Answer_Id);
        }
        private void deleteSubordinateAnswers(int questionId)
        {
            var all_subordinate = _all_AnswersList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var subordinate in all_subordinate)
            {
                DeleteAnswer(subordinate);
            }
        }

        public void EditAnswer(int answerId, string answer_text, int questionId)
        {
            var old_answer = _all_AnswersList.FirstOrDefault(x => x.Answer_Id == answerId);
            old_answer.Overwrite(answer_text, questionId);
        }

        public List<Answer> TakeAllAnswers(int questionId)
        {
            var all_answers = _all_AnswersList.Where(x => x.Question_Id == questionId).ToList();
            return all_answers;
        }
        private string encodeAllAnswers()
        {
            string code = "";
            int i = _all_AnswersList.Count();
            foreach (var answer in _all_AnswersList)
            {
                code += answer.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllAnswers(string code)
        {
            if (code == "")
            {
                return;
            }
            string[] row_answers = code.Split('/');
            foreach (var row_answer in row_answers)
            {
                Answer answer = new Answer();
                answer.Decode(row_answer);
                _all_AnswersList.Add(answer);
            }
        }




        /// <summary>
        /// INFLUENCE
        /// Методы влияний ответов
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        private AnswerInfluence addNewAnswerInfluence(AnswerInfluence answerInfluence, int answerId)
        {
            answerInfluence.AnswerInfluence_Id = _all_AnswerInfluencesList.Count;
            answerInfluence.Answer_Id = answerId;
            _all_AnswerInfluencesList.Add(answerInfluence);
            return answerInfluence;
        }

        public void DeleteAnswerInfluence(AnswerInfluence answerInfluence)
        {
            if (answerInfluence.AnswerInfluence_Id == null)
            {
                return;
            }

            int deleting_index = Convert.ToInt32(answerInfluence.AnswerInfluence_Id);
            for (int i = deleting_index + 1; i < _all_AnswerInfluencesList.Count; i++)
            {
                var changed = _all_AnswerInfluencesList.Where(x => x.AnswerInfluence_Id == i).FirstOrDefault();
                changed.AnswerInfluence_Id--;
            }
            _all_AnswerInfluencesList.Remove(answerInfluence);
        }

        private void deleteSubordinateInfluences_byAnswer(int answerId)
        {
            var all_subordinates = _all_AnswerInfluencesList.Where(x => x.Answer_Id == answerId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteAnswerInfluence(subordinate);
            }
        }
        private void deleteSubordinateInfluences_byScaleAttach(int scaleAttachId)
        {
            var all_subordinates = _all_AnswerInfluencesList.Where(x => x.ScaleAttach_Id == scaleAttachId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteAnswerInfluence(subordinate);
            }
        }

        public void EditAnswerInfluence(AnswerInfluence answerInfluence, int answerId)
        {
            var old_answerInfluence = _all_AnswerInfluencesList.FirstOrDefault(x => x.AnswerInfluence_Id == answerInfluence.AnswerInfluence_Id);
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

            var scaleAttaches = _all_ScaleAttachesList.Where(x=> x.Question_Id == answer.Question_Id).ToList();
            foreach (var scaleAttach in scaleAttaches) 
            {
                var search_scale = _all_ScalesList.Where(x=> x.Scale_Id == scaleAttach.Scale_Id).FirstOrDefault();
                var search_influence = _all_AnswerInfluencesList.FirstOrDefault(x=> x.ScaleAttach_Id == scaleAttach.Attach_Id 
                    && x.Answer_Id == answer.Answer_Id);
                if (search_influence == null)
                {
                    AnswerInfluence answerInfluence = new AnswerInfluence(answer.Answer_Id, scaleAttach.Attach_Id, search_scale.Scale_Title);
                    all_influenceForAnswer.Add(answerInfluence);
                }
                else
                {
                    all_influenceForAnswer.Add(search_influence);
                }
            }

            return all_influenceForAnswer;
        }
        private string encodeAllInfluences()
        {
            string code = "";
            int i = _all_AnswerInfluencesList.Count();
            foreach (var influence in _all_AnswerInfluencesList)
            {
                code += influence.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllInfluences(string code)
        {
            if (code == "")
            {
                return;
            }

            string[] row_influences = code.Split('/');
            foreach (var row_influence in row_influences)
            {
                AnswerInfluence answerInfluence = new AnswerInfluence();
                answerInfluence.Decode(row_influence);
                _all_AnswerInfluencesList.Add(answerInfluence);
            }
        }




        /// <summary>
        /// QUESTION
        /// Методы вопроса
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="questionId"></param>    
        public Question AddNewQuestion()
        {
            Question question = new Question();
            question.Question_Id = _all_QuestionsList.Count;
            _all_QuestionsList.Add(question);

            return question;
        }

        public void DeleteQuestion(Question question)
        {
            int deleting_index = question.Question_Id;
            for (int i = deleting_index + 1; i < _all_QuestionsList.Count; i++)
            {
                var changed = _all_QuestionsList.Where(x => x.Question_Id == i).FirstOrDefault();
                changed.Question_Id--;
            }
            _all_QuestionsList.Remove(question);

            deleteSubordinateAnswers(question.Question_Id);
            deleteSubordinateScaleAttaches_byQuestion(question.Question_Id);
        }
        private void deleteAllQuestions()
        {
            foreach (var question in _all_QuestionsList)
            {
                DeleteQuestion(question);
            }
        }

        public void EditQuestion(int questionId, string question_title, string question_text, int questionType)
        {
            var old_question = _all_QuestionsList.FirstOrDefault(x => x.Question_Id == questionId);
            old_question.Overwrite(question_title, question_text, questionType);
        }

        public List<Question> TakeAllQuestions()
        {
            return _all_QuestionsList;
        }
        public void MoveQuestion(Question question, string moveDirection)
        {
            int deleting_index = question.Question_Id;
            if (moveDirection == "Up")
            {
                bool isWorked = false;
                for (int i = deleting_index - 1; i > 0; i--)
                {
                    var changed = _all_QuestionsList.Where(x => x.Question_Id == i).FirstOrDefault();
                    if (changed == null)
                    {
                        isWorked = true;
                        break;
                    }
                    changed.Question_Id--;
                }
                if (isWorked)
                {
                    var raised_question = _all_QuestionsList.Where(x => x.Question_Id == question.Question_Id).FirstOrDefault();
                    raised_question.Question_Id--;
                }
            }
            else if (moveDirection == "Down")
            {
                bool isWorked = false;
                for (int i = deleting_index + 1; i > _all_QuestionsList.Count; i--)
                {
                    var changed = _all_QuestionsList.Where(x => x.Question_Id == i).FirstOrDefault();
                    if (changed == null)
                    {
                        isWorked = true;
                        break;
                    }
                    changed.Question_Id++;
                }
                if (isWorked)
                {
                    var omitted_question = _all_QuestionsList.Where(x => x.Question_Id == question.Question_Id).FirstOrDefault();
                    omitted_question.Question_Id++;
                }
            }
        }
        public bool NameCheckoutQuestion(string name)
        {
            var searched_item = _all_QuestionsList.FirstOrDefault(x => x.Question_Title == name);
            if (searched_item == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string encodeAllQuestions()
        {
            string code = "";
            int i = _all_QuestionsList.Count();
            foreach (var question in _all_QuestionsList)
            {
                code += question.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllQuestions(string code)
        {
            if (code == "")
            {
                return;
            }

            string[] row_questions = code.Split('/');
            foreach (var row_question in row_questions)
            {
                Question question = new Question();
                question.Decode(row_question);
                _all_QuestionsList.Add(question);
            }
        }




        /// <summary>
        /// EVALUATION
        /// Методы оценок
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public Evaluation AddNewEvaluation(int scaleId)
        {
            Evaluation evaluation = new Evaluation();
            evaluation.Evaluation_Id = _all_EvaluationsList.Count;
            evaluation.Scale_Id = scaleId;
            _all_EvaluationsList.Add(evaluation);

            return evaluation;
        }

        public void DeleteEvaluation(Evaluation evaluation)
        {
            int deleting_index = evaluation.Evaluation_Id;
            for (int i = deleting_index + 1; i < _all_EvaluationsList.Count; i++)
            {
                var changed = _all_EvaluationsList.Where(x => x.Evaluation_Id == i).FirstOrDefault();
                changed.Evaluation_Id--;
            }
            _all_EvaluationsList.Remove(evaluation);
        }

        private void deleteSubordinateEvaluations(int scaleId)
        {
            var all_subordinates = _all_EvaluationsList.Where(x => x.Scale_Id == scaleId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteEvaluation(subordinate);
            }
        }

        public void EditEvaluation(int evaluationId, string evaluation_title, string evaluation_description, int start_range, int end_range)
        {
            var old_evaluation = _all_EvaluationsList.FirstOrDefault(x => x.Evaluation_Id == evaluationId);
            old_evaluation.Overwrite(evaluation_title, evaluation_description, start_range, end_range);
        }

        public List<Evaluation> TakeAllEvaluations(int scaleId)
        {
            var all_evaluationForScale = _all_EvaluationsList.Where(x => x.Scale_Id == scaleId).ToList();
            return all_evaluationForScale;
        }
        public bool NameCheckoutEvaluation(string name)
        {
            var searched_item = _all_EvaluationsList.FirstOrDefault(x => x.Evaluation_Title == name);
            if (searched_item == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RangesCheckoutEvaluation(Evaluation evaluation, int scaleId, int startRange, int endRange)
        {
            var all_evaluations = _all_EvaluationsList.Where(x=> x.Evaluation_Id != evaluation.Evaluation_Id && x.Scale_Id == scaleId).ToList();
            foreach (var item in all_evaluations)
            {
                if (item.StartRange >= startRange && item.StartRange <= endRange)
                {
                    return false;
                }
                if (item.EndRange >= startRange && item.EndRange <= endRange)
                {
                    return false;
                }
            }
            return true;
        }
        private string encodeAllEvaluations()
        {
            string code = "";
            int i = _all_EvaluationsList.Count();
            foreach (var evaluation in _all_EvaluationsList)
            {
                code += evaluation.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllEvaluations(string code)
        {
            if (code == "")
            {
                return;
            }

            string[] row_evaluations = code.Split('/');
            foreach (var row_evaluation in row_evaluations)
            {
                Evaluation evaluation = new Evaluation();
                evaluation.Decode(row_evaluation);
                _all_EvaluationsList.Add(evaluation);
            }
        }




        /// <summary>
        /// SCALE_ATTACH
        /// Методы прикрепления шкалы
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>
        public void AddNewScaleAttach(SupScaleAttach sup_scaleAttach, int questionId)
        {
            ScaleAttach scaleAttach = new ScaleAttach();
            scaleAttach.Attach_Id = _all_ScaleAttachesList.Count;
            scaleAttach.Scale_Id = sup_scaleAttach.Scale.Scale_Id;
            scaleAttach.Question_Id = questionId;
            _all_ScaleAttachesList.Add(scaleAttach);
        }

        public void DeleteScaleAttach(int scaleAttach_Id)
        {
            for (int i = scaleAttach_Id + 1; i < _all_ScaleAttachesList.Count; i++)
            {
                var changed = _all_ScaleAttachesList.Where(x => x.Attach_Id == i).FirstOrDefault();
                changed.Attach_Id--;
            }
            var deleting_attach = _all_ScaleAttachesList.FirstOrDefault(x => x.Attach_Id == scaleAttach_Id);
            _all_ScaleAttachesList.Remove(deleting_attach);
            deleteSubordinateInfluences_byScaleAttach(scaleAttach_Id);
        }

        private void deleteSubordinateScaleAttaches_byQuestion(int questionId)
        {
            var all_subordinates = _all_ScaleAttachesList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteScaleAttach(subordinate.Attach_Id);
            }
        }
        private void deleteSubordinateScaleAttaches_byScale(int scaleId)
        {
            var all_subordinates = _all_ScaleAttachesList.Where(x => x.Scale_Id == scaleId).ToList();
            foreach (var subordinate in all_subordinates)
            {
                DeleteScaleAttach(subordinate.Attach_Id);
            }
        }

        public List<SupScaleAttach> TakeAllScaleAttach(int questionId)
        {
            List<SupScaleAttach> supScaleAttaches = new List<SupScaleAttach>();

            var all_scaleAttaches = _all_ScaleAttachesList.Where(x => x.Question_Id == questionId).ToList();
            foreach (var scaleAttach in all_scaleAttaches)
            {
                var scale = _all_ScalesList.FirstOrDefault(x => x.Scale_Id == scaleAttach.Scale_Id);
                SupScaleAttach supScaleAttach = new SupScaleAttach(scale, scaleAttach, TakeAllEvaluations(scale.Scale_Id));
                supScaleAttaches.Add(supScaleAttach);
            }
            return supScaleAttaches;
        }

        public List<SupScaleAttach> TakeAllScaleAttach2_NonAttached(int questionId)
        {
            List<SupScaleAttach> supNonAttachedScales = new List<SupScaleAttach>();

            var all_scales = _all_ScalesList.ToList();
            foreach (var scale in all_scales)
            {
                var search_attach = _all_ScaleAttachesList.Where(x => x.Scale_Id == scale.Scale_Id && x.Question_Id == questionId).FirstOrDefault();
                if (search_attach == null)
                {
                    SupScaleAttach supScaleAttach = new SupScaleAttach(scale,
                        new ScaleAttach(questionId, scale.Scale_Id), TakeAllEvaluations(scale.Scale_Id));
                    supNonAttachedScales.Add(supScaleAttach);
                }
            }
            return supNonAttachedScales;
        }
        private string encodeAllScaleAttaches()
        {
            string code = "";
            int i = _all_ScaleAttachesList.Count();
            foreach (var scaleAttach in _all_ScaleAttachesList)
            {
                code += scaleAttach.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllScaleAttaches(string code)
        {
            if (code == "")
            {
                return;
            }

            string[] row_scaleAttaches = code.Split('/');
            foreach (var row_scaleAttach in row_scaleAttaches)
            {
                ScaleAttach scaleAttach = new ScaleAttach();
                scaleAttach.Decode(row_scaleAttach);
                _all_ScaleAttachesList.Add(scaleAttach);
            }
        }






        /// <summary>
        /// SCALE
        /// Методы шкал
        /// </summary>
        /// <param name="answerInfluence"></param>
        /// <param name="answerId"></param>    
        public Scale AddNewScale()
        {
            Scale scale = new Scale();
            scale.Scale_Id = _all_ScalesList.Count;
            _all_ScalesList.Add(scale);

            return scale;
        }

        public void DeleteScale(Scale scale)
        {
            int deleting_index = scale.Scale_Id;
            for (int i = deleting_index + 1; i < _all_ScalesList.Count; i++)
            {
                var changed = _all_ScalesList.Where(x => x.Scale_Id == i).FirstOrDefault();
                changed.Scale_Id--;
            }
            _all_ScalesList.Remove(scale);

            deleteSubordinateScaleAttaches_byScale(scale.Scale_Id);
            deleteSubordinateEvaluations(scale.Scale_Id);
        }
        private void deleteAllScales()
        {
            foreach (var scale in _all_ScalesList)
            {
                DeleteScale(scale);
            }
        }

        public void EditScale(int scaleId, string scale_title, string scale_description)
        {
            var old_scale = _all_ScalesList.FirstOrDefault(x => x.Scale_Id == scaleId);
            old_scale.Overwrite(scale_title, scale_description);
        }

        public List<Scale> TakeAllScales()
        {
            return _all_ScalesList;
        }
        public bool NameCheckoutScale(string name)
        {
            var searched_item = _all_ScalesList.FirstOrDefault(x=> x.Scale_Title == name);
            if (searched_item == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string encodeAllScales()
        {
            string code = "";
            int i = _all_ScalesList.Count();
            foreach (var scale in _all_ScalesList)
            {
                code += scale.Encode();
                if (i != 1)
                {
                    code += "/";
                }
                i--;
            }
            return code;
        }
        private void decodeAllScales(string code)
        {
            if (code == "")
            {
                return;
            }

            string[] row_scales = code.Split('/');
            foreach (var row_scale in row_scales)
            {
                Scale scale = new Scale();
                scale.Decode(row_scale);
                _all_ScalesList.Add(scale);
            }
        }
    }
}
