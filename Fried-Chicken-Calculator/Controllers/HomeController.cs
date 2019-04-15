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
            CalculatorDBContext Context = new CalculatorDBContext();
            var context = new CalculatorDBContext();

            if (Session["user"] != null)
            {
                string username = Session["user"].ToString();
                var usernumber = from a in context.Users
                                 where a.UserName == username
                                 select a.UserNumber;
                //var userinfo = from a in context.Users
                //               where a.UserName == username
                //               select a;
                var userhistory = from b in context.UserHistories
                                  where b.UserNumber == usernumber.FirstOrDefault()
                                  select b.History;
                var usermoney = from c in context.Users
                                 where c.UserName == username
                                 select c.UserMoney;
                ViewBag.contextview = userhistory;
                ViewBag.contextmoney = "账户余额："+ usermoney.FirstOrDefault();
                ViewBag.Message = Session["user"] + " 欢迎您！";
            }
            else
            {
                ViewBag.Message = "欢迎登录";
                ViewBag.contextview = "没有历史记录";
                ViewBag.contextmoney = "";
            }
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}



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
                Session["user"] = Account;
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
        public ActionResult LoginOut()
        {
            if(Session["user"] != null)
            {
                Session["user"] = null;
            }
            return RedirectToRoute(new { controller = "Home", action= "Index" });
        }


        [HttpGet]
        public string GetSession()
        {
            if (Session["user"] == null)
            {
                return "";
            }
            else
            {
                string result = Session["user"].ToString();
                return (result);
            }
            
        }
    }
}