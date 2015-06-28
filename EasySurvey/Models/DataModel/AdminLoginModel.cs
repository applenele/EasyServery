using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{
    public class AdminLoginModel
    {
        [Display(Name="用户名")]
        public string  Username { get; set; }

        [Display(Name="密码")]
        public string  Password { get; set; }
    }
}