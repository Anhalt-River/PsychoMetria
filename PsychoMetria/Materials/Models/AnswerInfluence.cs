using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class AnswerInfluence
    {
        public int? AnswerInfluence_Id { get; set; } = null;
        public int Answer_Id { get; set; }
        public int ScaleAttach_Id { get; set; }
        public int Influence { get; set; } = 0;

        public void Overwrite(AnswerInfluence answerInfluence)
        {
            Influence = answerInfluence.Influence;
        }

        public AnswerInfluence() { }
        public AnswerInfluence(int answerId, int scaleAttachId)
        {
            Answer_Id = answerId;
            ScaleAttach_Id = scaleAttachId; 
        }
    }
}
