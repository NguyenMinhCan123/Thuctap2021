using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7;
using Microsoft.AspNet.Identity;
namespace TTQK7.Views
{
    public class TrucBansController : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();

        // GET: TrucBans
        [Authorize]
        public ActionResult Index()
        {
            string username = User.Identity.GetUserName();
            tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
           
            int idUser = user.idUser;
            int donViID = 0;
            try
            {
                 donViID = user.idDonVi.Value;
            }
            catch (Exception)
            {
                 donViID = 0;
               
            }
          
            if (user.UserName == "admin")
            {
                return View(db.TrucBan.ToList());
            }
            return View(db.TrucBan.Where(x=>x.DonViID==donViID).OrderByDescending(x=>x.NgayTruc).ToList());
        }

        // GET: TrucBans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrucBan trucBan = db.TrucBan.Find(id);
            if (trucBan == null)
            {
                return HttpNotFound();
            }
            return View(trucBan);
        }

        // GET: TrucBans/Create
        public ActionResult Create()
        {
          
            return PartialView();
        }

        // POST: TrucBans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "TrucBanID,TrucBanTruong,TrucBanPho,NgayTruc")] TrucBan trucBan)
        {
            string username = User.Identity.GetUserName();
            tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
            //int donViID = user.idDonVi.Value;
            if (ModelState.IsValid)
            {
                //trucBan.DonViID = donViID;
                db.TrucBan.Add(trucBan);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(trucBan);
        }

        // GET: TrucBans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrucBan trucBan = db.TrucBan.Find(id);
            if (trucBan == null)
            {
                return HttpNotFound();
            }
            return PartialView(trucBan);
        }

        // POST: TrucBans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrucBanID,TrucBanTruong,TrucBanPho,NgayTruc,DonViID")] TrucBan trucBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trucBan).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return PartialView(trucBan);
        }

        // GET: TrucBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrucBan trucBan = db.TrucBan.Find(id);
            if (trucBan == null)
            {
                return HttpNotFound();
            }
            return View(trucBan);
        }

        // POST: TrucBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrucBan trucBan = db.TrucBan.Find(id);
            db.TrucBan.Remove(trucBan);
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
