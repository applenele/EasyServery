using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySurvey.Models.DataModel
{
    public class TjQuestion
    {
        public int Id { get; set; }

        public string Title { set; get; }

        public int A1 { set; get; }

        public int A2 { get; set; }

        public int A3 { get; set; }

        public int A4 { get; set; }

    }
}