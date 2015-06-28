using EasySurvey.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySurvey.Controllers
{
    public class UserApiController : BaseController
    {
        // GET: UserApi
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            Models.User user = new Models.User();
            AjaxModel ajaxModel = new AjaxModel();
            user = db.Users.Where(u => u.Username == username || u.Phone == username).FirstOrDefault();
            if (user == null)
            {
                ajaxModel.State = "nouser";
                return Json(ajaxModel);
            }
            else
            {
                if (user.Password.Equals(Helper.Encryt.GetMD5(password)))
                {
                    ajaxModel.State = "ok";
                    ajaxModel.Values = user;
                    return Json(ajaxModel);
                }
                else
                {
                    ajaxModel.State = "fail";
                    return Json(ajaxModel);
                }
            }
        }


        [HttpPost]
        public ActionResult Register(string username,string password)
        {
            Models.User model = new Models.User();
            model = db.Users.Where(u => u.Phone == username ||  u.Username==username).FirstOrDefault();

            if (model == null)
            {
                Models.User user = new Models.User { Username = username, Password = Helper.Encryt.GetMD5(password), Phone = username, Sex = 0, Time = DateTime.Now };
                db.Users.Add(user);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("fail");
                }
            }
            else
            {
                return Content("exist");
            }
           
        }
    }
}