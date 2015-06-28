using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_UserQuestionnaire")]
    public class UserQuestionnaire
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Questionnaire")]
        public int QuestionnaireID { get; set; }

        public virtual Questionnaire  Questionnaire { get; set; }
    }
}