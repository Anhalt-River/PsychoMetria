using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoMetria.Materials.Models.SupportModels
{
    public class EstimateType
    {
        public string Estimate_Title { get; set; }
        public int Estimate_Id { get; set; }
        public void EstimateChange(string estimate_title)
        {
            Estimate_Title = estimate_title;
            switch (Estimate_Title)
            {
                case "Балльная":
                    Estimate_Id = 1;
                    break;
                case "Процентная":
                    Estimate_Id = 2;
                    break;
            }         
        }

        public EstimateType(string estimate_title)
        {
            EstimateChange(estimate_title);
        }
    }
}
