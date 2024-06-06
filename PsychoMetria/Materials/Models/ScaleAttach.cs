using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class ScaleAttach
    {
        public int Attach_Id { get; set; }
        public int Question_Id { get; set; }
        public int Scale_Id { get; set; }
        public ScaleAttach() { }

        public ScaleAttach(int questionId, int scaleId) 
        {
            Question_Id = questionId;
            Scale_Id = scaleId;
        }
    }
}
