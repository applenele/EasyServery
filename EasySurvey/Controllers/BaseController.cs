using EasySurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySurvey.Controllers
{
    public class BaseController : Controller
    {
        public DB db = new DB();
    }
}