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
                ViewBag.Message = Session["user"];
            }
            else
            {
                ViewBag.Message = "欢迎登录";
                ViewBag.contextview = "没有历史记录";
                ViewBag.contextmoney = "";
            }
            return View();
        }

        [HttpPost]
        public JsonResult Transfer(string User_number,string User_money)
        {
            ViewBag.Message = Session["user"];
            double Imoney = 0;
            CalculatorDBContext Context = new CalculatorDBContext();
            JsonResult result = new JsonResult();
            var contextmoney= from a in Context.Users
                              where a.UserNumber== User_number
                              select a;
            foreach(var item in contextmoney)
            {
                item.UserMoney = item.UserMoney + Convert.ToDouble(User_money);
                break;
            }
            var contextmoney2 = from b in Context.Users
                               where b.UserName == Session["user"].ToString()
                               select b.UserMoney;
            if (contextmoney2.FirstOrDefault() > Convert.ToDouble(User_money))
            {
                Context.SaveChanges();
                result.ContentType = "success";
                result.Data = "转账成功！";
            }
            else
            {
                result.ContentType = "error";
                result.Data = "转账失败，余额不足！";
            }
            return result;
        }

        public ActionResult Transfer()
        {
            ViewBag.Message = Session["user"];

            return View();
        }


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