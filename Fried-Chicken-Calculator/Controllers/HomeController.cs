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
        // GET: /Account/Login


        [HttpPost]
        public JsonResult Login(string Account, string Password)
        {
            CalculatorDBContext Context = new CalculatorDBContext();
            var contextuser = from a in Context.Users
                              where a.UserName == Account
                              select a.UserPassword;
            JsonResult result = new JsonResult();
            if(Password == contextuser.FirstOrDefault())
            {
                result.ContentType = "message";
                result.Data = "success";
                return Json(result);
            }
            else
            {
                result.ContentType = "message";
                result.Data = "error"; 
                return Json(result);
            }
           
            
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}