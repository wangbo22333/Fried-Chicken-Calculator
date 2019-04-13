using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Fried_Chicken_Calculator.Models;

namespace Fried_Chicken_Calculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}
        //public ActionResult GetAuthCode()
        //{
        //    var context = new CalculatorDBContext();
        //    return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        //}

        //public ActionResult CheckLogin(string username, string password, string code)
        //{
        //    var context = new CalculatorDBContext();
        //    var contextuser = from a in context.Users
        //                      where a.UserName == username
        //                      select a.UserPassword;
        //    try
        //    {
        //        if (username == "")
        //            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
        //        else if (password == "123456")
        //            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
        //        else
        //            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "请验证帐号及密码！" }.ToJson());
        //    }

        //    catch (Exception ex)
        //    {
        //        return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
        //    }
        //}
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}