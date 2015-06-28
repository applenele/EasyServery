using EasySurvey.Models;
using EasySurvey.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySurvey.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                Models.User user = new Models.User();
                string password = Helper.Encryt.GetMD5(model.Password);
                user = db.Users.Where(u => u.Username == model.Username && u.Password == password && u.RoleAsInt == 1).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "用户名或者密码输入错误！");
                }
                else
                {
                    return RedirectToAction("Api", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("","验证信息输入错误！");
            }
            return View(model);
        }
        
    }
}