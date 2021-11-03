using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7;

namespace TTQK7.Controllers
{
    public class TBBD_A1Controller : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();

        // GET: TBBD_A1
        public ActionResult Index()
        {
            var tBBD_A1 = db.TBBD_A1.Include(t => t.BaoCaoNgay);
            return View(tBBD_A1.ToList());
        }

        // GET: TBBD_A1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBBD_A1 tBBD_A1 = db.TBBD_A1.Find(id);
            if (tBBD_A1 == null)
            {
                return HttpNotFound();
            }
            return View(tBBD_A1);
        }

        // GET: TBBD_A1/Create
        [Authorize]
        public ActionResult Create()
        {
            //Kiểm tra xem người đang đăng nhập
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                int donViID = user.idDonVi.Value;
                //kiểm tra xem có báo cáo ngày chưa?
                BaoCaoNgay baoCao = db.BaoCaoNgay.Where(x => x.DonViID == donViID && x.NgayBaoCao.Value.Day == DateTime.Now.Day && x.NgayBaoCao.Value.Month == DateTime.Now.Month && x.NgayBaoCao.Value.Year == DateTime.Now.Year).FirstOrDefault();
                if (baoCao==null)
                {
                    //tạo cái mới
                    baoCao = new BaoCaoNgay();
                    baoCao.NgayBaoCao = DateTime.Now;
                    baoCao.NguoiBaoCao  = user.FirstName + " " + user.LastName;
                    baoCao.DonViID = donViID;
                    db.BaoCaoNgay.Add(baoCao);
                    db.SaveChanges();
                }
                ViewBag.BaoCaoID = baoCao.BaoCaoID;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }           
        }

        // POST: TBBD_A1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = @"A1_ID,CanhBTTM,CanhBTTM_Dut,
                                                CanhQK,CanhQK_Dut,Lanphatchuan,
                                                ThuTinHieuBo,
                                                ThuTinHieuQK,QKPhatTinHieu,GhiChu,BaoCaoID")] TBBD_A1 tBBD_A1)
        {
            if (ModelState.IsValid)
            {
                db.TBBD_A1.Add(tBBD_A1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaoCaoID = new SelectList(db.BaoCaoNgay, "BaoCaoID", "NguoiBaoCao", tBBD_A1.BaoCaoID);
            return View(tBBD_A1);
        }

        // GET: TBBD_A1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBBD_A1 tBBD_A1 = db.TBBD_A1.Find(id);
            if (tBBD_A1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaoCaoID = new SelectList(db.BaoCaoNgay, "BaoCaoID", "NguoiBaoCao", tBBD_A1.BaoCaoID);
            return View(tBBD_A1);
        }

        // POST: TBBD_A1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "A1_ID,CanhBTTM,CanhBTTM_Dut,CanhQK,CanhQK_Dut,Lanphatchuan,ThuTinHieuBo,ThuTinHieuQK,QKPhatTinHieu,GhiChu,BaoCaoID")] TBBD_A1 tBBD_A1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBBD_A1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaoCaoID = new SelectList(db.BaoCaoNgay, "BaoCaoID", "NguoiBaoCao", tBBD_A1.BaoCaoID);
            return View(tBBD_A1);
        }

        // GET: TBBD_A1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBBD_A1 tBBD_A1 = db.TBBD_A1.Find(id);
            if (tBBD_A1 == null)
            {
                return HttpNotFound();
            }
            return View(tBBD_A1);
        }

        // POST: TBBD_A1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBBD_A1 tBBD_A1 = db.TBBD_A1.Find(id);
            db.TBBD_A1.Remove(tBBD_A1);
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
