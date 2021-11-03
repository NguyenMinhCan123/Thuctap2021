using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7.Models;
using System.Security.Cryptography;
using System.Text;

namespace TTQK7.Controllers
{
    public class tblUserController : Controller
    {
        private TTQK7Entities  db = new TTQK7Entities();

        public JsonResult IsExists(string UserName)
        {
            return Json(!db.tblUser.Any(x => x.UserName.ToLower() == UserName.ToLower()), JsonRequestBehavior.AllowGet);
        }

        // GET: tblUser
        [Authorize]
        public ActionResult Index()
        {
            var tblUser = db.tblUser.Include(t => t.tblChucVu).Include(t => t.tblDonVi).OrderBy(t=> t.tblDonVi.DonViID).OrderBy(t=> t.LastName);
            return View(tblUser.ToList());
        }

        // GET: tblUser/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: tblUser/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu");
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi");
            ComboTreeController cb = new ComboTreeController();
            ViewBag.DataTree = cb.GetTreeData();
            return PartialView();
        }

        // POST: tblUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "idUser,FirstName,LastName,UserName,Password,idDonVi,idChucVu")] tblUser tblUser, string cboDonVi)
        {
            if (ModelState.IsValid)
            {
                // Mã hóa password
                tblUser.Password = getMd5Hash(tblUser.Password);
                //lấy ra tên đơn vị
                tblDonVi dv = db.tblDonVi.Where(x => x.TenDonVi.ToLower() == cboDonVi.ToLower()).FirstOrDefault();
                if (dv!=null)
                {
                    tblUser.idDonVi = dv.DonViID;
                    tblUser.idChucVu = 1;
                    db.tblUser.Add(tblUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                
            }

            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu", tblUser.idChucVu);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "idDonVi", "TenDonVi", tblUser.idDonVi);
            return PartialView(tblUser);
        }

        //ChangePassword
        [Authorize]
        public ActionResult ChangePassword(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu", tblUser.idChucVu);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi", tblUser.idDonVi);
            return PartialView(tblUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(string mkcu, string mk1, string mk2, int idUser)
        {         
            string matKhau = getMd5Hash(mkcu);
            tblUser obj = db.tblUser.Find(idUser);          
            //nhapLaiMatKhau == mkcu
            if (ModelState.IsValid && obj!=null)
            {
                if (mk1 == mk2 && matKhau  == obj.Password)
                {
                    obj .Password = getMd5Hash(mk1);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }        
            }
            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu", obj.idChucVu);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi", obj .idDonVi);           
            return View(obj);
        }

        // GET: tblUser/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu", tblUser.idChucVu);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi", tblUser.idDonVi);
            return PartialView(tblUser);
        }

        // POST: tblUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "idUser,FirstName,LastName,UserName,Password,idDonVi,idChucVu")] tblUser tblUser)
        { 
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.idChucVu = new SelectList(db.tblChucVu, "idChucVu", "TenChucVu", tblUser.idChucVu);
            ViewBag.idDonVi = new SelectList(db.tblDonVi, "DonViID", "TenDonVi", tblUser.idDonVi);
            return PartialView(tblUser);
        }

        // GET: tblUser/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblUser);
        }

        // POST: tblUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUser.Find(id);
            db.tblUser.Remove(tblUser);
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

        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object. 
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string. 
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            int i = 0;
            for (i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();

        }
        // Verify a hash against a string. 
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes. 
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
