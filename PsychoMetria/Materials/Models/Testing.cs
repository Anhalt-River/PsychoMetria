using PsychoMetria.Materials.Models.SupportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class Testing
    {
        public Questionnaire Taked_Questionnaire;

        public int QuestionProgress = 0;
        public int QuestionCount;

        private List<Question> _questionsUsed = new List<Question>();
        private List<ScaleInfluence> _workingScales = new List<ScaleInfluence>();

        public Testing(string questionCount, Questionnaire questionnaire)
        {
            QuestionCount = Convert.ToInt32(questionCount);
            Taked_Questionnaire = questionnaire;
            var scales = Taked_Questionnaire.TakeAllScales();
            foreach (var scale in scales)
            {
                ScaleInfluence scaleInfluence = new ScaleInfluence(scale);
                _workingScales.Add(scaleInfluence);
            }
        }

        public List<ScaleResult> TakeResults()
        {
            List<ScaleResult> scaleResults = new List<ScaleResult>();
            foreach (var work_scale in _workingScales)
            {
                Evaluation evaluation = Taked_Questionnaire.SearchScaleEvaluation(work_scale);
                if (evaluation != null)
                {
                    ScaleResult scaleResult = new ScaleResult(work_scale.Scale, evaluation);
                    var max = Taked_Questionnaire.GetMaxEvaluationInfluence(work_scale);
                    if (Taked_Questionnaire.EstimateType.Estimate_Id == 1)
                    {
                        if (work_scale.Influence > max)
                        {
                            work_scale.Influence = max;
                        }
                        scaleResult.Evaluation_Title += ($"({work_scale.Influence} из {max})");
                    }
                    else
                    {
                        scaleResult.Evaluation_Title += ($"({work_scale.Influence}%)");
                    }
                    scaleResults.Add(scaleResult);
                }
                else
                {
                    ScaleResult scaleResult = new ScaleResult(work_scale.Scale);
                    scaleResults.Add(scaleResult);
                }
            }
            return scaleResults;
        }

        public Tuple<Question, List<Answer>> NextQuestion()
        {
            if (QuestionProgress == QuestionCount)
            {
                return null;
            }

            if (!Taked_Questionnaire.IsMixedQuestions)
            {
                var question = Taked_Questionnaire.TakeQuestion(QuestionProgress);
                var answers = Taked_Questionnaire.TakeAllAnswers(QuestionProgress);

                QuestionProgress++;
                _questionsUsed.Add(question);
                return Tuple.Create(question, answers);
            }
            else
            {
                QuestionProgress++;

                List<Question> temp_list = new List<Question>();
                var all_questions = Taked_Questionnaire.TakeAllQuestions();
                foreach (var question in all_questions)
                {
                    var search_inUsed = _questionsUsed.FirstOrDefault(x=> x.Question_Id == question.Question_Id);
                    if (search_inUsed == null)
                    {
                        temp_list.Add(question);
                    }
                }

                if (temp_list.Count != 0)
                {
                    Random random = new Random();
                    int index = random.Next(0, temp_list.Count);
                    var answers = Taked_Questionnaire.TakeAllAnswers(index);
                    var array = temp_list.ToArray();

                    _questionsUsed.Add(array[index]);

                    return Tuple.Create(array[index], answers);
                }
                else
                {
                    return null;
                }
            }
        }

        public void TakeAnswer(Answer taked_answer)
        {
            if (_workingScales.Count > 0)
            {
                var all_influences = Taked_Questionnaire.TakeAllInfluences(taked_answer);
                foreach (var influence in all_influences)
                {
                    var scaleAttach = Taked_Questionnaire.TakeScaleAttach(influence.ScaleAttach_Id);
                    foreach (var scaleInfluence in _workingScales)
                    {
                        if (scaleInfluence.Scale.Scale_Id == scaleAttach.Scale_Id)
                        {
                            scaleInfluence.Influence += influence.Influence;
                        }
                    }
                }
            }
        }
    }
}
