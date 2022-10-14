using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace MyQuiz.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private MyQuizEntities db = new MyQuizEntities();

        // GET: Quiz
        public ActionResult Index()
        {
            return View(db.tlkpQuizs.ToList());
        }

        // GET: Quiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tlkpQuiz tlkpQuiz = db.tlkpQuizs.Find(id);
            if (tlkpQuiz == null)
            {
                return HttpNotFound();
            }
            return View(tlkpQuiz);
        }

        // GET: Quiz/Create
        [Authorize(Roles ="Edit")]
        public ActionResult Create()
        {
            return View();
        }
        //test
        // POST: Quiz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Create([Bind(Include = "QuizID,QuizName,QuizDescription")] tlkpQuiz tlkpQuiz)
        {
            if (ModelState.IsValid)
            {
                db.tlkpQuizs.Add(tlkpQuiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tlkpQuiz);
        }

        // GET: Quiz/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tlkpQuiz tlkpQuiz = db.tlkpQuizs.Find(id);
            if (tlkpQuiz == null)
            {
                return HttpNotFound();
            }
            return View(tlkpQuiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "QuizID,QuizName,QuizDescription")] tlkpQuiz tlkpQuiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tlkpQuiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tlkpQuiz);
        }

        // GET: Quiz/Delete/5
        [Authorize(Roles = "Edit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tlkpQuiz tlkpQuiz = db.tlkpQuizs.Find(id);
            if (tlkpQuiz == null)
            {
                return HttpNotFound();
            }
            return View(tlkpQuiz);
        }
        //TODO Quiz cannot be deletef before Questions aren't
        // POST: Quiz/Delete/5
        [Authorize(Roles = "Edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tlkpQuiz tlkpQuiz = db.tlkpQuizs.Find(id);
            db.tlkpQuizs.Remove(tlkpQuiz);
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
