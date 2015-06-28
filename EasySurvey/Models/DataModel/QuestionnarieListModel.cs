using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{
    public class QuestionnarieListModel
    {
        public int ID { get; set; }

        public string Title  { get; set; }

        public string Time { get; set; }

        public string Username { get; set; }

        public int UserID { set; get; }

        public QuestionnarieListModel() { }

        public QuestionnarieListModel(Questionnaire model)
        {
            this.ID = model.ID;
            this.Title = model.Title;
            this.Time = model.Time.ToString();
            this.Username = model.User.Username;
            this.UserID = model.UserID;
        }

    }
}