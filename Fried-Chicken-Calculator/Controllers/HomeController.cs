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
                var usernum = context.Users.Where(u => u.UserName == username).FirstOrDefault();
                //var userto = context.UserTransferTos.Where(u => u.UserNumber == usernum.UserNumber).
                //var userin = context.UserTransferIns.Where(u => u.UserNumber == usernum.UserNumber);
                var userto = from d in context.UserTransferTos
                             where d.UserNumber == usernum.UserNumber
                             select d.ToHistory;
                var userin = from e in context.UserTransferIns
                             where e.UserNumber == usernum.UserNumber
                             select e.InHistory;
                ViewBag.contextview = userhistory;
                ViewBag.contextmoney = "账户余额："+ usermoney.FirstOrDefault();
                ViewBag.Message = Session["user"];
                ViewBag.TransferTo = userto;
                ViewBag.TransferIn = userin;

            }
            else
            {
                ViewBag.Message = "欢迎登录";
                ViewBag.contextview = "无";
                ViewBag.contextmoney = "请登录查看余额";
                ViewBag.TransferTo = "无";
                ViewBag.TransferIn = "无";
            }
            return View();
        }

        [HttpPost]
        public JsonResult IuserHistory(string AccountName,string AccountHistory)
        {
            CalculatorDBContext Context = new CalculatorDBContext();
            JsonResult result = new JsonResult();
            var contextuse = from a in Context.Users
                             where a.UserName == AccountName
                             select a.UserNumber;
            //var contextdb = from b in Context.UserHistories
            //                where b.UserNumber == contextuse.FirstOrDefault()
            //                select b;
            //foreach (var item in contextdb)
            //{
            //    item.History = AccountHistory;
            //    break;
            //}
            Context.UserHistories.Add(
                new UserHistory
                {
                    UserNumber = contextuse.FirstOrDefault(),
                    History = AccountHistory,
                }
                );
            Context.SaveChanges();
            result.ContentType = "message";
            result.Data = "success";
            return Json(result);
        }


        [HttpPost]
        public JsonResult Transfer(string User_number,string User_money)
        {
            //ViewBag.Message = Session["user"];
            string myname = Session["user"].ToString();
            double Imoney = Convert.ToDouble(User_money);
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
            //var contextmoney2 = from b in Context.Users
            //                   where b.UserName == Session["user"].ToString()
            //                   select b.UserMoney;
            var mymoney = Context.Users.Where(u => u.UserName == myname).FirstOrDefault();
            if (mymoney.UserMoney > Imoney)
            {
                mymoney.UserMoney = mymoney.UserMoney - Imoney;
                Context.UserTransferTos.Add(
                new UserTransferTo
                {
                    UserNumber = mymoney.UserNumber,
                    ToHistory = "向" + User_number + "转账" + User_money,

                }
                );
                Context.UserTransferIns.Add(
                    new UserTransferIn
                    {
                        UserNumber = User_number,
                        InHistory = mymoney.UserName + "向我转账" + User_money,
                    }
                    );
                Context.SaveChanges();
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