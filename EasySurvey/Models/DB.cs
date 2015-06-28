using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    public class DB : DbContext
    {
        public DB() :
            base("mysqldb")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<SysAnswer> SysAnswers { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<UserQuestionnaire> UserQuestionnaires { get; set; }
    }
}