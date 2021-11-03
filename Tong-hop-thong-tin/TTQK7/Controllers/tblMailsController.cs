using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace TTQK7.Controllers
{
    public class tblMailsController : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();
        public ActionResult GetFile(string fileName)
        {
            try
            {
                //var fileStream = new FileStream(Server.MapPath("~/App_Data/FileDinhKem/") + fileName,
                //                            FileMode.Open,
                //                            FileAccess.Read
                //                          );
                //var fsResult = new FileStreamResult(fileStream, "application/pdf");
                //return fsResult;

                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/FileDinhKem/") + fileName);                
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            }
            catch (Exception)
            {

                //throw; 

            }
            return View("ErrorNotExistsView");
        }
        // GET: tblMails
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                return View(db.Get_ListMailSent(user.idUser).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: tblMails/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMail tblMail = db.tblMail.Find(id);
            if (tblMail == null)
            {
                return HttpNotFound();
            }
            //cập nhật trạng thái là đã xem
            string username = User.Identity.GetUserName();
            tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
            if (user!=null && user.idUser != tblMail.idUser) //không phải người gửi
            {
                tblMailUser mu = db.tblMailUser.Where(x => x.idMail == tblMail.idMail && x.idUser == user.idUser).FirstOrDefault();
                if (mu != null)
                {
                    mu.Status = 2;
                    db.Entry (mu).State = EntityState.Modified;
                    db.SaveChanges();
                }                
            }
            
            ViewBag.NguoiGui = tblMail.tblUser.FirstName + " " + tblMail.tblUser.LastName;
            ViewBag.NguoiNhan = db.Get_DSNguoiNhan(tblMail.idMail).FirstOrDefault ();
            var files = db.tblMailFile.Where(x => x.idMail == tblMail.idMail).FirstOrDefault();
            if (files!= null)
            {
                ViewBag.FileDinhKem = files.TenFile;
            }
            return View(tblMail);
        }

        // GET: tblMails/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                tblMail m = new tblMail();
                m.idUser = idUser;
                m.Ngay = DateTime.Now;
                ViewBag.Users = db.tblUser.Where(x => x.idUser != idUser);
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }            
        }

        // POST: tblMails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMail,Ngay,TieuDe,NoiDung,idUser")] tblMail tblMail, int[] chkNguoiNhan, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                tblMail.Ngay = DateTime.Now;
                db.tblMail.Add(tblMail);
                db.SaveChanges();
                if (upload != null && upload.ContentLength > 0)
                {
                    var img = Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/FileDinhKem/"),
                                        System.IO.Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    //thêm vào bảng file                    
                    tblMailFile f = new tblMailFile();
                    f.idMail = tblMail.idMail;
                    f.TenFile = upload.FileName;
                    db.tblMailFile.Add(f);
                    db.SaveChanges();
                }
                //thêm người nhận
                if (chkNguoiNhan.Length > 0)
                {
                    if (chkNguoiNhan[0] ==0) //tất cả
                    {
                        var listUser = db.tblUser.Where(x => x.idUser != tblMail.idUser);
                        foreach (var item in listUser)
                        {
                            tblMailUser mu = new tblMailUser();
                            mu.idUser = item.idUser;
                            mu.idMail = tblMail.idMail;
                            mu.Status = 1;
                            mu.TraLoi = "";
                            db.tblMailUser.Add(mu);
                        }
                    }else
                    {
                        foreach (var i in chkNguoiNhan)
                        {
                            tblMailUser mu = new tblMailUser();
                            mu.idUser = i;
                            mu.idMail = tblMail.idMail;
                            mu.Status = 1;
                            mu.TraLoi = "";
                            db.tblMailUser.Add(mu);
                        }
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }else
            {
                if (Request.IsAuthenticated)
                {
                    string username = User.Identity.GetUserName();
                    tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                    int idUser = user.idUser;
                    tblMail m = new tblMail();
                    m.idUser = idUser;
                    m.Ngay = DateTime.Now;
                    ViewBag.Users = db.tblUser.Where(x => x.idUser != idUser);
                    return View(m);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }            
        }

        // GET: tblMails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMail tblMail = db.tblMail.Find(id);
            if (tblMail == null)
            {
                return HttpNotFound();
            }
            return View(tblMail);
        }

        // POST: tblMails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMail,Ngay,TieuDe,NoiDung,idUser")] tblMail tblMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblMail);
        }

        // GET: tblMails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMail tblMail = db.tblMail.Find(id);
            if (tblMail == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblMail);            
        }

        // POST: tblMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMail tblMail = db.tblMail.Find(id);
            db.tblMail.Remove(tblMail);
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
