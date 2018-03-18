using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    [Authorize] //Used to allow access to views related to a particular controller only when logged
    public class bookInfoController : Controller
    {
        private BookStoreModel db = new BookStoreModel();

        // GET: bookInfo
        public ActionResult Index()
        {
            var books = db.bookInfo.Include(a => a.bookName);
            ViewBag.BookCount = books.Count(); // .Count() property stores the number of books found by the search
                                               // and saves it in the ViewBag

            return View(db.bookInfo.OrderBy(a => a.bookName).ToList()); // Books ordered by name
        }

        // POST: bookInfoController/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String Title)
        {
            var books = from a in db.bookInfo select a;

            if (Title != "") //If the search box is not empty...
            {
                books = books.Where(a => a.bookName.Contains(Title)); //return books that matches the search query
            }

            ViewBag.BookCount = books.Count();
            return View(books);
        }

        // GET: bookInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookInfo bookInfo = db.bookInfo.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // The methods below implement CRUD functions

        // GET: bookInfo/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: bookInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookID,bookName,bookAuthor,bookGenre,bookEdition,bookReleaseYear")] bookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                db.bookInfo.Add(bookInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookInfo);
        }

        // GET: bookInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookInfo bookInfo = db.bookInfo.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: bookInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookID,bookName,bookAuthor,bookGenre,bookEdition,bookReleaseYear")] bookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookInfo);
        }

        // GET: bookInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookInfo bookInfo = db.bookInfo.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: bookInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookInfo bookInfo = db.bookInfo.Find(id);
            db.bookInfo.Remove(bookInfo);
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
