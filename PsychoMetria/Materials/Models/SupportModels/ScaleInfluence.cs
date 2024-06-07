using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models.SupportModels
{
    public class ScaleInfluence
    {
        public Scale Scale;
        public int Influence = 0;

        public ScaleInfluence(Scale scale)
        {
            Scale = scale;
        }
    }
}
