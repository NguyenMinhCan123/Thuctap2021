using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using TTQK7;
using TTQK7.Models;

namespace TTQK7.Controllers
{
    public class CBaoCaoController : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();

        // GET: TBBD_A1     
        Log lg = new Log();
        public ActionResult Index()
        {
            var tBBD_A1 = db.TBBD_A1.Include(t => t.BaoCaoNgay);
            return View(tBBD_A1.ToList());
        }
        // GET: TBBD_A1/Create
        [Authorize]
        public ActionResult Details_empty()
        {
            //Kiểm tra xem người đang đăng nhập
            if (Request.IsAuthenticated)
            {

                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                if (user.UserName == "admin")
                {
                    //Nếu là admin thì hiện báo cáo ngày
                    return RedirectToAction("Index", "BaoCaoNgays", new { ngaybaocao = DateTime.Now });
                }
               

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
                if (user.UserName =="admin")
                {
                    //Nếu là admin thì không cho tạo báo cáo
                    return RedirectToAction("Index", "BaoCaoNgays", new { ngaybaocao = DateTime.Now });
                }
                int idUser = user.idUser;
                int donViID = user.idDonVi.Value;
                //kiểm tra xem có báo cáo ngày chưa?
                BaoCaoNgay baoCao = db.BaoCaoNgay.Where(x => x.DonViID == donViID && x.NgayBaoCao.Value.Day == DateTime.Now.Day && x.NgayBaoCao.Value.Month == DateTime.Now.Month && x.NgayBaoCao.Value.Year == DateTime.Now.Year).FirstOrDefault();
                if (baoCao!=null && baoCao.IsGui!=true)
                {
                    //Thông báo đã có báo cáo nên chỉ chỉnh sửa
                    ViewBag.ThongBao = "Đã tạo báo cáo trong này, chỉ được chỉnh sửa";
                    //Nếu đã có báo cáo rồi thì chỉ sửa báo cáo
                    return RedirectToAction("Edit", "CBaoCao", new { id = baoCao.BaoCaoID });
                } else if (baoCao != null && baoCao.IsGui != false)
                {
                    return RedirectToAction("Details", "CBaoCao", new { id = baoCao.BaoCaoID });
                }    
                else if (baoCao==null)
                {
                    //Lấy thông tin Tban trưởng phó theo ngày để hiển thị lên
                    try
                    {
                        TrucBan tb = db.TrucBan.Where(x => x.NgayTruc.Value.Day == DateTime.Now.Day && x.NgayTruc.Value.Month == DateTime.Now.Month && x.NgayTruc.Value.Year == DateTime.Now.Year).First();
                        ViewBag.TBTruong = tb.TrucBanTruong;
                        ViewBag.TBPho = tb.TrucBanPho;
                    }
                    catch (Exception ex)
                    {
                        //Không có thông tin
                        ViewBag.TBTruong = "";
                        ViewBag.TBPho = "";
                    }

                    //tạo cái mới
                 
                }
                ViewBag.NgayBaoCao = DateTime.Now;
                 
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
        public ActionResult Create([Bind(Include = @"A1_CanhBTTM,A1_CanhBTTM_Dut,A1_CanhQK,A1_CanhQK_Dut,A1_Lanphatchuan,A1_ThuTinHieuBo,A1_ThuTinHieuQK,A1_QKPhatTinHieu,A1_GhiChu,
                                                     A2_SoDoiTuong,A2_TongSoPhien,A2_TongSoPhien_Dut,A2_SoPhienCT,A2_SoPhienCT_Dut,A2_DienS,A2_DienT,A2_GhiChu,
                                                ,A3_BTTM_TongSoPhien
                                                ,A3_BTTM_TongSoPhien_Dut
                                                ,A3_BTTM_GoiCanh
                                                ,A3_BTTM_GoiCanh_Dut
                                                ,A3_BTTM_TraLoiCanh
                                                ,A3_BTTM_TraLoiCanh_Dut
                                                ,A3_BTTM_DienS
                                                ,A3_BTTM_DienR
                                                ,A3_BTTM_GhiChu

                                                ,A3_NoiBo_SoDoiTuong
                                                ,A3_NoiBo_TongSoPhien
                                                ,A3_NoiBo_TongSoPhien_Dut
                                                ,A3_NoiBo_SoPhienCT
                                                ,A3_NoiBo_SoPhienCT_Dut
                                                ,A3_NoiBo_TraLoiCanh
                                                ,A3_NoiBo_TraLoiCanh_Dut
                                                ,A3_NoiBo_GoiCanh
                                                ,A3_NoiBo_GoiCanh_Dut
                                                ,A3_NoiBo_DienS
                                                ,A3_NoiBo_DienR
                                                ,A3_NoiBo_DienDong
                                                ,A3_NoiBo_GhiChu

                                                ,A3_HiepDong_TraLoiCanh
                                                ,A3_HiepDong_TraLoiCanh_Dut
                                                ,A3_HiepDong_DienS
                                                ,A3_HiepDong_DienR
                                                ,A3_HiepDong_GhiChu

                                                ,A4_SoDoiTuong
                                                ,A4_TongSoPhien
                                                ,A4_TongSoPhien_Dut
                                                ,A4_SoPhienCT
                                                ,A4_SoPhienCT_Dut
                                                ,A4_DienS
                                                ,A4_DienR
                                                ,A4_GhiChu

                                                ,A6_SoDoiTuong
                                                ,A6_GioLienLac
                                                ,A6_GioPhatLienLac
                                                ,A6_GioPhat
                                                ,A6_GioPhat_Dut
                                                ,A6_GhiChu

                                                ,B2_SoDoiTuong
                                                ,B2_LanChuyenTiep
                                                ,B2_SoLanDut
                                                ,B2_DayTieuHao
                                                ,B2_GioLienLac
                                                ,B2_PhutLienLac
                                                ,B2_GhiChu

                                                ,B3_MayDung
                                                ,B3_MaySua
                                                ,B3_MayDong
                                                ,B3_DatMayMoi
                                                ,B3_TongSoTrKe
                                                ,B3_TongSoTrKe_Dut
                                                ,B3_TongSoTrKeNB
                                                ,B3_TongSoTrKeNB_Dut
                                                ,B3_GhiChu

                                                ,B4_47_TongVanHanh
                                                ,B4_47_TongVanHanh_Dut
                                                ,B4_47_GhiChu

                                                ,B4_HoaToc_Ve
                                                ,B4_HoaToc_Di
                                                ,B4_HoaToc_Dong
                                                ,B4_HoaToc_GhiChu

                                                ,B4_CongVan_Ve
                                                ,B4_CongVan_Di
                                                ,B4_CongVan_Dong
                                                ,B4_CongVan_GhiChu

                                                ,B4_VanKien_Ve
                                                ,B4_VanKien_Di
                                                ,B4_VanKien_Dong
                                                ,B4_VanKien_GhiChu

                                                ,B4_ThuBao_Ve
                                                ,B4_ThuBao_Di
                                                ,B4_ThuBao_Dong
                                                ,B4_ThuBao_GhiChu

                                                ,B4_PhuongTien_XeDap
                                                ,B4_PhuongTien_MoTo
                                                ,B4_PhuongTien_OTo
                                                ,B4_PhuongTien_SoChuyen
                                                ,B4_PhuongTien_TrongLuong
                                                ,B4_PhuongTien_CuLy
                                                ,B4_PhuongTien_GhiChu

                                                ,GhiChuChung, 
                                                   NguoiBaoCao,  
                                                   TrucBanTruong, TrucBanPho
                                                ")] CBaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                //cập nhật ghi chú cho báo cáo 
                //BaoCaoNgay bc = db.BaoCaoNgay.Find(baoCao.BaoCaoID);
                //bc.NguoiBaoCao = baoCao.NguoiBaoCao;
                //bc.GhiChu = baoCao.GhiChuChung;
                //bc.TrucBanTruong = baoCao.TrucBanTruong;
                //bc.TrucBanPho = baoCao.TrucBanPho;
                //db.Entry(bc).State = EntityState.Modified;
                //db.SaveChanges();
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                int donViID = user.idDonVi.Value;
                BaoCaoNgay m_baoCao = new BaoCaoNgay();
                m_baoCao.NgayBaoCao = DateTime.Now;
                //Kiểm tra ngày báo cáo xem đã có trong hệ thống hay chưa thì mới cho tạo, nếu k thì thông báo
               
                    var baocaotontai = db.BaoCaoNgay.Where(x => x.NgayBaoCao.Value.Day == baoCao.NgayBaoCao.Day && x.NgayBaoCao.Value.Month == baoCao.NgayBaoCao.Month && x.NgayBaoCao.Value.Year == baoCao.NgayBaoCao.Year &&
               x.DonViID == donViID).FirstOrDefault();

                if (baocaotontai != null)
                {
                    //Chưa có báo cáo thì tiếp tục thực hiện
                    //Nếu có rồi thì quay lại và hiện thông báo
                    ViewBag.ThongBao = "Báo cáo đã tồn tại";
                    return View();
                }
                else { 
                    ViewBag.ThongBao = "";
                }
               
                m_baoCao.NguoiBaoCao = baoCao.NguoiBaoCao;
                m_baoCao.TrucBanTruong = baoCao.TrucBanTruong;
                m_baoCao.TrucBanPho = baoCao.TrucBanPho;
                m_baoCao.DonViID = donViID;
                db.BaoCaoNgay.Add(m_baoCao);
                db.SaveChanges();
                //Lấy ID báo cáo vừa tạo
                m_baoCao = db.BaoCaoNgay.Where(x=>x.DonViID==donViID).OrderByDescending(x=>x.BaoCaoID).FirstOrDefault();
                baoCao.BaoCaoID = m_baoCao.BaoCaoID;
                //Kiểm tra, nếu đã có người báo cáo cho đơn vị đó trong ngày rồi thì xóa đi để làm báo cáo lại

                TBBD_A1 tBBD_A1 = new TBBD_A1();
                tBBD_A1.BaoCaoID = baoCao.BaoCaoID;
                tBBD_A1.CanhBTTM = baoCao.A1_CanhBTTM;
                tBBD_A1.CanhBTTM_Dut = baoCao.A1_CanhBTTM_Dut;
                tBBD_A1.CanhQK = baoCao.A1_CanhQK;
                tBBD_A1.CanhQK_Dut = baoCao.A1_CanhQK_Dut;
                tBBD_A1.Lanphatchuan = baoCao.A1_Lanphatchuan;
                tBBD_A1.ThuTinHieuBo = baoCao.A1_ThuTinHieuBo;
                tBBD_A1.ThuTinHieuQK = baoCao.A1_ThuTinHieuQK;
                tBBD_A1.QKPhatTinHieu = baoCao.A1_QKPhatTinHieu;
                tBBD_A1.GhiChu = baoCao.A1_GhiChu;
               
                db.TBBD_A1.Add(tBBD_A1);
                db.SaveChanges();
                DonBien_A2 a2 = new DonBien_A2();
                a2.SoDoiTuong = baoCao.A2_SoDoiTuong;
                a2.TongSoPhien = baoCao.A2_TongSoPhien;
                a2.TongSoPhien_Dut = baoCao.A2_TongSoPhien_Dut;
                a2.SoPhienCT = baoCao.A2_SoPhienCT;
                a2.SoPhienCT_Dut = baoCao.A2_SoPhienCT_Dut;
                a2.DienS = baoCao.A2_DienS;
                a2.DienT = baoCao.A2_DienT;
                a2.BaoCaoID = baoCao.BaoCaoID;
                db.DonBien_A2.Add(a2);
                db.SaveChanges();

                SongNgan_BTTM_A3 a3_BTTM = new TTQK7.SongNgan_BTTM_A3();
                a3_BTTM.BaoCaoID = baoCao.BaoCaoID;
                a3_BTTM.TongSoPhien = baoCao.A3_BTTM_TongSoPhien;
                a3_BTTM.TongSoPhien_Dut = baoCao.A3_BTTM_TongSoPhien_Dut;
                a3_BTTM.GoiCanh = baoCao.A3_BTTM_GoiCanh;
                a3_BTTM.GoiCanh_Dut = baoCao.A3_BTTM_GoiCanh_Dut;
                a3_BTTM.TraLoiCanh = baoCao.A3_BTTM_TraLoiCanh;
                a3_BTTM.TraLoiCanh_Dut = baoCao.A3_BTTM_TraLoiCanh_Dut;
                a3_BTTM.DienS = baoCao.A3_BTTM_DienS;
                a3_BTTM.DienR = baoCao.A3_BTTM_DienR;
                a3_BTTM.GhiChu = baoCao.A3_BTTM_GhiChu;
                db.SongNgan_BTTM_A3.Add(a3_BTTM);
                db.SaveChanges();
                SongNgan_NoiBo_A3 a3_NoiBo = new SongNgan_NoiBo_A3();
                a3_NoiBo.BaoCaoID  = baoCao.BaoCaoID ;
                a3_NoiBo.SoDoiTuong = baoCao.A3_NoiBo_SoDoiTuong;
                a3_NoiBo.TongSoPhien = baoCao.A3_NoiBo_TongSoPhien;
                a3_NoiBo.TongSoPhien_Dut = baoCao.A3_NoiBo_TongSoPhien_Dut;
                a3_NoiBo.SoPhienCT = baoCao.A3_NoiBo_SoPhienCT;
                a3_NoiBo.SoPhienCT_Dut = baoCao.A3_NoiBo_SoPhienCT_Dut;
                a3_NoiBo.TraLoiCanh = baoCao.A3_NoiBo_TraLoiCanh;
                a3_NoiBo.TraLoiCanh_Dut = baoCao.A3_NoiBo_TraLoiCanh_Dut;
                a3_NoiBo.GoiCanh = baoCao.A3_NoiBo_GoiCanh;
                a3_NoiBo.GoiCanh_Dut = baoCao.A3_NoiBo_GoiCanh_Dut;
                a3_NoiBo.DienS = baoCao.A3_NoiBo_DienS;
                a3_NoiBo.DienR = baoCao.A3_NoiBo_DienR;
                a3_NoiBo.GhiChu  = baoCao.A3_NoiBo_GhiChu;
                db.SongNgan_NoiBo_A3.Add(a3_NoiBo);
                db.SaveChanges();
                SongNgan_HiepDong_A3 a3_HiepDong = new SongNgan_HiepDong_A3();
                a3_HiepDong.BaoCaoID = baoCao.BaoCaoID;
                a3_HiepDong.TraLoiCanh = baoCao.A3_HiepDong_TraLoiCanh;
                a3_HiepDong.TraLoiCanh_Dut = baoCao.A3_HiepDong_TraLoiCanh_Dut;
                a3_HiepDong.DienS = baoCao.A3_HiepDong_DienS;
                a3_HiepDong.DienR = baoCao.A3_HiepDong_DienR;
                a3_HiepDong.GhiChu = baoCao.A3_HiepDong_GhiChu;
                db.SongNgan_HiepDong_A3.Add(a3_HiepDong);
                db.SaveChanges();
                SCN_A4 a4 = new TTQK7.SCN_A4();
                a4.BaoCaoID = baoCao.BaoCaoID;
                a4.SoDoiTuong = baoCao.A4_SoDoiTuong;
                a4.TongSoPhien = baoCao.A4_TongSoPhien;
                a4.TongSoPhien_Dut = baoCao.A4_TongSoPhien_Dut;
                a4.SoPhienCT = baoCao.A4_SoPhienCT;
                a4.SoPhienCT_Dut = baoCao.A4_SoPhienCT_Dut;
                a4.DienS = baoCao.A4_DienS;
                a4.DienR = baoCao.A4_DienR;
                a4.GhiChu = baoCao.A4_GhiChu;
                db.SCN_A4.Add(a4);
                db.SaveChanges();
                ViBa_A6 a6 = new ViBa_A6();
                a6.BaoCaoID = baoCao.BaoCaoID;
                a6.SoDoiTuong = baoCao.A6_SoDoiTuong;
                a6.GioLienLac = baoCao.A6_GioLienLac;
                a6.GioPhatLienLac = baoCao.A6_GioPhatLienLac;
                a6.GioPhat = baoCao.A6_GioPhat;
                a6.GioPhat_Dut = baoCao.A6_GioPhat_Dut;
                a6.GhiChu = baoCao.A6_GhiChu;
                db.ViBa_A6.Add(a6);
                db.SaveChanges();
                HDT_B2 b2 = new HDT_B2();
                b2.BaoCaoID = baoCao.BaoCaoID;
                b2.SoDoiTuong = baoCao.B2_SoDoiTuong;
                b2.LanChuyenTiep = baoCao.B2_LanChuyenTiep;
                b2.SoLanDut = baoCao.B2_SoLanDut;
                b2.DayTieuHao = baoCao.B2_DayTieuHao;
                b2.GioLienLac = baoCao.B2_GioLienLac;
                b2.PhutLienLac = baoCao.B2_PhutLienLac;
                b2.GhiChu = baoCao.B2_GhiChu;
                db.HDT_B2.Add(b2);
                db.SaveChanges();
                HTD_B3 b3 = new HTD_B3();
                b3.BaoCaoID = baoCao.BaoCaoID;
                b3.MayDung = baoCao.B3_MayDung;
                b3.MaySua = baoCao.B3_MaySua;
                b3.MayDong = baoCao.B3_MayDong;
                b3.DatMayMoi = baoCao.B3_DatMayMoi;
                b3.TongSoTrKe = baoCao.B3_TongSoTrKe;
                b3.TongSoTrKe_Dut = baoCao.B3_TongSoTrKe_Dut;
                b3.TongSoTrKeNB = baoCao.B3_TongSoTrKeNB;
                b3.TongSoTrKeNB_Dut = baoCao.B3_TongSoTrKeNB_Dut;
                b3.GhiChu = baoCao.B3_GhiChu;
                db.HTD_B3.Add(b3);
                db.SaveChanges();
                QuanBuu_47_B4 b4 = new QuanBuu_47_B4();
                b4.BaoCaoID = baoCao.BaoCaoID;
                b4.TongVanHanh = baoCao.B4_47_TongVanHanh;
                b4.TongVanHanh_Dut = baoCao.B4_47_TongVanHanh_Dut;
                b4.GhiChu = baoCao.B4_47_GhiChu;
                db.QuanBuu_47_B4.Add(b4);
                db.SaveChanges();
                QuanBuu_HoaToc_B4 b4_HoaToc = new QuanBuu_HoaToc_B4();
                b4_HoaToc.BaoCaoID = baoCao.BaoCaoID;
                b4_HoaToc.Ve = baoCao.B4_HoaToc_Ve;
                b4_HoaToc.Di = baoCao.B4_HoaToc_Di;
                b4_HoaToc.Dong = baoCao.B4_HoaToc_Dong;
                b4_HoaToc.GhiChu = baoCao.B4_HoaToc_GhiChu;
                db.QuanBuu_HoaToc_B4.Add(b4_HoaToc);
                db.SaveChanges();
                QuanBuu_CongVan_B4 b4_CongVan = new QuanBuu_CongVan_B4();
                b4_CongVan.BaoCaoID = baoCao.BaoCaoID;
                b4_CongVan.Ve = baoCao.B4_CongVan_Ve;
                b4_CongVan.Di = baoCao.B4_CongVan_Di;
                b4_CongVan.Dong = baoCao.B4_CongVan_Dong;
                b4_CongVan.GhiChu = baoCao.B4_CongVan_GhiChu;
                db.QuanBuu_CongVan_B4.Add(b4_CongVan);
                db.SaveChanges();
                QuanBuu_VanKien_B4 b4_VanKien = new QuanBuu_VanKien_B4();
                b4_VanKien.BaoCaoID = baoCao.BaoCaoID;
                b4_VanKien.Ve = baoCao.B4_VanKien_Ve;
                b4_VanKien.Di = baoCao.B4_VanKien_Di;
                b4_VanKien.Dong = baoCao.B4_VanKien_Dong;
                b4_VanKien.GhiChu = baoCao.B4_VanKien_GhiChu;
                db.QuanBuu_VanKien_B4.Add(b4_VanKien);
                db.SaveChanges();
                QuanBuu_ThuBao_B4 b4_ThuBao = new QuanBuu_ThuBao_B4();
                b4_ThuBao.BaoCaoID = baoCao.BaoCaoID;
                b4_ThuBao.Ve = baoCao.B4_ThuBao_Ve;
                b4_ThuBao.Di = baoCao.B4_ThuBao_Di;
                b4_ThuBao.Dong = baoCao.B4_ThuBao_Dong;
                b4_ThuBao.GhiChu = baoCao.B4_ThuBao_GhiChu;
                db.QuanBuu_ThuBao_B4.Add(b4_ThuBao);
                db.SaveChanges();
                QuanBuu_PhuongTien_B4 b4_PhuongTien = new QuanBuu_PhuongTien_B4();
                b4_PhuongTien.BaoCaoID = baoCao.BaoCaoID;
                b4_PhuongTien.XeDap = baoCao.B4_PhuongTien_XeDap;
                b4_PhuongTien.MoTo = baoCao.B4_PhuongTien_MoTo;
                b4_PhuongTien.OTo = baoCao.B4_PhuongTien_OTo;
                b4_PhuongTien.SoChuyen = baoCao.B4_PhuongTien_SoChuyen;
                b4_PhuongTien.TrongLuong = baoCao.B4_PhuongTien_TrongLuong;
                b4_PhuongTien.CuLy = baoCao.B4_PhuongTien_CuLy;
                b4_PhuongTien.GhiChu = baoCao.B4_PhuongTien_GhiChu;
                db.QuanBuu_PhuongTien_B4.Add(b4_PhuongTien);
                db.SaveChanges();
                //RedirectToAction("Action", new { id = 99 });
                DateTime ngaybaocao = DateTime.Now;
                lg.LogError("Tạo báo cáo theo ngày: " + ngaybaocao.ToString() + ", baocaoid:" + baoCao.BaoCaoID +
                    " donviid: " + donViID.ToString());
                return RedirectToAction("Details_empty", "CBaoCao", new { id = baoCao.BaoCaoID});
            }

            return View();
        }
        // GET: TBBD_A1/Create
        [Authorize]
        public ActionResult CreateNgay()
        {
            //Kiểm tra xem người đang đăng nhập
            if (Request.IsAuthenticated)
            {

                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                if (user.UserName == "admin")
                {
                    //Nếu là admin thì không cho tạo báo cáo
                    return RedirectToAction("Index", "BaoCaoNgays", new { ngaybaocao = DateTime.Now });
                }
                int idUser = user.idUser;
                int donViID = user.idDonVi.Value;
                //kiểm tra xem có báo cáo ngày chưa?
                //BaoCaoNgay baoCao = db.BaoCaoNgay.Where(x => x.DonViID == donViID && x.NgayBaoCao.Value.Day == DateTime.Now.Day && x.NgayBaoCao.Value.Month == DateTime.Now.Month && ///x.NgayBaoCao.Value.Year == DateTime.Now.Year).FirstOrDefault();
                //if (baoCao != null && baoCao.IsGui != true)
                //{
                //    //Thông báo đã có báo cáo nên chỉ chỉnh sửa
                //    ViewBag.ThongBao = "Đã tạo báo cáo trong này, chỉ được chỉnh sửa";
                //    //Nếu đã có báo cáo rồi thì chỉ sửa báo cáo
                //   // return RedirectToAction("Edit", "CBaoCao", new { id = baoCao.BaoCaoID });
                //}
                //else if (baoCao != null && baoCao.IsGui != false)
                //{
                //    return RedirectToAction("Details", "CBaoCao", new { id = baoCao.BaoCaoID });
                //}
                //else if (baoCao == null)
                //{
                    //Lấy thông tin Tban trưởng phó theo ngày để hiển thị lên
                    try
                    {
                        TrucBan tb = db.TrucBan.Where(x => x.NgayTruc.Value.Day == DateTime.Now.Day && x.NgayTruc.Value.Month == DateTime.Now.Month && x.NgayTruc.Value.Year == DateTime.Now.Year).First();
                        ViewBag.TBTruong = tb.TrucBanTruong;
                        ViewBag.TBPho = tb.TrucBanPho;
                    }
                    catch (Exception ex)
                    {
                        //Không có thông tin
                        ViewBag.TBTruong = "";
                        ViewBag.TBPho = "";
                    }

                    //tạo cái mới

                //}
                ViewBag.NgayBaoCao = DateTime.Now;

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
        public ActionResult CreateNgay([Bind(Include = @"A1_CanhBTTM,A1_CanhBTTM_Dut,A1_CanhQK,A1_CanhQK_Dut,A1_Lanphatchuan,A1_ThuTinHieuBo,A1_ThuTinHieuQK,A1_QKPhatTinHieu,A1_GhiChu,
                                                     A2_SoDoiTuong,A2_TongSoPhien,A2_TongSoPhien_Dut,A2_SoPhienCT,A2_SoPhienCT_Dut,A2_DienS,A2_DienT,A2_GhiChu,
                                                ,A3_BTTM_TongSoPhien
                                                ,A3_BTTM_TongSoPhien_Dut
                                                ,A3_BTTM_GoiCanh
                                                ,A3_BTTM_GoiCanh_Dut
                                                ,A3_BTTM_TraLoiCanh
                                                ,A3_BTTM_TraLoiCanh_Dut
                                                ,A3_BTTM_DienS
                                                ,A3_BTTM_DienR
                                                ,A3_BTTM_GhiChu

                                                ,A3_NoiBo_SoDoiTuong
                                                ,A3_NoiBo_TongSoPhien
                                                ,A3_NoiBo_TongSoPhien_Dut
                                                ,A3_NoiBo_SoPhienCT
                                                ,A3_NoiBo_SoPhienCT_Dut
                                                ,A3_NoiBo_TraLoiCanh
                                                ,A3_NoiBo_TraLoiCanh_Dut
                                                ,A3_NoiBo_GoiCanh
                                                ,A3_NoiBo_GoiCanh_Dut
                                                ,A3_NoiBo_DienS
                                                ,A3_NoiBo_DienR
                                                ,A3_NoiBo_DienDong
                                                ,A3_NoiBo_GhiChu

                                                ,A3_HiepDong_TraLoiCanh
                                                ,A3_HiepDong_TraLoiCanh_Dut
                                                ,A3_HiepDong_DienS
                                                ,A3_HiepDong_DienR
                                                ,A3_HiepDong_GhiChu

                                                ,A4_SoDoiTuong
                                                ,A4_TongSoPhien
                                                ,A4_TongSoPhien_Dut
                                                ,A4_SoPhienCT
                                                ,A4_SoPhienCT_Dut
                                                ,A4_DienS
                                                ,A4_DienR
                                                ,A4_GhiChu

                                                ,A6_SoDoiTuong
                                                ,A6_GioLienLac
                                                ,A6_GioPhatLienLac
                                                ,A6_GioPhat
                                                ,A6_GioPhat_Dut
                                                ,A6_GhiChu

                                                ,B2_SoDoiTuong
                                                ,B2_LanChuyenTiep
                                                ,B2_SoLanDut
                                                ,B2_DayTieuHao
                                                ,B2_GioLienLac
                                                ,B2_PhutLienLac
                                                ,B2_GhiChu

                                                ,B3_MayDung
                                                ,B3_MaySua
                                                ,B3_MayDong
                                                ,B3_DatMayMoi
                                                ,B3_TongSoTrKe
                                                ,B3_TongSoTrKe_Dut
                                                ,B3_TongSoTrKeNB
                                                ,B3_TongSoTrKeNB_Dut
                                                ,B3_GhiChu

                                                ,B4_47_TongVanHanh
                                                ,B4_47_TongVanHanh_Dut
                                                ,B4_47_GhiChu

                                                ,B4_HoaToc_Ve
                                                ,B4_HoaToc_Di
                                                ,B4_HoaToc_Dong
                                                ,B4_HoaToc_GhiChu

                                                ,B4_CongVan_Ve
                                                ,B4_CongVan_Di
                                                ,B4_CongVan_Dong
                                                ,B4_CongVan_GhiChu

                                                ,B4_VanKien_Ve
                                                ,B4_VanKien_Di
                                                ,B4_VanKien_Dong
                                                ,B4_VanKien_GhiChu

                                                ,B4_ThuBao_Ve
                                                ,B4_ThuBao_Di
                                                ,B4_ThuBao_Dong
                                                ,B4_ThuBao_GhiChu

                                                ,B4_PhuongTien_XeDap
                                                ,B4_PhuongTien_MoTo
                                                ,B4_PhuongTien_OTo
                                                ,B4_PhuongTien_SoChuyen
                                                ,B4_PhuongTien_TrongLuong
                                                ,B4_PhuongTien_CuLy
                                                ,B4_PhuongTien_GhiChu

                                                ,GhiChuChung, 
                                                   NguoiBaoCao,  
                                                   TrucBanTruong, TrucBanPho, NgayBaoCao
                                                ")] CBaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                //cập nhật ghi chú cho báo cáo 
                //BaoCaoNgay bc = db.BaoCaoNgay.Find(baoCao.BaoCaoID);
                //bc.NguoiBaoCao = baoCao.NguoiBaoCao;
                //bc.GhiChu = baoCao.GhiChuChung;
                //bc.TrucBanTruong = baoCao.TrucBanTruong;
                //bc.TrucBanPho = baoCao.TrucBanPho;
                //db.Entry(bc).State = EntityState.Modified;
                //db.SaveChanges();
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                int idUser = user.idUser;
                int donViID = user.idDonVi.Value;
                BaoCaoNgay m_baoCao = new BaoCaoNgay();
                m_baoCao.NgayBaoCao = baoCao.NgayBaoCao;
                //Kiểm tra ngày báo cáo xem đã có trong hệ thống hay chưa thì mới cho tạo, nếu k thì thông báo

                var baocaotontai = db.BaoCaoNgay.Where(x => x.NgayBaoCao.Value.Day == baoCao.NgayBaoCao.Day && x.NgayBaoCao.Value.Month == baoCao.NgayBaoCao.Month && x.NgayBaoCao.Value.Year == baoCao.NgayBaoCao.Year &&
           x.DonViID == donViID).FirstOrDefault();

                if (baocaotontai != null)
                {
                    //Chưa có báo cáo thì tiếp tục thực hiện
                    //Nếu có rồi thì quay lại và hiện thông báo
                    ViewBag.ThongBao = "Báo cáo đã tồn tại";
                    return View();
                }
                else
                {
                    ViewBag.ThongBao = "";
                }

                m_baoCao.NguoiBaoCao = baoCao.NguoiBaoCao;
                m_baoCao.TrucBanTruong = baoCao.TrucBanTruong;
                m_baoCao.TrucBanPho = baoCao.TrucBanPho;
                m_baoCao.DonViID = donViID;
                db.BaoCaoNgay.Add(m_baoCao);
                db.SaveChanges();
                //Lấy ID báo cáo vừa tạo
                m_baoCao = db.BaoCaoNgay.Where(x => x.DonViID == donViID).OrderByDescending(x => x.BaoCaoID).FirstOrDefault();
                baoCao.BaoCaoID = m_baoCao.BaoCaoID;
                //Kiểm tra, nếu đã có người báo cáo cho đơn vị đó trong ngày rồi thì xóa đi để làm báo cáo lại

                TBBD_A1 tBBD_A1 = new TBBD_A1();
                tBBD_A1.BaoCaoID = baoCao.BaoCaoID;
                tBBD_A1.CanhBTTM = baoCao.A1_CanhBTTM;
                tBBD_A1.CanhBTTM_Dut = baoCao.A1_CanhBTTM_Dut;
                tBBD_A1.CanhQK = baoCao.A1_CanhQK;
                tBBD_A1.CanhQK_Dut = baoCao.A1_CanhQK_Dut;
                tBBD_A1.Lanphatchuan = baoCao.A1_Lanphatchuan;
                tBBD_A1.ThuTinHieuBo = baoCao.A1_ThuTinHieuBo;
                tBBD_A1.ThuTinHieuQK = baoCao.A1_ThuTinHieuQK;
                tBBD_A1.QKPhatTinHieu = baoCao.A1_QKPhatTinHieu;
                tBBD_A1.GhiChu = baoCao.A1_GhiChu;

                db.TBBD_A1.Add(tBBD_A1);
                db.SaveChanges();
                DonBien_A2 a2 = new DonBien_A2();
                a2.SoDoiTuong = baoCao.A2_SoDoiTuong;
                a2.TongSoPhien = baoCao.A2_TongSoPhien;
                a2.TongSoPhien_Dut = baoCao.A2_TongSoPhien_Dut;
                a2.SoPhienCT = baoCao.A2_SoPhienCT;
                a2.SoPhienCT_Dut = baoCao.A2_SoPhienCT_Dut;
                a2.DienS = baoCao.A2_DienS;
                a2.DienT = baoCao.A2_DienT;
                a2.BaoCaoID = baoCao.BaoCaoID;
                db.DonBien_A2.Add(a2);
                db.SaveChanges();

                SongNgan_BTTM_A3 a3_BTTM = new TTQK7.SongNgan_BTTM_A3();
                a3_BTTM.BaoCaoID = baoCao.BaoCaoID;
                a3_BTTM.TongSoPhien = baoCao.A3_BTTM_TongSoPhien;
                a3_BTTM.TongSoPhien_Dut = baoCao.A3_BTTM_TongSoPhien_Dut;
                a3_BTTM.GoiCanh = baoCao.A3_BTTM_GoiCanh;
                a3_BTTM.GoiCanh_Dut = baoCao.A3_BTTM_GoiCanh_Dut;
                a3_BTTM.TraLoiCanh = baoCao.A3_BTTM_TraLoiCanh;
                a3_BTTM.TraLoiCanh_Dut = baoCao.A3_BTTM_TraLoiCanh_Dut;
                a3_BTTM.DienS = baoCao.A3_BTTM_DienS;
                a3_BTTM.DienR = baoCao.A3_BTTM_DienR;
                a3_BTTM.GhiChu = baoCao.A3_BTTM_GhiChu;
                db.SongNgan_BTTM_A3.Add(a3_BTTM);
                db.SaveChanges();
                SongNgan_NoiBo_A3 a3_NoiBo = new SongNgan_NoiBo_A3();
                a3_NoiBo.BaoCaoID = baoCao.BaoCaoID;
                a3_NoiBo.SoDoiTuong = baoCao.A3_NoiBo_SoDoiTuong;
                a3_NoiBo.TongSoPhien = baoCao.A3_NoiBo_TongSoPhien;
                a3_NoiBo.TongSoPhien_Dut = baoCao.A3_NoiBo_TongSoPhien_Dut;
                a3_NoiBo.SoPhienCT = baoCao.A3_NoiBo_SoPhienCT;
                a3_NoiBo.SoPhienCT_Dut = baoCao.A3_NoiBo_SoPhienCT_Dut;
                a3_NoiBo.TraLoiCanh = baoCao.A3_NoiBo_TraLoiCanh;
                a3_NoiBo.TraLoiCanh_Dut = baoCao.A3_NoiBo_TraLoiCanh_Dut;
                a3_NoiBo.GoiCanh = baoCao.A3_NoiBo_GoiCanh;
                a3_NoiBo.GoiCanh_Dut = baoCao.A3_NoiBo_GoiCanh_Dut;
                a3_NoiBo.DienS = baoCao.A3_NoiBo_DienS;
                a3_NoiBo.DienR = baoCao.A3_NoiBo_DienR;
                a3_NoiBo.GhiChu = baoCao.A3_NoiBo_GhiChu;
                db.SongNgan_NoiBo_A3.Add(a3_NoiBo);
                db.SaveChanges();
                SongNgan_HiepDong_A3 a3_HiepDong = new SongNgan_HiepDong_A3();
                a3_HiepDong.BaoCaoID = baoCao.BaoCaoID;
                a3_HiepDong.TraLoiCanh = baoCao.A3_HiepDong_TraLoiCanh;
                a3_HiepDong.TraLoiCanh_Dut = baoCao.A3_HiepDong_TraLoiCanh_Dut;
                a3_HiepDong.DienS = baoCao.A3_HiepDong_DienS;
                a3_HiepDong.DienR = baoCao.A3_HiepDong_DienR;
                a3_HiepDong.GhiChu = baoCao.A3_HiepDong_GhiChu;
                db.SongNgan_HiepDong_A3.Add(a3_HiepDong);
                db.SaveChanges();
                SCN_A4 a4 = new TTQK7.SCN_A4();
                a4.BaoCaoID = baoCao.BaoCaoID;
                a4.SoDoiTuong = baoCao.A4_SoDoiTuong;
                a4.TongSoPhien = baoCao.A4_TongSoPhien;
                a4.TongSoPhien_Dut = baoCao.A4_TongSoPhien_Dut;
                a4.SoPhienCT = baoCao.A4_SoPhienCT;
                a4.SoPhienCT_Dut = baoCao.A4_SoPhienCT_Dut;
                a4.DienS = baoCao.A4_DienS;
                a4.DienR = baoCao.A4_DienR;
                a4.GhiChu = baoCao.A4_GhiChu;
                db.SCN_A4.Add(a4);
                db.SaveChanges();
                ViBa_A6 a6 = new ViBa_A6();
                a6.BaoCaoID = baoCao.BaoCaoID;
                a6.SoDoiTuong = baoCao.A6_SoDoiTuong;
                a6.GioLienLac = baoCao.A6_GioLienLac;
                a6.GioPhatLienLac = baoCao.A6_GioPhatLienLac;
                a6.GioPhat = baoCao.A6_GioPhat;
                a6.GioPhat_Dut = baoCao.A6_GioPhat_Dut;
                a6.GhiChu = baoCao.A6_GhiChu;
                db.ViBa_A6.Add(a6);
                db.SaveChanges();
                HDT_B2 b2 = new HDT_B2();
                b2.BaoCaoID = baoCao.BaoCaoID;
                b2.SoDoiTuong = baoCao.B2_SoDoiTuong;
                b2.LanChuyenTiep = baoCao.B2_LanChuyenTiep;
                b2.SoLanDut = baoCao.B2_SoLanDut;
                b2.DayTieuHao = baoCao.B2_DayTieuHao;
                b2.GioLienLac = baoCao.B2_GioLienLac;
                b2.PhutLienLac = baoCao.B2_PhutLienLac;
                b2.GhiChu = baoCao.B2_GhiChu;
                db.HDT_B2.Add(b2);
                db.SaveChanges();
                HTD_B3 b3 = new HTD_B3();
                b3.BaoCaoID = baoCao.BaoCaoID;
                b3.MayDung = baoCao.B3_MayDung;
                b3.MaySua = baoCao.B3_MaySua;
                b3.MayDong = baoCao.B3_MayDong;
                b3.DatMayMoi = baoCao.B3_DatMayMoi;
                b3.TongSoTrKe = baoCao.B3_TongSoTrKe;
                b3.TongSoTrKe_Dut = baoCao.B3_TongSoTrKe_Dut;
                b3.TongSoTrKeNB = baoCao.B3_TongSoTrKeNB;
                b3.TongSoTrKeNB_Dut = baoCao.B3_TongSoTrKeNB_Dut;
                b3.GhiChu = baoCao.B3_GhiChu;
                db.HTD_B3.Add(b3);
                db.SaveChanges();
                QuanBuu_47_B4 b4 = new QuanBuu_47_B4();
                b4.BaoCaoID = baoCao.BaoCaoID;
                b4.TongVanHanh = baoCao.B4_47_TongVanHanh;
                b4.TongVanHanh_Dut = baoCao.B4_47_TongVanHanh_Dut;
                b4.GhiChu = baoCao.B4_47_GhiChu;
                db.QuanBuu_47_B4.Add(b4);
                db.SaveChanges();
                QuanBuu_HoaToc_B4 b4_HoaToc = new QuanBuu_HoaToc_B4();
                b4_HoaToc.BaoCaoID = baoCao.BaoCaoID;
                b4_HoaToc.Ve = baoCao.B4_HoaToc_Ve;
                b4_HoaToc.Di = baoCao.B4_HoaToc_Di;
                b4_HoaToc.Dong = baoCao.B4_HoaToc_Dong;
                b4_HoaToc.GhiChu = baoCao.B4_HoaToc_GhiChu;
                db.QuanBuu_HoaToc_B4.Add(b4_HoaToc);
                db.SaveChanges();
                QuanBuu_CongVan_B4 b4_CongVan = new QuanBuu_CongVan_B4();
                b4_CongVan.BaoCaoID = baoCao.BaoCaoID;
                b4_CongVan.Ve = baoCao.B4_CongVan_Ve;
                b4_CongVan.Di = baoCao.B4_CongVan_Di;
                b4_CongVan.Dong = baoCao.B4_CongVan_Dong;
                b4_CongVan.GhiChu = baoCao.B4_CongVan_GhiChu;
                db.QuanBuu_CongVan_B4.Add(b4_CongVan);
                db.SaveChanges();
                QuanBuu_VanKien_B4 b4_VanKien = new QuanBuu_VanKien_B4();
                b4_VanKien.BaoCaoID = baoCao.BaoCaoID;
                b4_VanKien.Ve = baoCao.B4_VanKien_Ve;
                b4_VanKien.Di = baoCao.B4_VanKien_Di;
                b4_VanKien.Dong = baoCao.B4_VanKien_Dong;
                b4_VanKien.GhiChu = baoCao.B4_VanKien_GhiChu;
                db.QuanBuu_VanKien_B4.Add(b4_VanKien);
                db.SaveChanges();
                QuanBuu_ThuBao_B4 b4_ThuBao = new QuanBuu_ThuBao_B4();
                b4_ThuBao.BaoCaoID = baoCao.BaoCaoID;
                b4_ThuBao.Ve = baoCao.B4_ThuBao_Ve;
                b4_ThuBao.Di = baoCao.B4_ThuBao_Di;
                b4_ThuBao.Dong = baoCao.B4_ThuBao_Dong;
                b4_ThuBao.GhiChu = baoCao.B4_ThuBao_GhiChu;
                db.QuanBuu_ThuBao_B4.Add(b4_ThuBao);
                db.SaveChanges();
                QuanBuu_PhuongTien_B4 b4_PhuongTien = new QuanBuu_PhuongTien_B4();
                b4_PhuongTien.BaoCaoID = baoCao.BaoCaoID;
                b4_PhuongTien.XeDap = baoCao.B4_PhuongTien_XeDap;
                b4_PhuongTien.MoTo = baoCao.B4_PhuongTien_MoTo;
                b4_PhuongTien.OTo = baoCao.B4_PhuongTien_OTo;
                b4_PhuongTien.SoChuyen = baoCao.B4_PhuongTien_SoChuyen;
                b4_PhuongTien.TrongLuong = baoCao.B4_PhuongTien_TrongLuong;
                b4_PhuongTien.CuLy = baoCao.B4_PhuongTien_CuLy;
                b4_PhuongTien.GhiChu = baoCao.B4_PhuongTien_GhiChu;
                db.QuanBuu_PhuongTien_B4.Add(b4_PhuongTien);
                db.SaveChanges();
                //RedirectToAction("Action", new { id = 99 });
                DateTime ngaybaocao = DateTime.Now;
                lg.LogError("Tạo báo cáo theo ngày: " + ngaybaocao.ToString() + ", baocaoid:" + baoCao.BaoCaoID +
                    " donviid: " + donViID.ToString());
                return RedirectToAction("Details_empty", "CBaoCao", new { id = baoCao.BaoCaoID });
            }

            return View();
        }

        // GET: TBBD_A1/Edit/5
        public ActionResult Edit(int? id)
        {
            //Kiểm tra xem người đang đăng nhập
            if (Request.IsAuthenticated)
            {
               
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            //kiểm tra xem có báo cáo ngày chưa?
            string username = User.Identity.GetUserName();
            tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
            int idUser = user.idUser;
            int donViID = user.idDonVi.Value;
            BaoCaoNgay baoCao = db.BaoCaoNgay.Where(x=>x.BaoCaoID == id).FirstOrDefault();
            ViewBag.DonVi = db.tblDonVi.Where(x => x.DonViID == baoCao.DonViID).Select(x => x.TenDonVi).First();
            ViewBag.NgayBaoCao = "Ngày " + baoCao.NgayBaoCao.Value.Day.ToString() + " tháng " + baoCao.NgayBaoCao.Value.Month.ToString() + " năm " +
                baoCao.NgayBaoCao.Value.Year.ToString();
            //Lấy thông tin Tban trưởng phó theo ngày để hiển thị lên
            try
            {
                TrucBan tb = db.TrucBan.Where(x => x.NgayTruc == baoCao.NgayBaoCao).First();
                ViewBag.TBTruong = tb.TrucBanTruong;
                ViewBag.TBPho = tb.TrucBanPho;
            }
            catch (Exception)
            {
                //Không có thông tin
                ViewBag.TBTruong = "Nhập tên";
                ViewBag.TBPho = "Nhập tên";
            }
           
            //Lấy thông tin của các Bảng để tạo CBaoCao
            TBBD_A1 a1 = db.TBBD_A1.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            DonBien_A2 a2= db.DonBien_A2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            CBaoCao cur_baocao = new CBaoCao();
            cur_baocao.TrucBanTruong = baoCao.TrucBanTruong;
            cur_baocao.TrucBanPho = baoCao.TrucBanPho;
            cur_baocao.GhiChuChung = baoCao.GhiChu;
            cur_baocao.NguoiBaoCao = baoCao.NguoiBaoCao;
            
            if (a1 !=null)
            {
                cur_baocao.A1_CanhBTTM = a1.CanhBTTM;
                cur_baocao.A1_CanhBTTM_Dut = a1.CanhBTTM_Dut;
                cur_baocao.A1_CanhQK = a1.CanhQK;
                cur_baocao.A1_CanhQK_Dut = a1.CanhQK_Dut;
                cur_baocao.A1_Lanphatchuan = a1.Lanphatchuan;
                cur_baocao.A1_ThuTinHieuBo = a1.ThuTinHieuBo;
                cur_baocao.A1_ThuTinHieuQK = a1.ThuTinHieuQK;
                cur_baocao.A1_QKPhatTinHieu = a1.QKPhatTinHieu;
                cur_baocao.A1_GhiChu = a1.GhiChu;
            }
            if (a2 !=null)
            {
                cur_baocao.A2_SoDoiTuong = a2.SoDoiTuong;
                cur_baocao.A2_TongSoPhien = a2.TongSoPhien;
                cur_baocao.A2_TongSoPhien_Dut = a2.TongSoPhien_Dut;
                cur_baocao.A2_SoPhienCT = a2.SoPhienCT;
                cur_baocao.A2_SoPhienCT_Dut = a2.SoPhienCT_Dut;
                cur_baocao.A2_DienS = a2.DienS;
                cur_baocao.A2_DienT = a2.DienT;
                cur_baocao.A2_GhiChu = a2.GhiChu;
            }
           
            SongNgan_BTTM_A3 a3_1 = db.SongNgan_BTTM_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_1!=null)
            {
                cur_baocao.A3_BTTM_TongSoPhien = a3_1.TongSoPhien;
                cur_baocao.A3_BTTM_TongSoPhien_Dut = a3_1.TongSoPhien_Dut;
                cur_baocao.A3_BTTM_GoiCanh = a3_1.GoiCanh;
                cur_baocao.A3_BTTM_GoiCanh_Dut = a3_1.GoiCanh_Dut;
                cur_baocao.A3_BTTM_TraLoiCanh = a3_1.TraLoiCanh;
                cur_baocao.A3_BTTM_TraLoiCanh_Dut = a3_1.TraLoiCanh_Dut;
                cur_baocao.A3_BTTM_DienS = a3_1.DienS;
                cur_baocao.A3_BTTM_DienR = a3_1.DienR;
                cur_baocao.A3_BTTM_GhiChu = a3_1.GhiChu;
            }
            
            SongNgan_NoiBo_A3 a3_2 = db.SongNgan_NoiBo_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_2 !=null)
            {
                cur_baocao.A3_NoiBo_SoDoiTuong = a3_2.SoDoiTuong;
                cur_baocao.A3_NoiBo_TongSoPhien = a3_2.TongSoPhien;
                cur_baocao.A3_NoiBo_TongSoPhien_Dut = a3_2.TongSoPhien_Dut;
                cur_baocao.A3_NoiBo_SoPhienCT = a3_2.SoPhienCT;
                cur_baocao.A3_NoiBo_SoPhienCT_Dut = a3_2.SoPhienCT_Dut;
                cur_baocao.A3_NoiBo_TraLoiCanh = a3_2.TraLoiCanh;
                cur_baocao.A3_NoiBo_TraLoiCanh_Dut = a3_2.TraLoiCanh_Dut;
                cur_baocao.A3_NoiBo_GoiCanh = a3_2.GoiCanh;
                cur_baocao.A3_NoiBo_GoiCanh_Dut = a3_2.GoiCanh_Dut;
                cur_baocao.A3_NoiBo_DienS = a3_2.DienS;
                cur_baocao.A3_NoiBo_DienR = a3_2.DienR;
                cur_baocao.A3_NoiBo_DienDong = a3_2.DienDong;
                cur_baocao.A3_NoiBo_GhiChu = a3_2.GhiChu;
            }
         
            SongNgan_HiepDong_A3 a3_3 = db.SongNgan_HiepDong_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_3!=null)
            {
                cur_baocao.A3_HiepDong_TraLoiCanh = a3_3.TraLoiCanh;
                cur_baocao.A3_HiepDong_TraLoiCanh_Dut = a3_3.TraLoiCanh_Dut;
                cur_baocao.A3_HiepDong_DienS = a3_3.DienS;
                cur_baocao.A3_HiepDong_DienR = a3_3.DienR;
                cur_baocao.A3_HiepDong_GhiChu = a3_3.GhiChu;
            }
           
            SCN_A4 a4 = db.SCN_A4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a4!=null)
            {
                cur_baocao.A4_SoDoiTuong = a4.SoDoiTuong;
                cur_baocao.A4_TongSoPhien = a4.TongSoPhien_Dut;
                cur_baocao.A4_TongSoPhien_Dut = a4.TongSoPhien_Dut;
                cur_baocao.A4_SoPhienCT = a4.SoPhienCT;
                cur_baocao.A4_SoPhienCT_Dut = a4.SoPhienCT_Dut;
                cur_baocao.A4_DienS = a4.DienS;
                cur_baocao.A4_DienR = a4.DienR;
                cur_baocao.A4_GhiChu = a4.GhiChu;
            }
            
            ViBa_A6 a6 = db.ViBa_A6.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a6!=null)
            {
                cur_baocao.A6_SoDoiTuong = a6.SoDoiTuong;
                cur_baocao.A6_GioLienLac = a6.GioLienLac;
                cur_baocao.A6_GioPhatLienLac = a6.GioPhatLienLac;
                cur_baocao.A6_GioPhat = a6.GioPhat;
                cur_baocao.A6_GioPhat_Dut = a6.GioPhat_Dut;
            }
           
            HDT_B2 b2 = db.HDT_B2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b2!=null)
            {
                cur_baocao.B2_SoDoiTuong = b2.SoDoiTuong;
                cur_baocao.B2_LanChuyenTiep = b2.LanChuyenTiep;
                cur_baocao.B2_SoLanDut = b2.SoLanDut;
                cur_baocao.B2_DayTieuHao = b2.DayTieuHao;
                cur_baocao.B2_GioLienLac = b2.GioLienLac;
                cur_baocao.B2_PhutLienLac = b2.PhutLienLac;
            }
            
            HTD_B3 b3 = db.HTD_B3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b3!=null)
            {
                cur_baocao.B3_MayDung = b3.MayDung;
                cur_baocao.B3_MaySua = b3.MaySua;
                cur_baocao.B3_MayDong = b3.MayDong;
                cur_baocao.B3_DatMayMoi = b3.DatMayMoi;
                cur_baocao.B3_TongSoTrKe = b3.TongSoTrKe;
                cur_baocao.B3_TongSoTrKe_Dut = b3.TongSoTrKe_Dut;
                cur_baocao.B3_TongSoTrKeNB = b3.TongSoTrKeNB;
                cur_baocao.B3_TongSoTrKeNB_Dut = b3.TongSoTrKeNB_Dut;
                cur_baocao.B3_GhiChu = b3.GhiChu;
            }

            QuanBuu_47_B4 b4_47 = db.QuanBuu_47_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_47!=null)
            {
                cur_baocao.B4_47_TongVanHanh = b4_47.TongVanHanh;
                cur_baocao.B4_47_TongVanHanh_Dut = b4_47.TongVanHanh_Dut;
                cur_baocao.B4_47_GhiChu = b4_47.GhiChu;
            }
           
