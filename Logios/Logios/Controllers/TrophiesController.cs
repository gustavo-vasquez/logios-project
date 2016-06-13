using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Logios.Entities;
using Logios.Services;

namespace Logios.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrophiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly TrophyService trophyServices = new TrophyService();

        // GET: Trophies
        public ActionResult Index()
        {
            return View(db.Trophies.ToList());
        }

        public PartialViewResult TrophiesList()
        {
            return PartialView("_TrophiesList", db.Trophies.ToList());
        }

        // GET: Trophies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = db.Trophies.Find(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // GET: Trophies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trophies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrophyId,Description,Points,Image")] Trophy trophy)
        {
            if (ModelState.IsValid)
            {
                db.Trophies.Add(trophy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trophy);
        }

        // GET: Trophies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = db.Trophies.Find(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrophyId,Description,Points,Image")] Trophy trophy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trophy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trophy);
        }

        // GET: Trophies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophy trophy = db.Trophies.Find(id);
            if (trophy == null)
            {
                return HttpNotFound();
            }
            return View(trophy);
        }

        // POST: Trophies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trophy trophy = db.Trophies.Find(id);
            db.Trophies.Remove(trophy);
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
