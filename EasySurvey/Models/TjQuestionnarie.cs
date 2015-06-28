using EasySurvey.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    public class TjQuestionnarie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public List<TjQuestion> Questions { get; set; }
    }
}