using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PsychoMetria.Materials.Models
{
    public class AnswerInfluence
    {
        public int? AnswerInfluence_Id { get; set; } = null;
        public int Answer_Id { get; set; }
        public int ScaleAttach_Id { get; set; }
        public string Scale_Title { get; set; }
        public int Influence { get; set; } = 0;

        public void Overwrite(AnswerInfluence answerInfluence)
        {
            Influence = answerInfluence.Influence;
        }

        public AnswerInfluence() { }
        public AnswerInfluence(int answerId, int scaleAttachId, string scaleTitle)
        {
            Answer_Id = answerId;
            ScaleAttach_Id = scaleAttachId;
            Scale_Title = scaleTitle;
        }

        public string Encode()
        {
            string code = ($"{AnswerInfluence_Id}\\{Answer_Id}\\{ScaleAttach_Id}\\{Scale_Title}\\{Influence}");
            return code;
        }

        public void Decode(string code)
        {
            try
            {
                string[] array = code.Split('\\');
                AnswerInfluence_Id = Convert.ToInt32(array[0]);
                Answer_Id = Convert.ToInt32(array[1]);
                ScaleAttach_Id = Convert.ToInt32(array[2]);
                Scale_Title = array[3];
                Influence = Convert.ToInt32(array[4]);
            }
            catch (Exception) { MessageBox.Show("Загружаемый файл теста поврежден!", "Ошибка при загрузке!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
