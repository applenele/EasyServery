using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasySurvey.Models
{
    [Table("T_User")]
    public class User
    {
        public int ID { get; set; }

        public string  Username { get; set; }

        public string  Password { get; set; }

        public string Phone { get; set; }

       
        public int RoleAsInt { get; set; }

        [NotMapped]
        public Role Role 
        {
            get { return (Role)RoleAsInt; }
            set { RoleAsInt = (int)value; }
        }

        public string Status { get; set; }

        public Sex Sex { get; set; }

        public DateTime Time { get; set; }

    }

    public enum Sex
    { 
        男,
        女
    }

    public enum Role
    { 
        User,
        Admin
    }
}