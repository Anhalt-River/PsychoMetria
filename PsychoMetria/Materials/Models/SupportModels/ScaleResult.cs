using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models.SupportModels
{
    public class ScaleResult
    {
        public string Title { get; set; }
        public string Scale_Description { get; set; }
        public string Evaluation_Title { get; set; }
        public string Evaluation_Description { get; set; }

        public ScaleResult(Scale scale, Evaluation evaluation)
        {
            Title = scale.Scale_Title;
            Scale_Description = scale.Scale_Description;
            Evaluation_Title = evaluation.Evaluation_Title;
            Evaluation_Description = evaluation.Evaluation_Description;
        }

        public ScaleResult(Scale scale)
        {
            Title = scale.Scale_Title;
            Scale_Description = scale.Scale_Description;
            Evaluation_Title = "Не удалось получить оценку";
            Evaluation_Description = "Не удалось получить оценку";
        }
    }
}
