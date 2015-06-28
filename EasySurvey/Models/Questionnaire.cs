using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_Questionnaires")]
    public class Questionnaire
    {
        public int ID { get; set; }

        public string  Title { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public DateTime Time { set; get; }
    }
}