            QuanBuu_HoaToc_B4 b4_ht= db.QuanBuu_HoaToc_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_47!=null)
            {
                cur_baocao.B4_HoaToc_Ve = b4_ht.Ve;
                cur_baocao.B4_HoaToc_Di = b4_ht.Di;
                cur_baocao.B4_HoaToc_Dong = b4_ht.Dong;
                cur_baocao.B4_HoaToc_GhiChu = b4_ht.GhiChu;
            }
            
            QuanBuu_CongVan_B4 b4_cv= db.QuanBuu_CongVan_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_cv!=null)
            {
                cur_baocao.B4_CongVan_Ve = b4_cv.Ve;
                cur_baocao.B4_CongVan_Di = b4_cv.Di;
                cur_baocao.B4_CongVan_Dong = b4_cv.Dong;
                cur_baocao.B4_CongVan_GhiChu = b4_cv.GhiChu;
            }
           
            QuanBuu_VanKien_B4 b4_vk = db.QuanBuu_VanKien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_vk!=null)
            {
                cur_baocao.B4_VanKien_Ve = b4_vk.Ve;
                cur_baocao.B4_VanKien_Di = b4_vk.Di;
                cur_baocao.B4_VanKien_Dong = b4_vk.Dong;
                cur_baocao.B4_VanKien_GhiChu = b4_vk.GhiChu;
            }
          
            QuanBuu_ThuBao_B4 b4_tb = db.QuanBuu_ThuBao_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_tb!=null)
            {
                cur_baocao.B4_ThuBao_Ve = b4_tb.Ve;
                cur_baocao.B4_ThuBao_Di = b4_tb.Di;
                cur_baocao.B4_ThuBao_Dong = b4_tb.Dong;
                cur_baocao.B4_ThuBao_GhiChu = b4_tb.GhiChu;
            }
         
            QuanBuu_PhuongTien_B4 b4_pt = db.QuanBuu_PhuongTien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_pt!=null)
            {
                cur_baocao.B4_PhuongTien_XeDap = b4_pt.XeDap;
                cur_baocao.B4_PhuongTien_MoTo = b4_pt.MoTo;
                cur_baocao.B4_PhuongTien_OTo = b4_pt.OTo;
                cur_baocao.B4_PhuongTien_SoChuyen = b4_pt.SoChuyen;
                cur_baocao.B4_PhuongTien_TrongLuong = b4_pt.TrongLuong;
                cur_baocao.B4_PhuongTien_CuLy = b4_pt.CuLy;
                cur_baocao.B4_PhuongTien_GhiChu = b4_pt.GhiChu;
            }
           
            if (baoCao == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaoCaoID = baoCao.BaoCaoID;
            return View(cur_baocao);


        }
        public ActionResult Details(int? id)
        {
            //Kiểm tra xem người đang đăng nhập
            if (Request.IsAuthenticated)
            {

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            //kiểm tra xem có báo cáo ngày chưa?
            string username = User.Identity.GetUserName();
            tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
            int idUser = user.idUser;
            //Đối với admin có thể xem báo cáo
            //int donViID = user.idDonVi.Value;
            BaoCaoNgay baoCao = db.BaoCaoNgay.Where(x => x.BaoCaoID == id).FirstOrDefault();
            //Lấy thông tin của các Bảng để tạo CBaoCao
            TBBD_A1 a1 = db.TBBD_A1.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            DonBien_A2 a2 = db.DonBien_A2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            CBaoCao cur_baocao = new CBaoCao();
            cur_baocao.TrucBanTruong = baoCao.TrucBanTruong;
            cur_baocao.TrucBanPho = baoCao.TrucBanPho;
            cur_baocao.GhiChuChung = baoCao.GhiChu;
            cur_baocao.NguoiBaoCao = baoCao.NguoiBaoCao;
            ViewBag.Gui = baoCao.IsGui;
            ViewBag.DonVi = db.tblDonVi.Where(x => x.DonViID == baoCao.DonViID).Select(x => x.TenDonVi).First();
            ViewBag.NgayBaoCao = "Ngày " + baoCao.NgayBaoCao.Value.Day.ToString() + " tháng " + baoCao.NgayBaoCao.Value.Month.ToString() + " năm " +
                baoCao.NgayBaoCao.Value.Year.ToString();
            if (a1 != null)
            {
                cur_baocao.A1_CanhBTTM = a1.CanhBTTM;
                cur_baocao.A1_CanhBTTM_Dut = a1.CanhBTTM_Dut;
                cur_baocao.A1_CanhQK = a1.CanhQK;
                cur_baocao.A1_CanhQK_Dut = a1.CanhQK_Dut;
                cur_baocao.A1_Lanphatchuan = a1.Lanphatchuan;
                cur_baocao.A1_ThuTinHieuBo = a1.ThuTinHieuBo;
                cur_baocao.A1_ThuTinHieuQK = a1.ThuTinHieuQK;
                cur_baocao.A1_QKPhatTinHieu = a1.QKPhatTinHieu;
                cur_baocao.A1_GhiChu = a1.GhiChu;
            }
            if (a2 != null)
            {
                cur_baocao.A2_SoDoiTuong = a2.SoDoiTuong;
                cur_baocao.A2_TongSoPhien = a2.TongSoPhien;
                cur_baocao.A2_TongSoPhien_Dut = a2.TongSoPhien_Dut;
                cur_baocao.A2_SoPhienCT = a2.SoPhienCT;
                cur_baocao.A2_SoPhienCT_Dut = a2.SoPhienCT_Dut;
                cur_baocao.A2_DienS = a2.DienS;
                cur_baocao.A2_DienT = a2.DienT;
                cur_baocao.A2_GhiChu = a2.GhiChu;
            }

            SongNgan_BTTM_A3 a3_1 = db.SongNgan_BTTM_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_1 != null)
            {
                cur_baocao.A3_BTTM_TongSoPhien = a3_1.TongSoPhien;
                cur_baocao.A3_BTTM_TongSoPhien_Dut = a3_1.TongSoPhien_Dut;
                cur_baocao.A3_BTTM_GoiCanh = a3_1.GoiCanh;
                cur_baocao.A3_BTTM_GoiCanh_Dut = a3_1.GoiCanh_Dut;
                cur_baocao.A3_BTTM_TraLoiCanh = a3_1.TraLoiCanh;
                cur_baocao.A3_BTTM_TraLoiCanh_Dut = a3_1.TraLoiCanh_Dut;
                cur_baocao.A3_BTTM_DienS = a3_1.DienS;
                cur_baocao.A3_BTTM_DienR = a3_1.DienR;
                cur_baocao.A3_BTTM_GhiChu = a3_1.GhiChu;
            }

            SongNgan_NoiBo_A3 a3_2 = db.SongNgan_NoiBo_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_2 != null)
            {
                cur_baocao.A3_NoiBo_SoDoiTuong = a3_2.SoDoiTuong;
                cur_baocao.A3_NoiBo_TongSoPhien = a3_2.TongSoPhien;
                cur_baocao.A3_NoiBo_TongSoPhien_Dut = a3_2.TongSoPhien_Dut;
                cur_baocao.A3_NoiBo_SoPhienCT = a3_2.SoPhienCT;
                cur_baocao.A3_NoiBo_SoPhienCT_Dut = a3_2.SoPhienCT_Dut;
                cur_baocao.A3_NoiBo_TraLoiCanh = a3_2.TraLoiCanh;
                cur_baocao.A3_NoiBo_TraLoiCanh_Dut = a3_2.TraLoiCanh_Dut;
                cur_baocao.A3_NoiBo_GoiCanh = a3_2.GoiCanh;
                cur_baocao.A3_NoiBo_GoiCanh_Dut = a3_2.GoiCanh_Dut;
                cur_baocao.A3_NoiBo_DienS = a3_2.DienS;
                cur_baocao.A3_NoiBo_DienR = a3_2.DienR;
                cur_baocao.A3_NoiBo_DienDong = a3_2.DienDong;
                cur_baocao.A3_NoiBo_GhiChu = a3_2.GhiChu;
            }

            SongNgan_HiepDong_A3 a3_3 = db.SongNgan_HiepDong_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a3_3 != null)
            {
                cur_baocao.A3_HiepDong_TraLoiCanh = a3_3.TraLoiCanh;
                cur_baocao.A3_HiepDong_TraLoiCanh_Dut = a3_3.TraLoiCanh_Dut;
                cur_baocao.A3_HiepDong_DienS = a3_3.DienS;
                cur_baocao.A3_HiepDong_DienR = a3_3.DienR;
                cur_baocao.A3_HiepDong_GhiChu = a3_3.GhiChu;
            }

            SCN_A4 a4 = db.SCN_A4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a4 != null)
            {
                cur_baocao.A4_SoDoiTuong = a4.SoDoiTuong;
                cur_baocao.A4_TongSoPhien = a4.TongSoPhien;
                cur_baocao.A4_TongSoPhien_Dut = a4.TongSoPhien_Dut;
                cur_baocao.A4_SoPhienCT = a4.SoPhienCT;
                cur_baocao.A4_SoPhienCT_Dut = a4.SoPhienCT_Dut;
                cur_baocao.A4_DienS = a4.DienS;
                cur_baocao.A4_DienR = a4.DienR;
                cur_baocao.A4_GhiChu = a4.GhiChu;
            }

            ViBa_A6 a6 = db.ViBa_A6.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (a6 != null)
            {
                cur_baocao.A6_SoDoiTuong = a6.SoDoiTuong;
                cur_baocao.A6_GioLienLac = a6.GioLienLac;
                cur_baocao.A6_GioPhatLienLac = a6.GioPhatLienLac;
                cur_baocao.A6_GioPhat = a6.GioPhat;
                cur_baocao.A6_GioPhat_Dut = a6.GioPhat_Dut;
            }

            HDT_B2 b2 = db.HDT_B2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b2 != null)
            {
                cur_baocao.B2_SoDoiTuong = b2.SoDoiTuong;
                cur_baocao.B2_LanChuyenTiep = b2.LanChuyenTiep;
                cur_baocao.B2_SoLanDut = b2.SoLanDut;
                cur_baocao.B2_DayTieuHao = b2.DayTieuHao;
                cur_baocao.B2_GioLienLac = b2.GioLienLac;
                cur_baocao.B2_PhutLienLac = b2.PhutLienLac;
            }

            HTD_B3 b3 = db.HTD_B3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b3 != null)
            {
                cur_baocao.B3_MayDung = b3.MayDung;
                cur_baocao.B3_MaySua = b3.MaySua;
                cur_baocao.B3_MayDong = b3.MayDong;
                cur_baocao.B3_DatMayMoi = b3.DatMayMoi;
                cur_baocao.B3_TongSoTrKe = b3.TongSoTrKe;
                cur_baocao.B3_TongSoTrKe_Dut = b3.TongSoTrKe_Dut;
                cur_baocao.B3_TongSoTrKeNB = b3.TongSoTrKeNB;
                cur_baocao.B3_TongSoTrKeNB_Dut = b3.TongSoTrKeNB_Dut;
                cur_baocao.B3_GhiChu = b3.GhiChu;
            }

            QuanBuu_47_B4 b4_47 = db.QuanBuu_47_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_47 != null)
            {
                cur_baocao.B4_47_TongVanHanh = b4_47.TongVanHanh;
                cur_baocao.B4_47_TongVanHanh_Dut = b4_47.TongVanHanh_Dut;
                cur_baocao.B4_47_GhiChu = b4_47.GhiChu;
            }

            QuanBuu_HoaToc_B4 b4_ht = db.QuanBuu_HoaToc_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_47 != null)
            {
                cur_baocao.B4_HoaToc_Ve = b4_ht.Ve;
                cur_baocao.B4_HoaToc_Di = b4_ht.Di;
                cur_baocao.B4_HoaToc_Dong = b4_ht.Dong;
                cur_baocao.B4_HoaToc_GhiChu = b4_ht.GhiChu;
            }

            QuanBuu_CongVan_B4 b4_cv = db.QuanBuu_CongVan_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_cv != null)
            {
                cur_baocao.B4_CongVan_Ve = b4_cv.Ve;
                cur_baocao.B4_CongVan_Di = b4_cv.Di;
                cur_baocao.B4_CongVan_Dong = b4_cv.Dong;
                cur_baocao.B4_CongVan_GhiChu = b4_cv.GhiChu;
            }

            QuanBuu_VanKien_B4 b4_vk = db.QuanBuu_VanKien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_vk != null)
            {
                cur_baocao.B4_VanKien_Ve = b4_vk.Ve;
                cur_baocao.B4_VanKien_Di = b4_vk.Di;
                cur_baocao.B4_VanKien_Dong = b4_vk.Dong;
                cur_baocao.B4_VanKien_GhiChu = b4_vk.GhiChu;
            }

            QuanBuu_ThuBao_B4 b4_tb = db.QuanBuu_ThuBao_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_tb != null)
            {
                cur_baocao.B4_ThuBao_Ve = b4_tb.Ve;
                cur_baocao.B4_ThuBao_Di = b4_tb.Di;
                cur_baocao.B4_ThuBao_Dong = b4_tb.Dong;
                cur_baocao.B4_ThuBao_GhiChu = b4_tb.GhiChu;
            }

            QuanBuu_PhuongTien_B4 b4_pt = db.QuanBuu_PhuongTien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).FirstOrDefault();
            if (b4_pt != null)
            {
                cur_baocao.B4_PhuongTien_XeDap = b4_pt.XeDap;
                cur_baocao.B4_PhuongTien_MoTo = b4_pt.MoTo;
                cur_baocao.B4_PhuongTien_OTo = b4_pt.OTo;
                cur_baocao.B4_PhuongTien_SoChuyen = b4_pt.SoChuyen;
                cur_baocao.B4_PhuongTien_TrongLuong = b4_pt.TrongLuong;
                cur_baocao.B4_PhuongTien_CuLy = b4_pt.CuLy;
                cur_baocao.B4_PhuongTien_GhiChu = b4_pt.GhiChu;
            }

            if (baoCao == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaoCaoID = baoCao.BaoCaoID;
            return View(cur_baocao);


        }
        // POST: TBBD_A1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = @"A1_CanhBTTM,A1_CanhBTTM_Dut,A1_CanhQK,A1_CanhQK_Dut,A1_Lanphatchuan,A1_ThuTinHieuBo,A1_ThuTinHieuQK,A1_QKPhatTinHieu,A1_GhiChu,
                                                     A2_SoDoiTuong,A2_TongSoPhien,A2_TongSoPhien_Dut,A2_SoPhienCT,A2_SoPhienCT_Dut,A2_DienS,A2_DienT,A2_GhiChu,
                                                ,A3_BTTM_TongSoPhien
                                                ,A3_BTTM_TongSoPhien_Dut
                                                ,A3_BTTM_GoiCanh
                                                ,A3_BTTM_GoiCanh_Dut
                                                ,A3_BTTM_TraLoiCanh
                                                ,A3_BTTM_TraLoiCanh_Dut
                                                ,A3_BTTM_DienS
                                                ,A3_BTTM_DienR
                                                ,A3_BTTM_GhiChu

                                                ,A3_NoiBo_SoDoiTuong
                                                ,A3_NoiBo_TongSoPhien
                                                ,A3_NoiBo_TongSoPhien_Dut
                                                ,A3_NoiBo_SoPhienCT
                                                ,A3_NoiBo_SoPhienCT_Dut
                                                ,A3_NoiBo_TraLoiCanh
                                                ,A3_NoiBo_TraLoiCanh_Dut
                                                ,A3_NoiBo_GoiCanh
                                                ,A3_NoiBo_GoiCanh_Dut
                                                ,A3_NoiBo_DienS
                                                ,A3_NoiBo_DienR
                                                ,A3_NoiBo_DienDong
                                                ,A3_NoiBo_GhiChu

                                                ,A3_HiepDong_TraLoiCanh
                                                ,A3_HiepDong_TraLoiCanh_Dut
                                                ,A3_HiepDong_DienS
                                                ,A3_HiepDong_DienR
                                                ,A3_HiepDong_GhiChu

                                                ,A4_SoDoiTuong
                                                ,A4_TongSoPhien
                                                ,A4_TongSoPhien_Dut
                                                ,A4_SoPhienCT
                                                ,A4_SoPhienCT_Dut
                                                ,A4_DienS
                                                ,A4_DienR
                                                ,A4_GhiChu

                                                ,A6_SoDoiTuong
                                                ,A6_GioLienLac
                                                ,A6_GioPhatLienLac
                                                ,A6_GioPhat
                                                ,A6_GioPhat_Dut
                                                ,A6_GhiChu

                                                ,B2_SoDoiTuong
                                                ,B2_LanChuyenTiep
                                                ,B2_SoLanDut
                                                ,B2_DayTieuHao
                                                ,B2_GioLienLac
                                                ,B2_PhutLienLac
                                                ,B2_GhiChu

                                                ,B3_MayDung
                                                ,B3_MaySua
                                                ,B3_MayDong
                                                ,B3_DatMayMoi
                                                ,B3_TongSoTrKe
                                                ,B3_TongSoTrKe_Dut
                                                ,B3_TongSoTrKeNB
                                                ,B3_TongSoTrKeNB_Dut
                                                ,B3_GhiChu

                                                ,B4_47_TongVanHanh
                                                ,B4_47_TongVanHanh_Dut
                                                ,B4_47_GhiChu

                                                ,B4_HoaToc_Ve
                                                ,B4_HoaToc_Di
                                                ,B4_HoaToc_Dong
                                                ,B4_HoaToc_GhiChu

                                                ,B4_CongVan_Ve
                                                ,B4_CongVan_Di
                                                ,B4_CongVan_Dong
                                                ,B4_CongVan_GhiChu

                                                ,B4_VanKien_Ve
                                                ,B4_VanKien_Di
                                                ,B4_VanKien_Dong
                                                ,B4_VanKien_GhiChu

                                                ,B4_ThuBao_Ve
                                                ,B4_ThuBao_Di
                                                ,B4_ThuBao_Dong
                                                ,B4_ThuBao_GhiChu

                                                ,B4_PhuongTien_XeDap
                                                ,B4_PhuongTien_MoTo
                                                ,B4_PhuongTien_OTo
                                                ,B4_PhuongTien_SoChuyen
                                                ,B4_PhuongTien_TrongLuong
                                                ,B4_PhuongTien_CuLy
                                                ,B4_PhuongTien_GhiChu

                                                ,GhiChuChung, NguoiBaoCao,
                                                       TrucBanTruong, TrucBanPho
                                                ,BaoCaoID")] CBaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                //cập nhật ghi chú cho báo cáo 
                BaoCaoNgay bc = db.BaoCaoNgay.Find(baoCao.BaoCaoID);       
                bc.NguoiBaoCao = baoCao.NguoiBaoCao;
                bc.GhiChu = baoCao.GhiChuChung;
                bc.TrucBanTruong = baoCao.TrucBanTruong;
                bc.TrucBanPho = baoCao.TrucBanPho;
                db.Entry(bc).State = EntityState.Modified;
                db.SaveChanges();
                //Cập nhật báo cáo
                try
                {
                    TBBD_A1 tBBD_A1 = db.TBBD_A1.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    tBBD_A1.BaoCaoID = baoCao.BaoCaoID;
                    tBBD_A1.CanhBTTM = baoCao.A1_CanhBTTM;
                    tBBD_A1.CanhBTTM_Dut = baoCao.A1_CanhBTTM_Dut;
                    tBBD_A1.CanhQK = baoCao.A1_CanhQK;
                    tBBD_A1.CanhQK_Dut = baoCao.A1_CanhQK_Dut;
                    tBBD_A1.Lanphatchuan = baoCao.A1_Lanphatchuan;
                    tBBD_A1.ThuTinHieuBo = baoCao.A1_ThuTinHieuBo;
                    tBBD_A1.ThuTinHieuQK = baoCao.A1_ThuTinHieuQK;
                    tBBD_A1.QKPhatTinHieu = baoCao.A1_QKPhatTinHieu;
                    tBBD_A1.GhiChu = baoCao.A1_GhiChu;
                    db.Entry(tBBD_A1).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    //Nếu chưa có thì phải tạo ra
                    TBBD_A1 tBBD_A1 = new TBBD_A1();
                    tBBD_A1.BaoCaoID = baoCao.BaoCaoID;
                    tBBD_A1.CanhBTTM = baoCao.A1_CanhBTTM;
                    tBBD_A1.CanhBTTM_Dut = baoCao.A1_CanhBTTM_Dut;
                    tBBD_A1.CanhQK = baoCao.A1_CanhQK;
                    tBBD_A1.CanhQK_Dut = baoCao.A1_CanhQK_Dut;
                    tBBD_A1.Lanphatchuan = baoCao.A1_Lanphatchuan;
                    tBBD_A1.ThuTinHieuBo = baoCao.A1_ThuTinHieuBo;
                    tBBD_A1.ThuTinHieuQK = baoCao.A1_ThuTinHieuQK;
                    tBBD_A1.QKPhatTinHieu = baoCao.A1_QKPhatTinHieu;
                    tBBD_A1.GhiChu = baoCao.A1_GhiChu;
                    db.TBBD_A1.Add(tBBD_A1);
                    db.SaveChanges();
                }
                try
                {
                    DonBien_A2 a2 = db.DonBien_A2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a2.SoDoiTuong = baoCao.A2_SoDoiTuong;
                    a2.TongSoPhien = baoCao.A2_TongSoPhien;
                    a2.TongSoPhien_Dut = baoCao.A2_TongSoPhien_Dut;
                    a2.SoPhienCT = baoCao.A2_SoPhienCT;
                    a2.SoPhienCT_Dut = baoCao.A2_SoPhienCT_Dut;
                    a2.DienS = baoCao.A2_DienS;
                    a2.DienT = baoCao.A2_DienT;
                    a2.GhiChu = baoCao.A2_GhiChu;
                    db.Entry(a2).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    DonBien_A2 a2 = new DonBien_A2();
                    a2.SoDoiTuong = baoCao.A2_SoDoiTuong;
                    a2.TongSoPhien = baoCao.A2_TongSoPhien;
                    a2.TongSoPhien_Dut = baoCao.A2_TongSoPhien_Dut;
                    a2.SoPhienCT = baoCao.A2_SoPhienCT;
                    a2.SoPhienCT_Dut = baoCao.A2_SoPhienCT_Dut;
                    a2.DienS = baoCao.A2_DienS;
                    a2.DienT = baoCao.A2_DienT;
                    a2.BaoCaoID = baoCao.BaoCaoID;
                    db.DonBien_A2.Add(a2);
                    db.SaveChanges();
                }
                try
                {
                    SongNgan_BTTM_A3 a3_BTTM = db.SongNgan_BTTM_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a3_BTTM.BaoCaoID = baoCao.BaoCaoID;
                    a3_BTTM.TongSoPhien = baoCao.A3_BTTM_TongSoPhien;
                    a3_BTTM.TongSoPhien_Dut = baoCao.A3_BTTM_TongSoPhien_Dut;
                    a3_BTTM.GoiCanh = baoCao.A3_BTTM_GoiCanh;
                    a3_BTTM.GoiCanh_Dut = baoCao.A3_BTTM_GoiCanh_Dut;
                    a3_BTTM.TraLoiCanh = baoCao.A3_BTTM_TraLoiCanh;
                    a3_BTTM.TraLoiCanh_Dut = baoCao.A3_BTTM_TraLoiCanh_Dut;
                    a3_BTTM.DienS = baoCao.A3_BTTM_DienS;
                    a3_BTTM.DienR = baoCao.A3_BTTM_DienR;
                    a3_BTTM.GhiChu = baoCao.A3_BTTM_GhiChu;
                    db.Entry(a3_BTTM).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    SongNgan_BTTM_A3 a3_BTTM = new SongNgan_BTTM_A3();
                    a3_BTTM.BaoCaoID = baoCao.BaoCaoID;
                    a3_BTTM.TongSoPhien = baoCao.A3_BTTM_TongSoPhien;
                    a3_BTTM.TongSoPhien_Dut = baoCao.A3_BTTM_TongSoPhien_Dut;
                    a3_BTTM.GoiCanh = baoCao.A3_BTTM_GoiCanh;
                    a3_BTTM.GoiCanh_Dut = baoCao.A3_BTTM_GoiCanh_Dut;
                    a3_BTTM.TraLoiCanh = baoCao.A3_BTTM_TraLoiCanh;
                    a3_BTTM.TraLoiCanh_Dut = baoCao.A3_BTTM_TraLoiCanh_Dut;
                    a3_BTTM.DienS = baoCao.A3_BTTM_DienS;
                    a3_BTTM.DienR = baoCao.A3_BTTM_DienR;
                    a3_BTTM.GhiChu = baoCao.A3_BTTM_GhiChu;
                    db.SongNgan_BTTM_A3.Add(a3_BTTM);
                    db.SaveChanges();
                }

                try
                {
                    SongNgan_NoiBo_A3 a3_NoiBo = db.SongNgan_NoiBo_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a3_NoiBo.BaoCaoID = baoCao.BaoCaoID;
                    a3_NoiBo.SoDoiTuong = baoCao.A3_NoiBo_SoDoiTuong;
                    a3_NoiBo.TongSoPhien = baoCao.A3_NoiBo_TongSoPhien;
                    a3_NoiBo.TongSoPhien_Dut = baoCao.A3_NoiBo_TongSoPhien_Dut;
                    a3_NoiBo.SoPhienCT = baoCao.A3_NoiBo_SoPhienCT;
                    a3_NoiBo.SoPhienCT_Dut = baoCao.A3_NoiBo_SoPhienCT_Dut;
                    a3_NoiBo.TraLoiCanh = baoCao.A3_NoiBo_TraLoiCanh;
                    a3_NoiBo.TraLoiCanh_Dut = baoCao.A3_NoiBo_TraLoiCanh_Dut;
                    a3_NoiBo.GoiCanh = baoCao.A3_NoiBo_GoiCanh;
                    a3_NoiBo.GoiCanh_Dut = baoCao.A3_NoiBo_GoiCanh_Dut;
                    a3_NoiBo.DienS = baoCao.A3_NoiBo_DienS;
                    a3_NoiBo.DienR = baoCao.A3_NoiBo_DienR;
                    a3_NoiBo.GhiChu = baoCao.A3_NoiBo_GhiChu;
                    db.Entry(a3_NoiBo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    SongNgan_NoiBo_A3 a3_NoiBo = new SongNgan_NoiBo_A3();
                    a3_NoiBo.BaoCaoID = baoCao.BaoCaoID;
                    a3_NoiBo.SoDoiTuong = baoCao.A3_NoiBo_SoDoiTuong;
                    a3_NoiBo.TongSoPhien = baoCao.A3_NoiBo_TongSoPhien;
                    a3_NoiBo.TongSoPhien_Dut = baoCao.A3_NoiBo_TongSoPhien_Dut;
                    a3_NoiBo.SoPhienCT = baoCao.A3_NoiBo_SoPhienCT;
                    a3_NoiBo.SoPhienCT_Dut = baoCao.A3_NoiBo_SoPhienCT_Dut;
                    a3_NoiBo.TraLoiCanh = baoCao.A3_NoiBo_TraLoiCanh;
                    a3_NoiBo.TraLoiCanh_Dut = baoCao.A3_NoiBo_TraLoiCanh_Dut;
                    a3_NoiBo.GoiCanh = baoCao.A3_NoiBo_GoiCanh;
                    a3_NoiBo.GoiCanh_Dut = baoCao.A3_NoiBo_GoiCanh_Dut;
                    a3_NoiBo.GhiChu = baoCao.A3_NoiBo_GhiChu;
                    db.SongNgan_NoiBo_A3.Add(a3_NoiBo);
                    db.SaveChanges();

                }

                try
                {
                    SongNgan_HiepDong_A3 a3_HiepDong = db.SongNgan_HiepDong_A3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a3_HiepDong.BaoCaoID = baoCao.BaoCaoID;
                    a3_HiepDong.TraLoiCanh = baoCao.A3_HiepDong_TraLoiCanh;
                    a3_HiepDong.TraLoiCanh_Dut = baoCao.A3_HiepDong_TraLoiCanh_Dut;
                    a3_HiepDong.DienS = baoCao.A3_HiepDong_DienS;
                    a3_HiepDong.DienR = baoCao.A3_HiepDong_DienR;
                    a3_HiepDong.GhiChu = baoCao.A3_HiepDong_GhiChu;
                    db.Entry(a3_HiepDong).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    SongNgan_HiepDong_A3 a3_HiepDong = new SongNgan_HiepDong_A3();
                    a3_HiepDong.BaoCaoID = baoCao.BaoCaoID;
                    a3_HiepDong.TraLoiCanh = baoCao.A3_HiepDong_TraLoiCanh;
                    a3_HiepDong.TraLoiCanh_Dut = baoCao.A3_HiepDong_TraLoiCanh_Dut;
                    a3_HiepDong.DienS = baoCao.A3_HiepDong_DienS;
                    a3_HiepDong.DienR = baoCao.A3_HiepDong_DienR;
                    a3_HiepDong.GhiChu = baoCao.A3_HiepDong_GhiChu;
                    db.SongNgan_HiepDong_A3.Add(a3_HiepDong);
                    db.SaveChanges();
                }

                try
                {
                    SCN_A4 a4 = db.SCN_A4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a4.BaoCaoID = baoCao.BaoCaoID;
                    a4.SoDoiTuong = baoCao.A4_SoDoiTuong;
                    a4.TongSoPhien = baoCao.A4_TongSoPhien;
                    a4.TongSoPhien_Dut = baoCao.A4_TongSoPhien_Dut;
                    a4.SoPhienCT = baoCao.A4_SoPhienCT;
                    a4.SoPhienCT_Dut = baoCao.A4_SoPhienCT_Dut;
                    a4.DienS = baoCao.A4_DienS;
                    a4.DienR = baoCao.A4_DienR;
                    a4.GhiChu = baoCao.A4_GhiChu;
                    db.Entry(a4).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    SCN_A4 a4 = new SCN_A4();
                    a4.BaoCaoID = baoCao.BaoCaoID;
                    a4.SoDoiTuong = baoCao.A4_SoDoiTuong;
                    a4.TongSoPhien = baoCao.A4_TongSoPhien;
                    a4.TongSoPhien_Dut = baoCao.A4_TongSoPhien_Dut;
                    a4.SoPhienCT = baoCao.A4_SoPhienCT;
                    a4.SoPhienCT_Dut = baoCao.A4_SoPhienCT_Dut;
                    a4.DienS = baoCao.A4_DienS;
                    a4.DienR = baoCao.A4_DienR;
                    a4.GhiChu = baoCao.A4_GhiChu;
                    db.SCN_A4.Add(a4);
                    db.SaveChanges();
                }

                try
                {
                    ViBa_A6 a6 = db.ViBa_A6.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    a6.BaoCaoID = baoCao.BaoCaoID;
                    a6.SoDoiTuong = baoCao.A6_SoDoiTuong;
                    a6.GioLienLac = baoCao.A6_GioLienLac;
                    a6.GioPhatLienLac = baoCao.A6_GioPhatLienLac;
                    a6.GioPhat = baoCao.A6_GioPhat;
                    a6.GioPhat_Dut = baoCao.A6_GioPhat_Dut;
                    a6.GhiChu = baoCao.A6_GhiChu;
                    db.Entry(a6).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    ViBa_A6 a6 = new ViBa_A6();
                    a6.BaoCaoID = baoCao.BaoCaoID;
                    a6.SoDoiTuong = baoCao.A6_SoDoiTuong;
                    a6.GioLienLac = baoCao.A6_GioLienLac;
                    a6.GioPhatLienLac = baoCao.A6_GioPhatLienLac;
                    a6.GioPhat = baoCao.A6_GioPhat;
                    a6.GioPhat_Dut = baoCao.A6_GioPhat_Dut;
                    a6.GhiChu = baoCao.A6_GhiChu;
                    db.ViBa_A6.Add(a6);
                    db.SaveChanges();
                }
                try
                {

                    HDT_B2 b2 = db.HDT_B2.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b2.BaoCaoID = baoCao.BaoCaoID;
                    b2.SoDoiTuong = baoCao.B2_SoDoiTuong;
                    b2.LanChuyenTiep = baoCao.B2_LanChuyenTiep;
                    b2.SoLanDut = baoCao.B2_SoLanDut;
                    b2.DayTieuHao = baoCao.B2_DayTieuHao;
                    b2.GioLienLac = baoCao.B2_GioLienLac;
                    b2.PhutLienLac = baoCao.B2_PhutLienLac;
                    b2.GhiChu = baoCao.B2_GhiChu;
                    db.Entry(b2).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    HDT_B2 b2 = new HDT_B2();
                    b2.BaoCaoID = baoCao.BaoCaoID;
                    b2.SoDoiTuong = baoCao.B2_SoDoiTuong;
                    b2.LanChuyenTiep = baoCao.B2_LanChuyenTiep;
                    b2.SoLanDut = baoCao.B2_SoLanDut;
                    b2.DayTieuHao = baoCao.B2_DayTieuHao;
                    b2.GioLienLac = baoCao.B2_GioLienLac;
                    b2.PhutLienLac = baoCao.B2_PhutLienLac;
                    b2.GhiChu = baoCao.B2_GhiChu;
                    db.HDT_B2.Add(b2);
                    db.SaveChanges();
                }
                try
                {
                    HTD_B3 b3 = db.HTD_B3.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b3.BaoCaoID = baoCao.BaoCaoID;
                    b3.MayDung = baoCao.B3_MayDung;
                    b3.MaySua = baoCao.B3_MaySua;
                    b3.MayDong = baoCao.B3_MayDong;
                    b3.DatMayMoi = baoCao.B3_DatMayMoi;
                    b3.TongSoTrKe = baoCao.B3_TongSoTrKe;
                    b3.TongSoTrKe_Dut = baoCao.B3_TongSoTrKe_Dut;
                    b3.TongSoTrKeNB = baoCao.B3_TongSoTrKeNB;
                    b3.TongSoTrKeNB_Dut = baoCao.B3_TongSoTrKeNB_Dut;
                    b3.GhiChu = baoCao.B3_GhiChu;
                    db.Entry(b3).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    HTD_B3 b3 = new HTD_B3();
                    b3.BaoCaoID = baoCao.BaoCaoID;
                    b3.MayDung = baoCao.B3_MayDung;
                    b3.MaySua = baoCao.B3_MaySua;
                    b3.MayDong = baoCao.B3_MayDong;
                    b3.DatMayMoi = baoCao.B3_DatMayMoi;
                    b3.TongSoTrKe = baoCao.B3_TongSoTrKe;
                    b3.TongSoTrKe_Dut = baoCao.B3_TongSoTrKe_Dut;
                    b3.TongSoTrKeNB = baoCao.B3_TongSoTrKeNB;
                    b3.TongSoTrKeNB_Dut = baoCao.B3_TongSoTrKeNB_Dut;
                    b3.GhiChu = baoCao.B3_GhiChu;
                    db.HTD_B3.Add(b3);
                    db.SaveChanges();
                }

                try
                {
                    QuanBuu_47_B4 b4 = db.QuanBuu_47_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4.BaoCaoID = baoCao.BaoCaoID;
                    b4.TongVanHanh = baoCao.B4_47_TongVanHanh;
                    b4.TongVanHanh_Dut = baoCao.B4_47_TongVanHanh_Dut;
                    b4.GhiChu = baoCao.B4_47_GhiChu;
                    db.Entry(b4).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    QuanBuu_47_B4 b4 = new QuanBuu_47_B4();
                    b4.BaoCaoID = baoCao.BaoCaoID;
                    b4.TongVanHanh = baoCao.B4_47_TongVanHanh;
                    b4.TongVanHanh_Dut = baoCao.B4_47_TongVanHanh_Dut;
                    b4.GhiChu = baoCao.B4_47_GhiChu;
                    db.QuanBuu_47_B4.Add(b4); db.SaveChanges();

                }
                try
                {
                    QuanBuu_HoaToc_B4 b4_HoaToc = db.QuanBuu_HoaToc_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4_HoaToc.BaoCaoID = baoCao.BaoCaoID;
                    b4_HoaToc.Ve = baoCao.B4_HoaToc_Ve;
                    b4_HoaToc.Di = baoCao.B4_HoaToc_Di;
                    b4_HoaToc.Dong = baoCao.B4_HoaToc_Dong;
                    b4_HoaToc.GhiChu = baoCao.B4_HoaToc_GhiChu;
                    db.Entry(b4_HoaToc).State = EntityState.Modified; db.SaveChanges();
                }
                catch (Exception)
                {

                    QuanBuu_HoaToc_B4 b4_HoaToc = new QuanBuu_HoaToc_B4();
                    b4_HoaToc.BaoCaoID = baoCao.BaoCaoID;
                    b4_HoaToc.Ve = baoCao.B4_HoaToc_Ve;
                    b4_HoaToc.Di = baoCao.B4_HoaToc_Di;
                    b4_HoaToc.Dong = baoCao.B4_HoaToc_Dong;
                    b4_HoaToc.GhiChu = baoCao.B4_HoaToc_GhiChu;
                    db.QuanBuu_HoaToc_B4.Add(b4_HoaToc); db.SaveChanges();
                }
                try
                {

                    QuanBuu_CongVan_B4 b4_CongVan = db.QuanBuu_CongVan_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4_CongVan.BaoCaoID = baoCao.BaoCaoID;
                    b4_CongVan.Ve = baoCao.B4_CongVan_Ve;
                    b4_CongVan.Di = baoCao.B4_CongVan_Di;
                    b4_CongVan.Dong = baoCao.B4_CongVan_Dong;
                    b4_CongVan.GhiChu = baoCao.B4_CongVan_GhiChu;
                    db.Entry(b4_CongVan).State = EntityState.Modified; db.SaveChanges();
                }
                catch (Exception)
                {
                    QuanBuu_CongVan_B4 b4_CongVan = new QuanBuu_CongVan_B4();
                    b4_CongVan.BaoCaoID = baoCao.BaoCaoID;
                    b4_CongVan.Ve = baoCao.B4_CongVan_Ve;
                    b4_CongVan.Di = baoCao.B4_CongVan_Di;
                    b4_CongVan.Dong = baoCao.B4_CongVan_Dong;
                    b4_CongVan.GhiChu = baoCao.B4_CongVan_GhiChu;
                    db.QuanBuu_CongVan_B4.Add(b4_CongVan);
                    db.SaveChanges();

                }
                try
                {
                    QuanBuu_VanKien_B4 b4_VanKien = db.QuanBuu_VanKien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4_VanKien.BaoCaoID = baoCao.BaoCaoID;
                    b4_VanKien.Ve = baoCao.B4_VanKien_Ve;
                    b4_VanKien.Di = baoCao.B4_VanKien_Di;
                    b4_VanKien.Dong = baoCao.B4_VanKien_Dong;
                    b4_VanKien.GhiChu = baoCao.B4_VanKien_GhiChu;
                    db.Entry(b4_VanKien).State = EntityState.Modified; db.SaveChanges();
                }
                catch (Exception)
                {
                    QuanBuu_VanKien_B4 b4_VanKien = new QuanBuu_VanKien_B4();
                    b4_VanKien.BaoCaoID = baoCao.BaoCaoID;
                    b4_VanKien.Ve = baoCao.B4_VanKien_Ve;
                    b4_VanKien.Di = baoCao.B4_VanKien_Di;
                    b4_VanKien.Dong = baoCao.B4_VanKien_Dong;
                    b4_VanKien.GhiChu = baoCao.B4_VanKien_GhiChu;
                    db.QuanBuu_VanKien_B4.Add(b4_VanKien); db.SaveChanges();

                }

                try
                {
                    QuanBuu_ThuBao_B4 b4_ThuBao = db.QuanBuu_ThuBao_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4_ThuBao.BaoCaoID = baoCao.BaoCaoID;
                    b4_ThuBao.Ve = baoCao.B4_ThuBao_Ve;
                    b4_ThuBao.Di = baoCao.B4_ThuBao_Di;
                    b4_ThuBao.Dong = baoCao.B4_ThuBao_Dong;
                    b4_ThuBao.GhiChu = baoCao.B4_ThuBao_GhiChu;
                    db.Entry(b4_ThuBao).State = EntityState.Modified; db.SaveChanges();

                }
                catch (Exception)
                {
                    QuanBuu_ThuBao_B4 b4_ThuBao = new QuanBuu_ThuBao_B4();
                    b4_ThuBao.BaoCaoID = baoCao.BaoCaoID;
                    b4_ThuBao.Ve = baoCao.B4_ThuBao_Ve;
                    b4_ThuBao.Di = baoCao.B4_ThuBao_Di;
                    b4_ThuBao.Dong = baoCao.B4_ThuBao_Dong;
                    b4_ThuBao.GhiChu = baoCao.B4_ThuBao_GhiChu;
                    db.QuanBuu_ThuBao_B4.Add(b4_ThuBao);
                    db.SaveChanges();

                }
                try
                {
                    QuanBuu_PhuongTien_B4 b4_PhuongTien = db.QuanBuu_PhuongTien_B4.Where(x => x.BaoCaoID == baoCao.BaoCaoID).First();
                    b4_PhuongTien.BaoCaoID = baoCao.BaoCaoID;
                    b4_PhuongTien.XeDap = baoCao.B4_PhuongTien_XeDap;
                    b4_PhuongTien.MoTo = baoCao.B4_PhuongTien_MoTo;
                    b4_PhuongTien.OTo = baoCao.B4_PhuongTien_OTo;
                    b4_PhuongTien.SoChuyen = baoCao.B4_PhuongTien_SoChuyen;
                    b4_PhuongTien.TrongLuong = baoCao.B4_PhuongTien_TrongLuong;
                    b4_PhuongTien.CuLy = baoCao.B4_PhuongTien_CuLy;
                    b4_PhuongTien.GhiChu = baoCao.B4_PhuongTien_GhiChu;
                    db.Entry(b4_PhuongTien).State = EntityState.Modified; db.SaveChanges();

                }
                catch (Exception)
                {
                    QuanBuu_PhuongTien_B4 b4_PhuongTien = new QuanBuu_PhuongTien_B4();
                    b4_PhuongTien.BaoCaoID = baoCao.BaoCaoID;
                    b4_PhuongTien.XeDap = baoCao.B4_PhuongTien_XeDap;
                    b4_PhuongTien.MoTo = baoCao.B4_PhuongTien_MoTo;
                    b4_PhuongTien.OTo = baoCao.B4_PhuongTien_OTo;
                    b4_PhuongTien.SoChuyen = baoCao.B4_PhuongTien_SoChuyen;
                    b4_PhuongTien.TrongLuong = baoCao.B4_PhuongTien_TrongLuong;
                    b4_PhuongTien.CuLy = baoCao.B4_PhuongTien_CuLy;
                    b4_PhuongTien.GhiChu = baoCao.B4_PhuongTien_GhiChu;
                    db.QuanBuu_PhuongTien_B4.Add(b4_PhuongTien); db.SaveChanges();

                }            

             
                //RedirectToAction("Action", new { id = 99 });
                DateTime ngaybaocao = DateTime.Now;
                lg.LogError("Sửa báo cáo theo ngày: " + ngaybaocao.ToString() + ", baocaoid:" + baoCao.BaoCaoID +
                  " donviid: ");
                return RedirectToAction("Details_empty", "CBaoCao" );

            }
            ViewBag.BaoCaoID = new SelectList(db.BaoCaoNgay, "BaoCaoID", "NguoiBaoCao", baoCao.BaoCaoID);
            return View();
        }

        // GET: TBBD_A1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCaoNgay baocao = db.BaoCaoNgay.Where(x => x.BaoCaoID == id).FirstOrDefault();

            if (baocao == null)
            {
                return HttpNotFound();
            }
            return View(baocao);
        }

        // POST: TBBD_A1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaoCaoNgay baocao = db.BaoCaoNgay.Where(x => x.BaoCaoID == id).FirstOrDefault();
            db.BaoCaoNgay.Remove(baocao);
            db.SaveChanges();
            return RedirectToAction("Details_empty", "CBaoCao");
        }
        public ActionResult Gui(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCaoNgay baoCao = db.BaoCaoNgay.Find(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }else
            {
                //Gửi báo cáo đi
                baoCao.IsGui = true;
                db.Entry(baoCao).State = EntityState.Modified;
                db.SaveChanges();
                lg.LogError("Gửi báo cáo đơn vị: " + baoCao.DonViID.ToString());
            }    
            return RedirectToAction("Details_empty", "CBaoCao");
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
