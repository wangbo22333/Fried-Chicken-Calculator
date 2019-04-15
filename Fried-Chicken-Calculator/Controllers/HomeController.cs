﻿using System;
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
            var contextuser = from a in context.Users
                              join b in context.UserHistories on a.UserNumber equals b.UserNumber
                              select new
                              {
                                  a.UserName,
                                  a.UserNumber,
                                  b.History,
                                  a.UserMoney
                              };



            if (Session["user"] != null)
            {
                ViewBag.Message = Session["user"] + " 欢迎您！";
            }
            else
            {
                ViewBag.Message = "欢迎登录";
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