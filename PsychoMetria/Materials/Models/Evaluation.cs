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
        public int StartRange { get; set; }
        public int EndRange { get; set; }
    }
}