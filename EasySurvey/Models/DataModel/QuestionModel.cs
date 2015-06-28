using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{

    /// <summary>
    /// 提交的时候 问题的形式
    /// </summary>
    public class QuestionModel
    {
        public string Title { get; set; }

        public string Q1 { get; set; }

        public string Q2 { get; set; }

        public string Q3 { get; set; }
        public string Q4 { get; set; }
    }
}