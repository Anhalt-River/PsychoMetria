using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models
{
    public class Scale
    {
        public int Scale_Id { get; set; }
        private string _scale_title { get; set; }
        public string Scale_Title
        {
            get
            {
                return _scale_title;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _scale_title = value;
                }
            }
        }
        private string _scale_description { get; set; }
        private string Scale_Description
        {
            get
            {
                return _scale_description;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _scale_description = value;
                }
            }
        }
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        public Scale(string scale_title, string scale_description)
        {
            Scale_Title = scale_title;
            Scale_Description = scale_description;
        }
        public void Overwrite(Scale scale)
        {
            Scale_Title = scale.Scale_Title;
            Scale_Description = scale.Scale_Description;
            Evaluations = scale.Evaluations;
        }
    }
}
