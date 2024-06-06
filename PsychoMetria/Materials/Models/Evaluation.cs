﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PsychoMetria.Materials.Models
{
    public class Evaluation
    {
        public int Evaluation_Id { get; set; }
        private string _evaluation_title { get; set; }
        public string Evaluation_Title
        {
            get
            {
                return _evaluation_title;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _evaluation_title = value;
                }
            }
        }
        private string _evaluation_description { get; set; }
        public string Evaluation_Description
        {
            get
            {
                return _evaluation_description;
            }
            set
            {
                if (!value.Contains('\\') && !value.Contains('/'))
                {
                    _evaluation_description = value;
                }
            }
        }
        public int Scale_Id { get; set; }
        public int StartRange { get; set; } = 0;
        public int EndRange { get; set; } = 0;
        public string RangeOfValuesString { get; set; } = ("От 0 и До 0");

        public void Overwrite(string evaluation_title, string evaluation_description, int start_range, int end_range)
        {
            Evaluation_Title = evaluation_title;
            Evaluation_Description = evaluation_description;
            StartRange = start_range;
            EndRange = end_range;

            RangeOfValuesString = ($"От {StartRange} и До {EndRange}");
        }
    }
}
