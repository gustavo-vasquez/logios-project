using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Logios.Entities;

namespace Logios.Controllers
{
    public class UserTrophiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserTrophies
        public ActionResult Index()
        {
            var userTrophies = db.UserTrophies.Include(u => u.ApplicationUser).Include(u => u.Trophy);
            return View(userTrophies.ToList());
        }

        // GET: UserTrophies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTrophy userTrophy = db.UserTrophies.Find(id);
            if (userTrophy == null)
            {
                return HttpNotFound();
            }
            return View(userTrophy);
        }

        // GET: UserTrophies/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.TrophyId = new SelectList(db.Trophies, "TrophyId", "Description");
            return View();
        }

        // POST: UserTrophies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,TrophyId")] UserTrophy userTrophy)
        {
            if (ModelState.IsValid)
            {
                db.UserTrophies.Add(userTrophy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userTrophy.UserId);
            ViewBag.TrophyId = new SelectList(db.Trophies, "TrophyId", "Description", userTrophy.TrophyId);
            return View(userTrophy);
        }

        // GET: UserTrophies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTrophy userTrophy = db.UserTrophies.Find(id);
            if (userTrophy == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userTrophy.UserId);
            ViewBag.TrophyId = new SelectList(db.Trophies, "TrophyId", "Description", userTrophy.TrophyId);
            return View(userTrophy);
        }

        // POST: UserTrophies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,TrophyId")] UserTrophy userTrophy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTrophy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userTrophy.UserId);
            ViewBag.TrophyId = new SelectList(db.Trophies, "TrophyId", "Description", userTrophy.TrophyId);
            return View(userTrophy);
        }

        // GET: UserTrophies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTrophy userTrophy = db.UserTrophies.Find(id);
            if (userTrophy == null)
            {
                return HttpNotFound();
            }
            return View(userTrophy);
        }

        // POST: UserTrophies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserTrophy userTrophy = db.UserTrophies.Find(id);
            db.UserTrophies.Remove(userTrophy);
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
