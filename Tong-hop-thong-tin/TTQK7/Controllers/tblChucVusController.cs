using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7.Models;

namespace TTQK7.Controllers
{
    public class tblChucVuController : Controller
    {
        private TTQK7Entities  db = new TTQK7Entities();

        // GET: tblChucVu
        public ActionResult Index()
        {
            return View(db.tblChucVu.ToList());
        }

        // GET: tblChucVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChucVu tblChucVu = db.tblChucVu.Find(id);
            if (tblChucVu == null)
            {
                return HttpNotFound();
            }
            return View(tblChucVu);
        }

        // GET: tblChucVu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblChucVu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idChucVu,TenChucVu")] tblChucVu tblChucVu)
        {
            if (ModelState.IsValid)
            {
                db.tblChucVu.Add(tblChucVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblChucVu);
        }

        // GET: tblChucVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChucVu tblChucVu = db.tblChucVu.Find(id);
            if (tblChucVu == null)
            {
                return HttpNotFound();
            }
            return View(tblChucVu);
        }

        // POST: tblChucVu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idChucVu,TenChucVu")] tblChucVu tblChucVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblChucVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblChucVu);
        }

        // GET: tblChucVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChucVu tblChucVu = db.tblChucVu.Find(id);
            if (tblChucVu == null)
            {
                return HttpNotFound();
            }
            return View(tblChucVu);
        }

        // POST: tblChucVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblChucVu tblChucVu = db.tblChucVu.Find(id);
            db.tblChucVu.Remove(tblChucVu);
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
