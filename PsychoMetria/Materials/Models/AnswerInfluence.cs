using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class AnswerInfluence
    {
        public int AnswerInfluence_Id { get; set; }
        public int Answer_Id { get; set; }
        public int ScaleAttach_Id { get; set; }
        public int Influence { get; set; } = 0;

        public void Overwrite(AnswerInfluence answerInfluence)
        {
            Influence = answerInfluence.Influence;
        }
    }
}
