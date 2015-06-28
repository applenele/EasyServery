using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_Sys_Answer")]
    public class SysAnswer
    {
        public int ID { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }

        public string Content { get; set; }
    }
}