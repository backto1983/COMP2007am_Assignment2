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
    public class bookLocationsController : Controller
    {
        private BookStoreModel db = new BookStoreModel();

        // GET: bookLocations
        public ActionResult Index()
        {
            var bookLocations = db.bookLocations.Include(b => b.bookInfo);
            return View(bookLocations.OrderBy(a => a.bookStoreLocation).ToList()); // Books ordered by store location
        }

        // GET: bookLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookLocation bookLocation = db.bookLocations.Find(id);
            if (bookLocation == null)
            {
                return HttpNotFound();
            }
            return View(bookLocation);
        }

        // The methods below implement CRUD functions

        // GET: bookLocations/Create
        public ActionResult Create()
        {
            ViewBag.bookID = new SelectList(db.bookInfo, "bookID", "bookName");
            return View();
        }

        // POST: bookLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookStoreNumber,bookID,bookStoreLocation,bookShelve,bookPhysical")] bookLocation bookLocation)
        {
            if (ModelState.IsValid)
            {
                db.bookLocations.Add(bookLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookID = new SelectList(db.bookInfo, "bookID", "bookName", bookLocation.bookID);
            return View(bookLocation);
        }

        // GET: bookLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookLocation bookLocation = db.bookLocations.Find(id);
            if (bookLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookID = new SelectList(db.bookInfo, "bookID", "bookName", bookLocation.bookID);
            return View(bookLocation);
        }

        // POST: bookLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookStoreNumber,bookID,bookStoreLocation,bookShelve,bookPhysical")] bookLocation bookLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookID = new SelectList(db.bookInfo, "bookID", "bookName", bookLocation.bookID);
            return View(bookLocation);
        }

        // GET: bookLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bookLocation bookLocation = db.bookLocations.Find(id);
            if (bookLocation == null)
            {
                return HttpNotFound();
            }
            return View(bookLocation);
        }

        // POST: bookLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookLocation bookLocation = db.bookLocations.Find(id);
            db.bookLocations.Remove(bookLocation);
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
