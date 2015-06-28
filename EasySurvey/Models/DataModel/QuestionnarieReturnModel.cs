using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{
    /// <summary>
    /// 返回的问卷的是形式
    /// </summary>
    public class QuestionnarieReturnModel
    {
        public int ID { get; set; }

        public string Title { set; get; }

        public int  UserID { get; set; }

        public string Time { get; set; }

        public List<QuestionReturnModel> Questions { set; get; }
    }
}