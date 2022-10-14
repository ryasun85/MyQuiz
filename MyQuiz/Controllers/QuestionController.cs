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
    public class QuestionController : Controller
    {
        private MyQuizEntities db = new MyQuizEntities();

        // GET: Question
        //public ActionResult Index()
        //{
        //    var tblQuestions = db.tblQuestions.Include(t => t.tlkpQuiz);
        //    return View(tblQuestions.ToList());
        //}
        //public ActionResult Index(int quizId)
        //{

        //    var questions = db.tlkpQuizs.FirstOrDefault(x => x.QuizID == quizId).tblQuestions.ToList();
        //    //var viewUser = questions.Select(s => s.QuestionText);
        //    var questionAndAnswers = db.tlkpQuizs.FirstOrDefault(x => x.QuizID == quizId).tblQuestions.Select(x => new {x.QuestionText,x.Option1,x.Option2,x.Option3,x.Option5}).ToList();
        //    var onlyQuestionText = db.tlkpQuizs.FirstOrDefault(x => x.QuizID == quizId).tblQuestions.Select(x => new { x.QuestionText }).ToList();

        //    if (User.IsInRole("Edit"))
        //    {
        //        return View(questions);
        //    }
        //    if (User.IsInRole("View"))
        //    {
        //        return View(questionAndAnswers);
        //    }
        //    if (User.IsInRole("Restricted"))
        //    {
        //        return View(onlyQuestionText);
        //    }
        //    return View(String.Empty);
        //}

        //ORIGINAL METHOD
        public ActionResult Index(int quizId)
        {
            var questions = db.tlkpQuizs.FirstOrDefault(x => x.QuizID == quizId).tblQuestions.ToList();
            return View(questions);
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            if (tblQuestion == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }

        // GET: Question/Create
        [Authorize(Roles = "Edit")]
        public ActionResult Create()
        {
            ViewBag.QuizID = new SelectList(db.tlkpQuizs, "QuizID", "QuizName");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Create([Bind(Include = "QuestionID,QuizID,QuestionText,Option1,Option2,Option3,Option4,Option5,CorrectAnswer")] tblQuestion tblQuestion, tlkpQuiz tlkpQuiz)
        {
            if (ModelState.IsValid)
            {
                var quizId = db.tlkpQuizs.Find(tlkpQuiz.QuizID);
                db.tblQuestions.Add(tblQuestion);
                db.SaveChanges();
                return RedirectToAction("Index", quizId);
            }

            ViewBag.QuizID = new SelectList(db.tlkpQuizs, "QuizID", "QuizName", tblQuestion.QuizID);
            return View(tblQuestion);
        }

        // GET: Question/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            if (tblQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizID = new SelectList(db.tlkpQuizs, "QuizID", "QuizName", tblQuestion.QuizID);
            return View(tblQuestion);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public ActionResult Edit([Bind(Include = "QuestionID,QuizID,QuestionText,Option1,Option2,Option3,Option4,Option5,CorrectAnswer")] tblQuestion tblQuestion, tlkpQuiz tlkpQuiz)
        {
            if (ModelState.IsValid)
            {
                var quizId = db.tlkpQuizs.Find(tlkpQuiz.QuizID);
                db.Entry(tblQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", quizId);
            }
            ViewBag.QuizID = new SelectList(db.tlkpQuizs, "QuizID", "QuizName", tblQuestion.QuizID);
            return View(tblQuestion);
        }
        //TODO ged Delete to return Index View correctly
        // GET: Question/Delete/5
        [Authorize(Roles = "Edit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
           
            if (tblQuestion == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }

        // POST: Question/Delete/5
        [Authorize(Roles = "Edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            db.tblQuestions.Remove(tblQuestion);
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
