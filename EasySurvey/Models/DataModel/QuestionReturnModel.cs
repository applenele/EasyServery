using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{
    public class QuestionReturnModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Q1 { get; set; }

        public string Q2 { get; set; }

        public string Q3 { get; set; }
        public string Q4 { get; set; }
    }
}