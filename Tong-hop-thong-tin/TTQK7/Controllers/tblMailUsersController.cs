using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7.Models;
using Microsoft.AspNet.Identity;

namespace TTQK7.Controllers
{
    public class tblMailUsersController : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();       

        // GET: tblMailUsers
        public ActionResult Index()
        {
            var tblMailUsers = db.tblMailUser.Include(t => t.tblMail).Include(t => t.tblUser);
            return View(tblMailUsers.ToList());
        }

        public ActionResult Inbox()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                var tblMailUsers = db.tblMailUser.Where(t => t.idUser == idUser && t.Status != 3).Include(t => t.tblMail).Include(t => t.tblUser);
                return View(tblMailUsers.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult MailDel()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                var tblMailUsers = db.tblMailUser.Where(t => t.idUser == idUser && t.Status == 3).Include(t => t.tblMail).Include(t => t.tblUser);
                return View(tblMailUsers.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ChuaDoc()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                var tblMailUsers = db.tblMailUser.Where(t => t.idUser == idUser && t.Status == 1).Include(t => t.tblMail).Include(t => t.tblUser);
                return View(tblMailUsers.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
         }       

        // GET: tblMailUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMailUser tblMailUser = db.tblMailUser.Find(id);

            //Update Status = 2 // Đã xem
            tblMailUser.Status = 2;
            db.SaveChanges();

            if (tblMailUser == null)
            {
                return HttpNotFound();
            }            
            return View(tblMailUser);
        }

        // GET: tblMailUsers/Create
        public ActionResult Create()
        {
            ViewBag.idMail = new SelectList(db.tblMail, "idMail", "TieuDe");
            ViewBag.idUser = new SelectList(db.tblUser, "idUser", "FirstName");
            return View();
        }

        // POST: tblMailUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMailUser,idMail,idUser,IsRead,IsDel,TraLoi")] tblMailUser tblMailUser)
        {
            if (ModelState.IsValid)
            {
                db.tblMailUser.Add(tblMailUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idMail = new SelectList(db.tblMail, "idMail", "TieuDe", tblMailUser.idMail);
            ViewBag.idUser = new SelectList(db.tblUser, "idUser", "FirstName", tblMailUser.idUser);
            return View(tblMailUser);
        }

        // GET: tblMailUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMailUser tblMailUser = db.tblMailUser.Find(id);
            if (tblMailUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMail = new SelectList(db.tblMail, "idMail", "TieuDe", tblMailUser.idMail);
            ViewBag.idUser = new SelectList(db.tblUser, "idUser", "FirstName", tblMailUser.idUser);
            return View(tblMailUser);
        }

        // POST: tblMailUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMailUser,idMail,idUser,IsRead,IsDel,TraLoi")] tblMailUser tblMailUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMailUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMail = new SelectList(db.tblMail, "idMail", "TieuDe", tblMailUser.idMail);
            ViewBag.idUser = new SelectList(db.tblUser, "idUser", "FirstName", tblMailUser.idUser);
            return View(tblMailUser);
        }

        // GET: tblMailUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMailUser tblMailUser = db.tblMailUser.Find(id);
            if (tblMailUser == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblMailUser);
        }

        // POST: tblMailUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMailUser tblMailUser = db.tblMailUser.Find(id);
            //db.tblMailUsers.Remove(tblMailUser);
            //Update Status = 3 // Xóa
            tblMailUser.Status = 3;
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
