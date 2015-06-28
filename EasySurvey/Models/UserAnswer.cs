using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_UserAnswer")]
    public class UserAnswer
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Questionnaire")]
        public int QuestionnaireID { set; get; }

        public virtual Questionnaire Questionnaire { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }
    }
}