using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_Question")]
    public class Question
    {
        public int ID { get; set; }

        [ForeignKey("Questionnaire")]
        public int QuestionnaireID { get; set; }

        /// <summary>
        /// 问卷
        /// </summary>
        public virtual Questionnaire Questionnaire { get; set; }

        public string Title { get; set; }

        public DateTime Time { get; set; }

        public bool IsMulSelect { get; set; }

        public bool IsBlank { get; set; }
    }
}