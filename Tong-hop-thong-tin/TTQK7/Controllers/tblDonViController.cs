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
    public class tblDonViController : Controller
    {
        private TTQK7Entities  db = new TTQK7Entities ();
        public string getPath(int? parentID)
        {
            string path = "";
            tblDonVi pb = db.tblDonVi.Find(parentID);
            while (pb != null)
            {
                path = pb.TenDonVi  + "\\" + path;
                pb = db.tblDonVi.Find(pb.ParentID);
            }
            return path;
        }
        public JsonResult IsExistsChild(string DepartmentName, int ParentID)
        {
            return Json(!db.tblDonVi.Any(x => x.ParentID == ParentID && x.TenDonVi .ToLower() == DepartmentName.ToLower()), JsonRequestBehavior.AllowGet);
        }
        // GET: tblDonVi
        public ActionResult Index()
        {
            return View(db.tblDonVi.ToList());
        }

        // GET: tblDonVi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonVi tblDonVi = db.tblDonVi.Find(id);
            if (tblDonVi == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblDonVi);
        }

        // GET: tblDonVi/Create
        public ActionResult Create(int parentID)
        {
            tblDonVi  pb = new tblDonVi ();
            pb.ParentID = parentID;
            pb.IsLast = true;
            ViewBag.path = getPath(parentID);
            return PartialView();
        }

        // POST: tblDonVi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonViID,TenDonVi, ParentID, IsLast")] tblDonVi tblDonVi)
        {
            if (ModelState.IsValid)
            {
                db.tblDonVi.Add(tblDonVi);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return View(tblDonVi);
        }

        // GET: tblDonVi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonVi tblDonVi = db.tblDonVi.Find(id);
            if (tblDonVi == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblDonVi);
        }

        // POST: tblDonVi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonViID,TenDonVi,ParentID, IsLast")] tblDonVi tblDonVi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDonVi).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return View(tblDonVi);
        }

        // GET: tblDonVi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonVi tblDonVi = db.tblDonVi.Find(id);
            if (tblDonVi == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblDonVi);
        }

        // POST: tblDonVi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDonVi tblDonVi = db.tblDonVi.Find(id);
            db.tblDonVi.Remove(tblDonVi);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
