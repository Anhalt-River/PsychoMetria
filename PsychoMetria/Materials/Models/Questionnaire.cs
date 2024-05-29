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

        public Questionnaire(string file_path)
        {
            StreamReader file = new StreamReader(file_path);
            string all_text = file.ReadToEnd();
            char c = '/';
            string[] row_text = all_text.Split(c);
            Name = row_text[0];
            Description = row_text[1];
        }
    }
}
