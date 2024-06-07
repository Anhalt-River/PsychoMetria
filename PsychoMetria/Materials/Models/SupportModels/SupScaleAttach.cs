using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models.SupportModels
{
    public class SupScaleAttach
    {
        public Scale Scale;
        public string Scale_Title;
        public ScaleAttach ScaleAttach;
        public int MaxRange { get; set; }

        public SupScaleAttach(Scale scale, ScaleAttach scaleAttach, List<Evaluation> evaluations)
        {
            Scale = scale;
            Scale_Title = Scale.Scale_Title;
            ScaleAttach = scaleAttach;

            int max = 0;
            foreach (var eval in evaluations)
            {
                if (eval.EndRange > max)
                {
                    max = eval.EndRange;
                }
            }
            MaxRange = max;
        }

        public void Overwrite(SupScaleAttach supScaleAttach)
        {
            Scale = supScaleAttach.Scale;
            Scale_Title = Scale.Scale_Title;
            ScaleAttach = supScaleAttach.ScaleAttach;
        }
    }
}
