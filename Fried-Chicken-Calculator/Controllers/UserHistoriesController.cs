using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fried_Chicken_Calculator.Models;

namespace Fried_Chicken_Calculator.Controllers
{
    public class UserHistoriesController : Controller
    {
        private CalculatorDBContext db = new CalculatorDBContext();

        // GET: UserHistories
        public ActionResult Index()
        {
            return View(db.UserHistories.ToList());
        }

        // GET: UserHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            return View(userHistory);
        }

        // GET: UserHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserHistories/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserNumber,History")] UserHistory userHistory)
        {
            if (ModelState.IsValid)
            {
                db.UserHistories.Add(userHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userHistory);
        }

        // GET: UserHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            return View(userHistory);
        }

        // POST: UserHistories/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserNumber,History")] UserHistory userHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userHistory);
        }

        // GET: UserHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            return View(userHistory);
        }

        // POST: UserHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserHistory userHistory = db.UserHistories.Find(id);
            db.UserHistories.Remove(userHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
