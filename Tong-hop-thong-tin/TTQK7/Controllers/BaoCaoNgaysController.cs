using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTQK7;

namespace TTQK7.Controllers
{
    public class BaoCaoNgaysController : Controller
    {
        private TTQK7Entities db = new TTQK7Entities();

        //Báo cáo tháng
        [Authorize]
        public ActionResult BaoCaoThang()
        {
            var list = db.tblDonVi.Where(x => x.ParentID == 23).ToList();
            tblDonVi obj = new TTQK7.tblDonVi();
            obj.DonViID = 23;
            obj.TenDonVi = "TOÀN QUÂN KHU";
            list.Insert(0, obj);
            ViewBag.DonVi = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BaoCaoThang(int valThang, int valNam, int valDonVi)
        {
            if (ModelState.IsValid)
            {                            
                var stream = new MemoryStream();
                tblDonVi obj = db.tblDonVi.Find(valDonVi);
                string excelName = "BaoCaoThang" + valThang.ToString() + "-" + valNam.ToString() + ".xlsx";
                if (obj != null)
                {
                    excelName = obj.TenDonVi + "_" + excelName;
                }
                
                using (var package = new ExcelPackage(stream))
                {
                    var Sheet = package.Workbook.Worksheets.Add("Sheet1");
                    Sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    Sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    Sheet.Cells.Style.Font.Size = 10; //Default font size for whole sheet
                    Sheet.Cells.Style.Font.Name = "Arial"; //Default Font name for whole sheet
                    //thực hiện hàm load thông tin ở đây
                    Sheet.Cells["A1:A4"].Merge = true;
                    Sheet.Cells["A1"].Value = "STT";
                    Sheet.Cells["B1:B4"].Merge = true;
                    Sheet.Cells["B1"].Value = "Ngày tháng";

                    Sheet.Cells["C1:H2"].Merge = true;
                    Sheet.Cells["C1"].Value = "TBBĐ (A1)";

                    Sheet.Cells["I1:L2"].Merge = true;
                    Sheet.Cells["I1"].Value = "ĐƠN BIÊN (A2)";

                    Sheet.Cells["M2:P2"].Merge = true;
                    Sheet.Cells["M2"].Value = "BTTM";

                    Sheet.Cells["Q2:W2"].Merge = true;
                    Sheet.Cells["Q2"].Value = "Nội bộ";

                    Sheet.Cells["X2:Y2"].Merge = true;
                    Sheet.Cells["X2"].Value = "Hiệp đồng";

                    Sheet.Cells["M1:Y1"].Merge = true;
                    Sheet.Cells["M1"].Value = "SÓNG NGẮN (A3)";

                    Sheet.Cells["Z1:AC2"].Merge = true;
                    Sheet.Cells["Z1"].Value = "SCN (A4)";

                    Sheet.Cells["AD1:AF2"].Merge = true;
                    Sheet.Cells["AD1"].Value = "VIBA (A6)";

                    Sheet.Cells["AG1:AQ2"].Merge = true;
                    Sheet.Cells["AG1"].Value = "HTĐ (B2 + B3)";

                    Sheet.Cells["AR1:BJ1"].Merge = true;
                    Sheet.Cells["AR1"].Value = "QUÂN BƯU (B4)";

                    Sheet.Cells["AR2:AR3"].Merge = true;
                    Sheet.Cells["AR2"].Value = "47";

                    Sheet.Cells["AS2:AU2"].Merge = true;
                    Sheet.Cells["AS2"].Value = "Hỏa tốc";

                    Sheet.Cells["Av2:Ax2"].Merge = true;
                    Sheet.Cells["Av2"].Value = "Công văn";

                    Sheet.Cells["Ay2:ba2"].Merge = true;
                    Sheet.Cells["Ay2"].Value = "Văn kiện";

                    Sheet.Cells["bb2:bd2"].Merge = true;
                    Sheet.Cells["bb2"].Value = "Thư báo";

                    Sheet.Cells["be2:bj2"].Merge = true;
                    Sheet.Cells["be2"].Value = "Phương tiện";

                    Sheet.Cells["bk1:bk4"].Merge = true;
                    Sheet.Cells["bk1"].Value = "Ghi chú";

                    Sheet.Cells["A1:bk4"].Style.Font.Bold   = true;
                    Sheet.Cells["A1:bk4"].Style.Font.Size  = 9;

                    //tiêu đề dòng 3
                    Sheet.Cells[3, 3].Value = "1";
                    Sheet.Cells[3, 4].Value = "2";
                    Sheet.Cells[3, 5].Value = "3";
                    Sheet.Cells[3, 6].Value = "4";
                    Sheet.Cells[3, 7].Value = "5";
                    Sheet.Cells[3, 8].Value = "6";
                    Sheet.Cells[3, 9].Value = "7";
                    Sheet.Cells[3, 10].Value = "8";
                    Sheet.Cells[3, 11].Value = "9";
                    Sheet.Cells[3, 12].Value = "10";
                    Sheet.Cells[3, 13].Value = "11";
                    Sheet.Cells[3, 14].Value = "12";
                    Sheet.Cells[3, 15].Value = "13";
                    Sheet.Cells[3, 16].Value = "14";
                    Sheet.Cells[3, 17].Value = "15";
                    Sheet.Cells[3, 18].Value = "16";
                    Sheet.Cells[3, 19].Value = "17";
                    Sheet.Cells[3, 20].Value = "18";
                    Sheet.Cells[3, 21].Value = "19";
                    Sheet.Cells[3, 22].Value = "20";
                    Sheet.Cells[3, 23].Value = "21";
                    Sheet.Cells[3, 24].Value = "22";
                    Sheet.Cells[3, 25].Value = "23";
                    Sheet.Cells[3, 26].Value = "24";
                    Sheet.Cells[3, 27].Value = "25";
                    Sheet.Cells[3, 28].Value = "26";
                    Sheet.Cells[3, 29].Value = "27";
                    Sheet.Cells[3, 30].Value = "31";
                    Sheet.Cells[3, 31].Value = "32";
                    Sheet.Cells[3, 32].Value = "33";
                    Sheet.Cells[3, 33].Value = "36";
                    Sheet.Cells[3, 34].Value = "37";
                    Sheet.Cells[3, 35].Value = "38";
                    Sheet.Cells[3, 36].Value = "39";
                    Sheet.Cells[3, 37].Value = "40";
                    Sheet.Cells[3, 38].Value = "41";
                    Sheet.Cells[3, 39].Value = "42";
                    Sheet.Cells[3, 40].Value = "43";
                    Sheet.Cells[3, 41].Value = "44";
                    Sheet.Cells[3, 42].Value = "45";
                    Sheet.Cells[3, 43].Value = "46";
                    Sheet.Cells[3, 45].Value = "48";
                    Sheet.Cells[3, 46].Value = "49";
                    Sheet.Cells[3, 47].Value = "50";
                    Sheet.Cells[3, 48].Value = "51";
                    Sheet.Cells[3, 49].Value = "52";
                    Sheet.Cells[3, 50].Value = "53";
                    Sheet.Cells[3, 51].Value = "54";
                    Sheet.Cells[3, 52].Value = "55";
                    Sheet.Cells[3, 53].Value = "56";
                    Sheet.Cells[3, 54].Value = "57";
                    Sheet.Cells[3, 55].Value = "58";
                    Sheet.Cells[3, 56].Value = "59";
                    Sheet.Cells[3, 57].Value = "60";
                    Sheet.Cells[3, 58].Value = "61";
                    Sheet.Cells[3, 59].Value = "62";
                    Sheet.Cells[3, 60].Value = "63";
                    Sheet.Cells[3, 61].Value = "64";
                    Sheet.Cells[3, 62].Value = "65";


                    //tiêu đề dòng 4                   
                    Sheet.Cells[4, 3].Value = "Canh BTTM/đứt";
                    Sheet.Cells[4, 4].Value = "Canh QK/đứt";
                    Sheet.Cells[4, 5].Value = "Lần phát chuẩn";
                    Sheet.Cells[4, 6].Value = "Thu t/hiệu Bộ";
                    Sheet.Cells[4, 7].Value = "Thu t/hiệu QK";
                    Sheet.Cells[4, 8].Value = "QK phát t/hiệu";
                    Sheet.Cells[4, 9].Value = "Số đ/tượng";
                    Sheet.Cells[4, 10].Value = "T.số phiên/đứt";
                    Sheet.Cells[4, 11].Value = "Số phiên CT/đứt";
                    Sheet.Cells[4, 12].Value = "Điện S/R";
                    Sheet.Cells[4, 13].Value = "T.số phiên/đứt";
                    Sheet.Cells[4, 14].Value = "Gọi canh/đứt";
                    Sheet.Cells[4, 15].Value = "Trả lời canh/đứt";
                    Sheet.Cells[4, 16].Value = "Điện S/R";
                    Sheet.Cells[4, 17].Value = "Số đ/tượng";
                    Sheet.Cells[4, 18].Value = "T.số phiên/đứt";
                    Sheet.Cells[4, 19].Value = "Số phiên CT/đứt";
                    Sheet.Cells[4, 20].Value = "Trả lời canh.đứt";
                    Sheet.Cells[4, 21].Value = "Gọi canh/đứt";
                    Sheet.Cells[4, 22].Value = "Điện S/R";
                    Sheet.Cells[4, 23].Value = "Điện đọng";
                    Sheet.Cells[4, 24].Value = "Trả lời canh/đứt";
                    Sheet.Cells[4, 25].Value = "Điện S/R";
                    Sheet.Cells[4, 26].Value = "Số đ/tượng";
                    Sheet.Cells[4, 27].Value = "T.số phiên/đứt";
                    Sheet.Cells[4, 28].Value = "Số phiên CT/đứt";
                    Sheet.Cells[4, 29].Value = "Điện S/R";
                    Sheet.Cells[4, 30].Value = "Số đ/tượng";
                    Sheet.Cells[4, 31].Value = "Giờ/phát liên lạc";
                    Sheet.Cells[4, 32].Value = "Giờ/phát đứt";
                    Sheet.Cells[4, 33].Value = "Số đ/tượng";
                    Sheet.Cells[4, 34].Value = "Lần chuyển tiếp";
                    Sheet.Cells[4, 35].Value = "Số lần đứt";
                    Sheet.Cells[4, 36].Value = "Dây tiêu hao";
                    Sheet.Cells[4, 37].Value = "Giờ/phút liên lạc";
                    Sheet.Cells[4, 38].Value = "Máy dùng";
                    Sheet.Cells[4, 39].Value = "Máy sửa";
                    Sheet.Cells[4, 40].Value = "Máy đọng";
                    Sheet.Cells[4, 41].Value = "Đặt máy mới";
                    Sheet.Cells[4, 42].Value = "T.số Tr.kế/đứt";
                    Sheet.Cells[4, 43].Value = "T.số Tr.kế NB/đứt";
                    Sheet.Cells[4, 44].Value = "T.vận hành/đứt";
                    Sheet.Cells[4, 45].Value = "Về";
                    Sheet.Cells[4, 46].Value = "Đi";
                    Sheet.Cells[4, 47].Value = "Đọng";
                    Sheet.Cells[4, 48].Value = "Về";
                    Sheet.Cells[4, 49].Value = "Đi";
                    Sheet.Cells[4, 50].Value = "Đọng";
                    Sheet.Cells[4, 51].Value = "Về";
                    Sheet.Cells[4, 52].Value = "Đi";
                    Sheet.Cells[4, 53].Value = "Đọng";
                    Sheet.Cells[4, 54].Value = "Về";
                    Sheet.Cells[4, 55].Value = "Đi";
                    Sheet.Cells[4, 56].Value = "Đọng";
                    Sheet.Cells[4, 57].Value = "Xe đạp";
                    Sheet.Cells[4, 58].Value = "Mô tô";
                    Sheet.Cells[4, 59].Value = "Ô tô";
                    Sheet.Cells[4, 60].Value = "Số chuyến";
                    Sheet.Cells[4, 61].Value = "Trọng lượng";
                    Sheet.Cells[4, 62].Value = "Cự ly";
                    
                    int i = 1;
                    if (valDonVi == 23)
                    {
                        var list = db.sp_Thanh_BaoCaoThang(valThang, valNam);
                        foreach (var item in list)
                        {
                            Sheet.Cells[i + 4, 1].Value = i.ToString();
                            Sheet.Cells[i + 4, 2].Value = item.NgayBaoCao.Value.ToString("dd/MM/yyyy");
                            if (item.TBBD_A1_CanhBTTM.Value != 0 || item.TBBD_A1_CanhBTTM_Dut.Value != 0)
                            {
                                Sheet.Cells[i + 4, 3].Value = item.TBBD_A1_CanhBTTM.Value.ToString() + "/" + item.TBBD_A1_CanhBTTM_Dut.Value.ToString();
                            }
                            if (item.TBBD_A1_CanhQK.Value != 0 || item.TBBD_A1_CanhQK_Dut.Value != 0)
                            {
                                Sheet.Cells[i + 4, 4].Value = item.TBBD_A1_CanhQK.Value.ToString() + "/" + item.TBBD_A1_CanhQK_Dut.Value.ToString();
                            }
                            if (item.TBBD_A1_Lanphatchuan.Value != 0)
                            {
                                Sheet.Cells[i + 4, 5].Value = item.TBBD_A1_Lanphatchuan.Value.ToString();
                            }
                            if (item.TBBD_A1_ThuTinHieuBo.Value != 0) { Sheet.Cells[i + 4, 6].Value = item.TBBD_A1_ThuTinHieuBo.Value.ToString(); }
                            if (item.TBBD_A1_ThuTinHieuQK.Value != 0) { Sheet.Cells[i + 4, 7].Value = item.TBBD_A1_ThuTinHieuQK.Value.ToString(); }
                            if (item.TBBD_A1_QKPhatTinHieu.Value != 0) { Sheet.Cells[i + 4, 8].Value = item.TBBD_A1_QKPhatTinHieu.Value.ToString(); }
                            if (item.DonBien_A2_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 9].Value = item.DonBien_A2_SoDoiTuong.Value.ToString(); }
                            if (item.DonBien_A2_TongSoPhien.Value != 0 || item.DonBien_A2_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 10].Value = item.DonBien_A2_TongSoPhien.Value.ToString() + "/" + item.DonBien_A2_TongSoPhien_Dut.Value.ToString(); }
                            if (item.DonBien_A2_SoPhienCT.Value != 0 || item.DonBien_A2_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 11].Value = item.DonBien_A2_SoPhienCT.Value.ToString() + "/" + item.DonBien_A2_SoPhienCT_Dut.Value.ToString(); }
                            if (item.DonBien_A2_DienS.Value != 0 || item.DonBien_A2_DienT.Value != 0) { Sheet.Cells[i + 4, 12].Value = item.DonBien_A2_DienS.Value.ToString() + "/" + item.DonBien_A2_DienT.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_TongSoPhien.Value != 0 || item.SongNgan_BTTM_A3_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 13].Value = item.SongNgan_BTTM_A3_TongSoPhien.Value.ToString() + "/" + item.SongNgan_BTTM_A3_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_GoiCanh.Value != 0 || item.SongNgan_BTTM_A3_GoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 14].Value = item.SongNgan_BTTM_A3_GoiCanh.Value.ToString() + "/" + item.SongNgan_BTTM_A3_GoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_TraLoiCanh.Value != 0 || item.SongNgan_BTTM_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 15].Value = item.SongNgan_BTTM_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_BTTM_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_DienS.Value != 0 || item.SongNgan_HiepDong_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 16].Value = item.SongNgan_HiepDong_A3_DienS.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_DienR.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 17].Value = item.SongNgan_NoiBo_A3_SoDoiTuong.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_TongSoPhien.Value != 0 || item.SongNgan_NoiBo_A3_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 18].Value = item.SongNgan_NoiBo_A3_TongSoPhien.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_SoPhienCT.Value != 0 || item.SongNgan_NoiBo_A3_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 19].Value = item.SongNgan_NoiBo_A3_SoPhienCT.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_SoPhienCT_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_TraLoiCanh.Value != 0 || item.SongNgan_NoiBo_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 20].Value = item.SongNgan_NoiBo_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_GoiCanh.Value != 0 || item.SongNgan_NoiBo_A3_GoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 21].Value = item.SongNgan_NoiBo_A3_GoiCanh.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_GoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_DienS.Value != 0 || item.SongNgan_NoiBo_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 22].Value = item.SongNgan_NoiBo_A3_DienS.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_DienR.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_DienDong.Value != 0) { Sheet.Cells[i + 4, 23].Value = item.SongNgan_NoiBo_A3_DienDong.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_TraLoiCanh.Value != 0 || item.SongNgan_HiepDong_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 24].Value = item.SongNgan_HiepDong_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_DienS.Value != 0 || item.SongNgan_HiepDong_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 25].Value = item.SongNgan_HiepDong_A3_DienS.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_DienR.Value.ToString(); }
                            if (item.SCN_A4_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 26].Value = item.SCN_A4_SoDoiTuong.Value.ToString(); }
                            if (item.SCN_A4_TongSoPhien.Value != 0 || item.SCN_A4_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 27].Value = item.SCN_A4_TongSoPhien.Value.ToString() + "/" + item.SCN_A4_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SCN_A4_SoPhienCT.Value != 0 || item.SCN_A4_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 28].Value = item.SCN_A4_SoPhienCT.Value.ToString() + "/" + item.SCN_A4_SoPhienCT_Dut.Value.ToString(); }
                            if (item.SCN_A4_DienS.Value != 0 || item.SCN_A4_DienR.Value != 0) { Sheet.Cells[i + 4, 29].Value = item.SCN_A4_DienS.Value.ToString() + "/" + item.SCN_A4_DienR.Value.ToString(); }
                            if (item.ViBa_A6_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 30].Value = item.ViBa_A6_SoDoiTuong.Value.ToString(); }
                            if (item.ViBa_A6_GioLienLac.Value != 0 || item.ViBa_A6_GioPhatLienLac.Value != 0) { Sheet.Cells[i + 4, 31].Value = item.ViBa_A6_GioLienLac.Value.ToString() + "/" + item.ViBa_A6_GioPhatLienLac.Value.ToString(); }
                            if (item.ViBa_A6_GioPhat.Value != 0 || item.ViBa_A6_GioPhat_Dut.Value != 0) { Sheet.Cells[i + 4, 32].Value = item.ViBa_A6_GioPhat.Value.ToString() + "/" + item.ViBa_A6_GioPhat_Dut.Value.ToString(); }
                            if (item.HDT_B2_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 33].Value = item.HDT_B2_SoDoiTuong.Value.ToString(); }
                            if (item.HDT_B2_LanChuyenTiep.Value != 0) { Sheet.Cells[i + 4, 34].Value = item.HDT_B2_LanChuyenTiep.Value.ToString(); }
                            if (item.HDT_B2_SoLanDut.Value != 0) { Sheet.Cells[i + 4, 35].Value = item.HDT_B2_SoLanDut.Value.ToString(); }
                            if (item.HDT_B2_DayTieuHao.Value != 0) { Sheet.Cells[i + 4, 36].Value = item.HDT_B2_DayTieuHao.Value.ToString(); }
                            if (item.HDT_B2_GioLienLac.Value != 0 || item.HDT_B2_PhutLienLac.Value != 0) { Sheet.Cells[i + 4, 37].Value = item.HDT_B2_GioLienLac.Value.ToString() + "/" + item.HDT_B2_PhutLienLac.Value.ToString(); }
                            if (item.HTD_B3_MayDung.Value != 0) { Sheet.Cells[i + 4, 38].Value = item.HTD_B3_MayDung.Value.ToString(); }
                            if (item.HTD_B3_MaySua.Value != 0) { Sheet.Cells[i + 4, 39].Value = item.HTD_B3_MaySua.Value.ToString(); }
                            if (item.HTD_B3_MayDong.Value != 0) { Sheet.Cells[i + 4, 40].Value = item.HTD_B3_MayDong.Value.ToString(); }
                            if (item.HTD_B3_DatMayMoi.Value != 0) { Sheet.Cells[i + 4, 41].Value = item.HTD_B3_DatMayMoi.Value.ToString(); }
                            if (item.HTD_B3_TongSoTrKe.Value != 0 || item.HTD_B3_TongSoTrKe_Dut.Value != 0) { Sheet.Cells[i + 4, 42].Value = item.HTD_B3_TongSoTrKe.Value.ToString() + "/" + item.HTD_B3_TongSoTrKe_Dut.Value.ToString(); }
                            if (item.HTD_B3_TongSoTrKeNB.Value != 0 || item.HTD_B3_TongSoTrKeNB_Dut.Value != 0) { Sheet.Cells[i + 4, 43].Value = item.HTD_B3_TongSoTrKeNB.Value.ToString() + "/" + item.HTD_B3_TongSoTrKeNB_Dut.Value.ToString(); }
                            if (item.QuanBuu_47_B4_TongVanHanh.Value != 0 || item.QuanBuu_47_B4_TongVanHanh_Dut.Value != 0) { Sheet.Cells[i + 4, 44].Value = item.QuanBuu_47_B4_TongVanHanh.Value.ToString() + "/" + item.QuanBuu_47_B4_TongVanHanh_Dut.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 45].Value = item.QuanBuu_HoaToc_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Di.Value != 0) { Sheet.Cells[i + 4, 46].Value = item.QuanBuu_HoaToc_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 47].Value = item.QuanBuu_HoaToc_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 48].Value = item.QuanBuu_CongVan_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Di.Value != 0) { Sheet.Cells[i + 4, 49].Value = item.QuanBuu_CongVan_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 50].Value = item.QuanBuu_CongVan_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 51].Value = item.QuanBuu_VanKien_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Di.Value != 0) { Sheet.Cells[i + 4, 52].Value = item.QuanBuu_VanKien_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 53].Value = item.QuanBuu_VanKien_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 54].Value = item.QuanBuu_ThuBao_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Di.Value != 0) { Sheet.Cells[i + 4, 55].Value = item.QuanBuu_ThuBao_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 56].Value = item.QuanBuu_ThuBao_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_XeDap.Value != 0) { Sheet.Cells[i + 4, 57].Value = item.QuanBuu_PhuongTien_B4_XeDap.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_MoTo.Value != 0) { Sheet.Cells[i + 4, 58].Value = item.QuanBuu_PhuongTien_B4_MoTo.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_OTo.Value != 0) { Sheet.Cells[i + 4, 59].Value = item.QuanBuu_PhuongTien_B4_OTo.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_SoChuyen.Value != 0) { Sheet.Cells[i + 4, 60].Value = item.QuanBuu_PhuongTien_B4_SoChuyen.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_TrongLuong.Value != 0) { Sheet.Cells[i + 4, 61].Value = item.QuanBuu_PhuongTien_B4_TrongLuong.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_CuLy.Value != 0) { Sheet.Cells[i + 4, 62].Value = item.QuanBuu_PhuongTien_B4_CuLy.Value.ToString(); }
                            Sheet.Cells[i + 4, 63].Value = item.GhiChu;

                            i++;
                        }
                    }
                    else
                    {
                        var list = db.sp_Thanh_BaoCaoThang_DonVi(valThang, valNam, valDonVi );
                        foreach (var item in list)
                        {
                            Sheet.Cells[i + 4, 1].Value = i.ToString();
                            Sheet.Cells[i + 4, 2].Value = item.NgayBaoCao.Value.ToString("dd/MM/yyyy");
                            if (item.TBBD_A1_CanhBTTM.Value != 0 || item.TBBD_A1_CanhBTTM_Dut.Value != 0)
                            {
                                Sheet.Cells[i + 4, 3].Value = item.TBBD_A1_CanhBTTM.Value.ToString() + "/" + item.TBBD_A1_CanhBTTM_Dut.Value.ToString();
                            }
                            if (item.TBBD_A1_CanhQK.Value != 0 || item.TBBD_A1_CanhQK_Dut.Value != 0)
                            {
                                Sheet.Cells[i + 4, 4].Value = item.TBBD_A1_CanhQK.Value.ToString() + "/" + item.TBBD_A1_CanhQK_Dut.Value.ToString();
                            }
                            if (item.TBBD_A1_Lanphatchuan.Value != 0)
                            {
                                Sheet.Cells[i + 4, 5].Value = item.TBBD_A1_Lanphatchuan.Value.ToString();
                            }
                            if (item.TBBD_A1_ThuTinHieuBo.Value != 0) { Sheet.Cells[i + 4, 6].Value = item.TBBD_A1_ThuTinHieuBo.Value.ToString(); }
                            if (item.TBBD_A1_ThuTinHieuQK.Value != 0) { Sheet.Cells[i + 4, 7].Value = item.TBBD_A1_ThuTinHieuQK.Value.ToString(); }
                            if (item.TBBD_A1_QKPhatTinHieu.Value != 0) { Sheet.Cells[i + 4, 8].Value = item.TBBD_A1_QKPhatTinHieu.Value.ToString(); }
                            if (item.DonBien_A2_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 9].Value = item.DonBien_A2_SoDoiTuong.Value.ToString(); }
                            if (item.DonBien_A2_TongSoPhien.Value != 0 || item.DonBien_A2_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 10].Value = item.DonBien_A2_TongSoPhien.Value.ToString() + "/" + item.DonBien_A2_TongSoPhien_Dut.Value.ToString(); }
                            if (item.DonBien_A2_SoPhienCT.Value != 0 || item.DonBien_A2_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 11].Value = item.DonBien_A2_SoPhienCT.Value.ToString() + "/" + item.DonBien_A2_SoPhienCT_Dut.Value.ToString(); }
                            if (item.DonBien_A2_DienS.Value != 0 || item.DonBien_A2_DienT.Value != 0) { Sheet.Cells[i + 4, 12].Value = item.DonBien_A2_DienS.Value.ToString() + "/" + item.DonBien_A2_DienT.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_TongSoPhien.Value != 0 || item.SongNgan_BTTM_A3_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 13].Value = item.SongNgan_BTTM_A3_TongSoPhien.Value.ToString() + "/" + item.SongNgan_BTTM_A3_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_GoiCanh.Value != 0 || item.SongNgan_BTTM_A3_GoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 14].Value = item.SongNgan_BTTM_A3_GoiCanh.Value.ToString() + "/" + item.SongNgan_BTTM_A3_GoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_BTTM_A3_TraLoiCanh.Value != 0 || item.SongNgan_BTTM_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 15].Value = item.SongNgan_BTTM_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_BTTM_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_DienS.Value != 0 || item.SongNgan_HiepDong_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 16].Value = item.SongNgan_HiepDong_A3_DienS.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_DienR.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 17].Value = item.SongNgan_NoiBo_A3_SoDoiTuong.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_TongSoPhien.Value != 0 || item.SongNgan_NoiBo_A3_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 18].Value = item.SongNgan_NoiBo_A3_TongSoPhien.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_SoPhienCT.Value != 0 || item.SongNgan_NoiBo_A3_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 19].Value = item.SongNgan_NoiBo_A3_SoPhienCT.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_SoPhienCT_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_TraLoiCanh.Value != 0 || item.SongNgan_NoiBo_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 20].Value = item.SongNgan_NoiBo_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_GoiCanh.Value != 0 || item.SongNgan_NoiBo_A3_GoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 21].Value = item.SongNgan_NoiBo_A3_GoiCanh.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_GoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_DienS.Value != 0 || item.SongNgan_NoiBo_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 22].Value = item.SongNgan_NoiBo_A3_DienS.Value.ToString() + "/" + item.SongNgan_NoiBo_A3_DienR.Value.ToString(); }
                            if (item.SongNgan_NoiBo_A3_DienDong.Value != 0) { Sheet.Cells[i + 4, 23].Value = item.SongNgan_NoiBo_A3_DienDong.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_TraLoiCanh.Value != 0 || item.SongNgan_HiepDong_A3_TraLoiCanh_Dut.Value != 0) { Sheet.Cells[i + 4, 24].Value = item.SongNgan_HiepDong_A3_TraLoiCanh.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_TraLoiCanh_Dut.Value.ToString(); }
                            if (item.SongNgan_HiepDong_A3_DienS.Value != 0 || item.SongNgan_HiepDong_A3_DienR.Value != 0) { Sheet.Cells[i + 4, 25].Value = item.SongNgan_HiepDong_A3_DienS.Value.ToString() + "/" + item.SongNgan_HiepDong_A3_DienR.Value.ToString(); }
                            if (item.SCN_A4_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 26].Value = item.SCN_A4_SoDoiTuong.Value.ToString(); }
                            if (item.SCN_A4_TongSoPhien.Value != 0 || item.SCN_A4_TongSoPhien_Dut.Value != 0) { Sheet.Cells[i + 4, 27].Value = item.SCN_A4_TongSoPhien.Value.ToString() + "/" + item.SCN_A4_TongSoPhien_Dut.Value.ToString(); }
                            if (item.SCN_A4_SoPhienCT.Value != 0 || item.SCN_A4_SoPhienCT_Dut.Value != 0) { Sheet.Cells[i + 4, 28].Value = item.SCN_A4_SoPhienCT.Value.ToString() + "/" + item.SCN_A4_SoPhienCT_Dut.Value.ToString(); }
                            if (item.SCN_A4_DienS.Value != 0 || item.SCN_A4_DienR.Value != 0) { Sheet.Cells[i + 4, 29].Value = item.SCN_A4_DienS.Value.ToString() + "/" + item.SCN_A4_DienR.Value.ToString(); }
                            if (item.ViBa_A6_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 30].Value = item.ViBa_A6_SoDoiTuong.Value.ToString(); }
                            if (item.ViBa_A6_GioLienLac.Value != 0 || item.ViBa_A6_GioPhatLienLac.Value != 0) { Sheet.Cells[i + 4, 31].Value = item.ViBa_A6_GioLienLac.Value.ToString() + "/" + item.ViBa_A6_GioPhatLienLac.Value.ToString(); }
                            if (item.ViBa_A6_GioPhat.Value != 0 || item.ViBa_A6_GioPhat_Dut.Value != 0) { Sheet.Cells[i + 4, 32].Value = item.ViBa_A6_GioPhat.Value.ToString() + "/" + item.ViBa_A6_GioPhat_Dut.Value.ToString(); }
                            if (item.HDT_B2_SoDoiTuong.Value != 0) { Sheet.Cells[i + 4, 33].Value = item.HDT_B2_SoDoiTuong.Value.ToString(); }
                            if (item.HDT_B2_LanChuyenTiep.Value != 0) { Sheet.Cells[i + 4, 34].Value = item.HDT_B2_LanChuyenTiep.Value.ToString(); }
                            if (item.HDT_B2_SoLanDut.Value != 0) { Sheet.Cells[i + 4, 35].Value = item.HDT_B2_SoLanDut.Value.ToString(); }
                            if (item.HDT_B2_DayTieuHao.Value != 0) { Sheet.Cells[i + 4, 36].Value = item.HDT_B2_DayTieuHao.Value.ToString(); }
                            if (item.HDT_B2_GioLienLac.Value != 0 || item.HDT_B2_PhutLienLac.Value != 0) { Sheet.Cells[i + 4, 37].Value = item.HDT_B2_GioLienLac.Value.ToString() + "/" + item.HDT_B2_PhutLienLac.Value.ToString(); }
                            if (item.HTD_B3_MayDung.Value != 0) { Sheet.Cells[i + 4, 38].Value = item.HTD_B3_MayDung.Value.ToString(); }
                            if (item.HTD_B3_MaySua.Value != 0) { Sheet.Cells[i + 4, 39].Value = item.HTD_B3_MaySua.Value.ToString(); }
                            if (item.HTD_B3_MayDong.Value != 0) { Sheet.Cells[i + 4, 40].Value = item.HTD_B3_MayDong.Value.ToString(); }
                            if (item.HTD_B3_DatMayMoi.Value != 0) { Sheet.Cells[i + 4, 41].Value = item.HTD_B3_DatMayMoi.Value.ToString(); }
                            if (item.HTD_B3_TongSoTrKe.Value != 0 || item.HTD_B3_TongSoTrKe_Dut.Value != 0) { Sheet.Cells[i + 4, 42].Value = item.HTD_B3_TongSoTrKe.Value.ToString() + "/" + item.HTD_B3_TongSoTrKe_Dut.Value.ToString(); }
                            if (item.HTD_B3_TongSoTrKeNB.Value != 0 || item.HTD_B3_TongSoTrKeNB_Dut.Value != 0) { Sheet.Cells[i + 4, 43].Value = item.HTD_B3_TongSoTrKeNB.Value.ToString() + "/" + item.HTD_B3_TongSoTrKeNB_Dut.Value.ToString(); }
                            if (item.QuanBuu_47_B4_TongVanHanh.Value != 0 || item.QuanBuu_47_B4_TongVanHanh_Dut.Value != 0) { Sheet.Cells[i + 4, 44].Value = item.QuanBuu_47_B4_TongVanHanh.Value.ToString() + "/" + item.QuanBuu_47_B4_TongVanHanh_Dut.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 45].Value = item.QuanBuu_HoaToc_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Di.Value != 0) { Sheet.Cells[i + 4, 46].Value = item.QuanBuu_HoaToc_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_HoaToc_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 47].Value = item.QuanBuu_HoaToc_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 48].Value = item.QuanBuu_CongVan_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Di.Value != 0) { Sheet.Cells[i + 4, 49].Value = item.QuanBuu_CongVan_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_CongVan_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 50].Value = item.QuanBuu_CongVan_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 51].Value = item.QuanBuu_VanKien_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Di.Value != 0) { Sheet.Cells[i + 4, 52].Value = item.QuanBuu_VanKien_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_VanKien_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 53].Value = item.QuanBuu_VanKien_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Ve.Value != 0) { Sheet.Cells[i + 4, 54].Value = item.QuanBuu_ThuBao_B4_Ve.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Di.Value != 0) { Sheet.Cells[i + 4, 55].Value = item.QuanBuu_ThuBao_B4_Di.Value.ToString(); }
                            if (item.QuanBuu_ThuBao_B4_Dong.Value != 0) { Sheet.Cells[i + 4, 56].Value = item.QuanBuu_ThuBao_B4_Dong.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_XeDap.Value != 0) { Sheet.Cells[i + 4, 57].Value = item.QuanBuu_PhuongTien_B4_XeDap.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_MoTo.Value != 0) { Sheet.Cells[i + 4, 58].Value = item.QuanBuu_PhuongTien_B4_MoTo.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_OTo.Value != 0) { Sheet.Cells[i + 4, 59].Value = item.QuanBuu_PhuongTien_B4_OTo.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_SoChuyen.Value != 0) { Sheet.Cells[i + 4, 60].Value = item.QuanBuu_PhuongTien_B4_SoChuyen.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_TrongLuong.Value != 0) { Sheet.Cells[i + 4, 61].Value = item.QuanBuu_PhuongTien_B4_TrongLuong.Value.ToString(); }
                            if (item.QuanBuu_PhuongTien_B4_CuLy.Value != 0) { Sheet.Cells[i + 4, 62].Value = item.QuanBuu_PhuongTien_B4_CuLy.Value.ToString(); }
                            Sheet.Cells[i + 4, 63].Value = item.GhiChu;

                            i++;
                        }
                    }
                    //format lại
                    string sRange = "A1:BK" + (i+4 - 1).ToString();
                    ExcelRange range = Sheet.Cells[sRange];
                    range.AutoFitColumns();
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.Black);
                    range.Style.Border.Bottom.Color.SetColor(Color.Black);

                    Sheet.Cells["bk1:bk" + (i + 4 - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                    package.Save();
                }
                stream.Position = 0;
                

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

            return View();
        }
        // GET: BaoCaoNgays
        [Authorize]
        public ActionResult Index(DateTime ngaybaocao)
        {

            //kiểm tra có đúng là admin hay không?
            if (Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                Log lg = new Log();
                if (user != null && user.UserName == "admin")
                {
                    //ngaybaocao = new DateTime(ngaybaocao.Year, ngaybaocao.Month, ngaybaocao.Day);
                    //try
                    //{
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    // the code that you want to measure comes here
                    lg.LogError("Bắt đầu truy vấn báo cáo tổng hợp");
                    //var list = db.BaoCaoTongHop(ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();
                    var list = db.p_BaoCaoTongHop_View(ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    lg.LogError("Đã qua chức năng lấy danh sách(s): " + watch.ElapsedMilliseconds.ToString());
                    ViewBag.a1_1_1 = list.Sum(x => x.TBBD_A1_CanhBTTM);
                    ViewBag.a1_1_2 = list.Sum(x => x.TBBD_A1_CanhBTTM_Dut);
                    ViewBag.a1_2_1 = list.Sum(x => x.TBBD_A1_CanhQK);
                    ViewBag.a1_2_1 = list.Sum(x => x.TBBD_A1_CanhQK_Dut);
                    ViewBag.a1_3 = list.Sum(x => x.TBBD_A1_Lanphatchuan);
                    ViewBag.a1_4 = list.Sum(x => x.TBBD_A1_ThuTinHieuBo);
                    //Lưu ý đếm phần 4, 5, không phải sum

                    ViewBag.a1_5 = list.Count(x => x.TBBD_A1_ThuTinHieuQK == 0);//list.Sum(x => x.TBBD_A1_ThuTinHieuQK);
                    ViewBag.a1_6 = list.Count(x => x.TBBD_A1_QKPhatTinHieu == 0);// list.Sum(x => x.TBBD_A1_QKPhatTinHieu);

                    ViewBag.a2_7 = list.Sum(x => x.DonBien_A2_SoDoiTuong);
                    ViewBag.a2_8_1 = list.Sum(x => x.DonBien_A2_TongSoPhien);
                    ViewBag.a2_8_2 = list.Sum(x => x.DonBien_A2_TongSoPhien_Dut);
                    ViewBag.a2_9_1 = list.Sum(x => x.DonBien_A2_SoPhienCT);
                    ViewBag.a2_9_2 = list.Sum(x => x.DonBien_A2_SoPhienCT_Dut);
                    ViewBag.a2_10_1 = list.Sum(x => x.DonBien_A2_DienS);
                    ViewBag.a2_10_2 = list.Sum(x => x.DonBien_A2_DienT);

                    ViewBag.a3_11_1 = list.Sum(x => x.SongNgan_BTTM_A3_TongSoPhien);
                    ViewBag.a3_11_2 = list.Sum(x => x.SongNgan_BTTM_A3_TongSoPhien_Dut);
                    ViewBag.a3_12_1 = list.Sum(x => x.SongNgan_BTTM_A3_GoiCanh);
                    ViewBag.a3_12_2 = list.Sum(x => x.SongNgan_BTTM_A3_GoiCanh_Dut);
                    ViewBag.a3_13_1 = list.Sum(x => x.SongNgan_BTTM_A3_TraLoiCanh);
                    ViewBag.a3_13_2 = list.Sum(x => x.SongNgan_BTTM_A3_TraLoiCanh_Dut);
                    ViewBag.a3_14_1 = list.Sum(x => x.SongNgan_BTTM_A3_DienS);
                    ViewBag.a3_14_2 = list.Sum(x => x.SongNgan_BTTM_A3_DienR);
                    ViewBag.a3_15 = list.Sum(x => x.SongNgan_NoiBo_A3_SoDoiTuong);
                    ViewBag.a3_16_1 = list.Sum(x => x.SongNgan_NoiBo_A3_TongSoPhien);
                    ViewBag.a3_16_2 = list.Sum(x => x.SongNgan_NoiBo_A3_TongSoPhien_Dut);
                    ViewBag.a3_17_1 = list.Sum(x => x.SongNgan_NoiBo_A3_SoPhienCT);
                    ViewBag.a3_17_2 = list.Sum(x => x.SongNgan_NoiBo_A3_SoPhienCT_Dut);
                    ViewBag.a3_18_1 = list.Sum(x => x.SongNgan_NoiBo_A3_TraLoiCanh);
                    ViewBag.a3_18_2 = list.Sum(x => x.SongNgan_NoiBo_A3_TraLoiCanh_Dut);
                    ViewBag.a3_19_1 = list.Sum(x => x.SongNgan_NoiBo_A3_GoiCanh);
                    ViewBag.a3_19_2 = list.Sum(x => x.SongNgan_NoiBo_A3_GoiCanh_Dut);
                    ViewBag.a3_20_1 = list.Sum(x => x.SongNgan_NoiBo_A3_DienS);
                    ViewBag.a3_20_2 = list.Sum(x => x.SongNgan_NoiBo_A3_DienR);
                    ViewBag.a3_21_1 = list.Sum(x => x.SongNgan_NoiBo_A3_DienDong);
                    ViewBag.a3_22_1 = list.Sum(x => x.SongNgan_HiepDong_A3_TraLoiCanh);
                    ViewBag.a3_22_2 = list.Sum(x => x.SongNgan_HiepDong_A3_TraLoiCanh_Dut);
                    ViewBag.a3_23_1 = list.Sum(x => x.SongNgan_HiepDong_A3_DienS);
                    ViewBag.a3_23_2 = list.Sum(x => x.SongNgan_HiepDong_A3_DienR);

                    ViewBag.a4_24 = list.Sum(x => x.SCN_A4_SoDoiTuong);
                    ViewBag.a4_25_1 = list.Sum(x => x.SCN_A4_TongSoPhien);
                    ViewBag.a4_25_2 = list.Sum(x => x.SCN_A4_TongSoPhien_Dut);
                    ViewBag.a4_26_1 = list.Sum(x => x.SCN_A4_SoPhienCT);
                    ViewBag.a4_26_2 = list.Sum(x => x.SCN_A4_SoPhienCT_Dut);
                    ViewBag.a4_27_1 = list.Sum(x => x.SCN_A4_DienS);
                    ViewBag.a4_27_2 = list.Sum(x => x.SCN_A4_DienR);

                    ViewBag.a6_31 = list.Sum(x => x.ViBa_A6_SoDoiTuong);
                    ViewBag.a6_32_1 = list.Sum(x => x.ViBa_A6_GioLienLac);
                    ViewBag.a6_32_2 = list.Sum(x => x.ViBa_A6_GioPhatLienLac);
                    ViewBag.a6_33_1 = list.Sum(x => x.ViBa_A6_GioPhat);
                    ViewBag.a6_33_2 = list.Sum(x => x.ViBa_A6_GioPhat_Dut);

                    ViewBag.b2_36 = list.Sum(x => x.HDT_B2_SoDoiTuong);
                    ViewBag.b2_37 = list.Sum(x => x.HDT_B2_LanChuyenTiep);
                    ViewBag.b2_38 = list.Sum(x => x.HDT_B2_SoLanDut);
                    ViewBag.b2_39 = list.Sum(x => x.HDT_B2_DayTieuHao);
                    ViewBag.b2_40_1 = list.Sum(x => x.HDT_B2_GioLienLac);
                    ViewBag.b2_40_2 = list.Sum(x => x.HDT_B2_PhutLienLac);

                    ViewBag.b3_41 = list.Sum(x => x.HTD_B3_MayDung);
                    ViewBag.b3_42 = list.Sum(x => x.HTD_B3_MaySua);
                    ViewBag.b3_43 = list.Sum(x => x.HTD_B3_MayDong);
                    ViewBag.b3_44 = list.Sum(x => x.HTD_B3_DatMayMoi);
                    ViewBag.b3_45_1 = list.Sum(x => x.HTD_B3_TongSoTrKe);
                    ViewBag.b3_45_2 = list.Sum(x => x.HTD_B3_TongSoTrKe_Dut);
                    ViewBag.b3_46_1 = list.Sum(x => x.HTD_B3_TongSoTrKeNB);
                    ViewBag.b3_46_2 = list.Sum(x => x.HTD_B3_TongSoTrKeNB_Dut);

                    ViewBag.b4_47_1 = list.Sum(x => x.QuanBuu_47_B4_TongVanHanh);
                    ViewBag.b4_47_2 = list.Sum(x => x.QuanBuu_47_B4_TongVanHanh_Dut);
                    ViewBag.b4_48 = list.Sum(x => x.QuanBuu_HoaToc_B4_Ve);
                    ViewBag.b4_49 = list.Sum(x => x.QuanBuu_HoaToc_B4_Di);
                    ViewBag.b4_50 = list.Sum(x => x.QuanBuu_HoaToc_B4_Dong);
                    ViewBag.b4_51 = list.Sum(x => x.QuanBuu_CongVan_B4_Ve);
                    ViewBag.b4_52 = list.Sum(x => x.QuanBuu_CongVan_B4_Di);
                    ViewBag.b4_53 = list.Sum(x => x.QuanBuu_CongVan_B4_Dong);
                    ViewBag.b4_54 = list.Sum(x => x.QuanBuu_VanKien_B4_Ve);
                    ViewBag.b4_55 = list.Sum(x => x.QuanBuu_VanKien_B4_Di);
                    ViewBag.b4_56 = list.Sum(x => x.QuanBuu_VanKien_B4_Dong);
                    ViewBag.b4_57 = list.Sum(x => x.QuanBuu_ThuBao_B4_Ve);
                    ViewBag.b4_58 = list.Sum(x => x.QuanBuu_ThuBao_B4_Di);
                    ViewBag.b4_59 = list.Sum(x => x.QuanBuu_ThuBao_B4_Dong);
                    ViewBag.b4_60 = list.Sum(x => x.XeDap);
                    ViewBag.b4_61 = list.Sum(x => x.MoTo);
                    ViewBag.b4_62 = list.Sum(x => x.OTo);
                    ViewBag.b4_63 = list.Sum(x => x.SoChuyen);
                    ViewBag.b4_64 = list.Sum(x => x.TrongLuong);
                    ViewBag.b4_65 = list.Sum(x => x.CuLy);


                    if (list.Count() > 0)
                    {
                        return View(list);
                    }
                    else
                    {
                        return View();
                    }

                    //}
                    //catch (Exception)
                    //{
                    //    //ngaybaocao = new DateTime(2020, 3, 11);
                    //    // var list = db.BaoCaoTongHop().ToList().Where(x => x.NgayBaoCao.Value.Year == ngaybaocao.Year && x.NgayBaoCao.Value.Month
                    //    //== ngaybaocao.Month && x.NgayBaoCao.Value.Day == ngaybaocao.Day && x.IsGui == true).ToList();
                    //    var list = db.BaoCaoTongHop(ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();
                    //    return View(list);
                    //}
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        // GET: BaoCaoNgays
        [Authorize]
        public ActionResult IndexHuyen(DateTime ngaybaocao, int donviID)
        {
            //kiểm tra có đúng là admin hay không?
            if (Request.IsAuthenticated)
            {
                Log lg = new Log();
                string username = User.Identity.GetUserName();
                tblUser user = db.tblUser.Where(x => x.UserName == username).FirstOrDefault();
                if (user != null)
                {
                    //ngaybaocao = new DateTime(ngaybaocao.Year, ngaybaocao.Month, ngaybaocao.Day);
                    try
                    {
                        //Lấy thông tin đơn vị con của đơn vị hiện tại
                        //var list = db.BaoCaoTongHopHuyen(donviID).Where(x => x.NgayBaoCao == null ? false : x.NgayBaoCao.Value.Year == ngaybaocao.Year && x.NgayBaoCao.Value.Month
                        //== ngaybaocao.Month && x.NgayBaoCao.Value.Day == ngaybaocao.Day && x.IsGui == true).ToList();
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        // the code that you want to measure comes here

                        var list = db.BaoCaoTongHopHuyen(donviID, ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();
                        watch.Stop();
                        var elapsedMs = watch.ElapsedMilliseconds;
                        lg.LogError("Đã qua chức năng lấy danh sách huyện: " + watch.ElapsedMilliseconds.ToString());
                        ViewBag.a1_1_1 = list.Sum(x => x.TBBD_A1_CanhBTTM);
                        ViewBag.a1_1_2 = list.Sum(x => x.TBBD_A1_CanhBTTM_Dut);
                        ViewBag.a1_2_1 = list.Sum(x => x.TBBD_A1_CanhQK);
                        ViewBag.a1_2_1 = list.Sum(x => x.TBBD_A1_CanhQK_Dut);
                        ViewBag.a1_3 = list.Sum(x => x.TBBD_A1_Lanphatchuan);
                        ViewBag.a1_4 = list.Sum(x => x.TBBD_A1_ThuTinHieuBo);
                        //Lưu ý đếm phần 4, 5, không phải sum

                        ViewBag.a1_5 = list.Count(x => x.TBBD_A1_ThuTinHieuQK == 0);//list.Sum(x => x.TBBD_A1_ThuTinHieuQK);
                        ViewBag.a1_6 = list.Count(x => x.TBBD_A1_QKPhatTinHieu == 0);// list.Sum(x => x.TBBD_A1_QKPhatTinHieu);

                        ViewBag.a2_7 = list.Sum(x => x.DonBien_A2_SoDoiTuong);
                        ViewBag.a2_8_1 = list.Sum(x => x.DonBien_A2_TongSoPhien);
                        ViewBag.a2_8_2 = list.Sum(x => x.DonBien_A2_TongSoPhien_Dut);
                        ViewBag.a2_9_1 = list.Sum(x => x.DonBien_A2_SoPhienCT);
                        ViewBag.a2_9_2 = list.Sum(x => x.DonBien_A2_SoPhienCT_Dut);
                        ViewBag.a2_10_1 = list.Sum(x => x.DonBien_A2_DienS);
                        ViewBag.a2_10_2 = list.Sum(x => x.DonBien_A2_DienT);

                        ViewBag.a3_11_1 = list.Sum(x => x.SongNgan_BTTM_A3_TongSoPhien);
                        ViewBag.a3_11_2 = list.Sum(x => x.SongNgan_BTTM_A3_TongSoPhien_Dut);
                        ViewBag.a3_12_1 = list.Sum(x => x.SongNgan_BTTM_A3_GoiCanh);
                        ViewBag.a3_12_2 = list.Sum(x => x.SongNgan_BTTM_A3_GoiCanh_Dut);
                        ViewBag.a3_13_1 = list.Sum(x => x.SongNgan_BTTM_A3_TraLoiCanh);
                        ViewBag.a3_13_2 = list.Sum(x => x.SongNgan_BTTM_A3_TraLoiCanh_Dut);
                        ViewBag.a3_14_1 = list.Sum(x => x.SongNgan_BTTM_A3_DienS);
                        ViewBag.a3_14_2 = list.Sum(x => x.SongNgan_BTTM_A3_DienR);
                        ViewBag.a3_15 = list.Sum(x => x.SongNgan_NoiBo_A3_SoDoiTuong);
                        ViewBag.a3_16_1 = list.Sum(x => x.SongNgan_NoiBo_A3_TongSoPhien);
                        ViewBag.a3_16_2 = list.Sum(x => x.SongNgan_NoiBo_A3_TongSoPhien_Dut);
                        ViewBag.a3_17_1 = list.Sum(x => x.SongNgan_NoiBo_A3_SoPhienCT);
                        ViewBag.a3_17_2 = list.Sum(x => x.SongNgan_NoiBo_A3_SoPhienCT_Dut);
                        ViewBag.a3_18_1 = list.Sum(x => x.SongNgan_NoiBo_A3_TraLoiCanh);
                        ViewBag.a3_18_2 = list.Sum(x => x.SongNgan_NoiBo_A3_TraLoiCanh_Dut);
                        ViewBag.a3_19_1 = list.Sum(x => x.SongNgan_NoiBo_A3_GoiCanh);
                        ViewBag.a3_19_2 = list.Sum(x => x.SongNgan_NoiBo_A3_GoiCanh_Dut);
                        ViewBag.a3_20_1 = list.Sum(x => x.SongNgan_NoiBo_A3_DienS);
                        ViewBag.a3_20_2 = list.Sum(x => x.SongNgan_NoiBo_A3_DienR);
                        ViewBag.a3_21_1 = list.Sum(x => x.SongNgan_NoiBo_A3_DienDong);
                        ViewBag.a3_22_1 = list.Sum(x => x.SongNgan_HiepDong_A3_TraLoiCanh);
                        ViewBag.a3_22_2 = list.Sum(x => x.SongNgan_HiepDong_A3_TraLoiCanh_Dut);
                        ViewBag.a3_23_1 = list.Sum(x => x.SongNgan_HiepDong_A3_DienS);
                        ViewBag.a3_23_2 = list.Sum(x => x.SongNgan_HiepDong_A3_DienR);

                        ViewBag.a4_24 = list.Sum(x => x.SCN_A4_SoDoiTuong);
                        ViewBag.a4_25_1 = list.Sum(x => x.SCN_A4_TongSoPhien);
                        ViewBag.a4_25_2 = list.Sum(x => x.SCN_A4_TongSoPhien_Dut);
                        ViewBag.a4_26_1 = list.Sum(x => x.SCN_A4_SoPhienCT);
                        ViewBag.a4_26_2 = list.Sum(x => x.SCN_A4_SoPhienCT_Dut);
                        ViewBag.a4_27_1 = list.Sum(x => x.SCN_A4_DienS);
                        ViewBag.a4_27_2 = list.Sum(x => x.SCN_A4_DienR);

                        ViewBag.a6_31 = list.Sum(x => x.ViBa_A6_SoDoiTuong);
                        ViewBag.a6_32_1 = list.Sum(x => x.ViBa_A6_GioLienLac);
                        ViewBag.a6_32_2 = list.Sum(x => x.ViBa_A6_GioPhatLienLac);
                        ViewBag.a6_33_1 = list.Sum(x => x.ViBa_A6_GioPhat);
                        ViewBag.a6_33_2 = list.Sum(x => x.ViBa_A6_GioPhat_Dut);

                        ViewBag.b2_36 = list.Sum(x => x.HDT_B2_SoDoiTuong);
                        ViewBag.b2_37 = list.Sum(x => x.HDT_B2_LanChuyenTiep);
                        ViewBag.b2_38 = list.Sum(x => x.HDT_B2_SoLanDut);
                        ViewBag.b2_39 = list.Sum(x => x.HDT_B2_DayTieuHao);
                        ViewBag.b2_40_1 = list.Sum(x => x.HDT_B2_GioLienLac);
                        ViewBag.b2_40_2 = list.Sum(x => x.HDT_B2_PhutLienLac);

                        ViewBag.b3_41 = list.Sum(x => x.HTD_B3_MayDung);
                        ViewBag.b3_42 = list.Sum(x => x.HTD_B3_MaySua);
                        ViewBag.b3_43 = list.Sum(x => x.HTD_B3_MayDong);
                        ViewBag.b3_44 = list.Sum(x => x.HTD_B3_DatMayMoi);
                        ViewBag.b3_45_1 = list.Sum(x => x.HTD_B3_TongSoTrKe);
                        ViewBag.b3_45_2 = list.Sum(x => x.HTD_B3_TongSoTrKe_Dut);
                        ViewBag.b3_46_1 = list.Sum(x => x.HTD_B3_TongSoTrKeNB);
                        ViewBag.b3_46_2 = list.Sum(x => x.HTD_B3_TongSoTrKeNB_Dut);

                        ViewBag.b4_47_1 = list.Sum(x => x.QuanBuu_47_B4_TongVanHanh);
                        ViewBag.b4_47_2 = list.Sum(x => x.QuanBuu_47_B4_TongVanHanh_Dut);
                        ViewBag.b4_48 = list.Sum(x => x.QuanBuu_HoaToc_B4_Ve);
                        ViewBag.b4_49 = list.Sum(x => x.QuanBuu_HoaToc_B4_Di);
                        ViewBag.b4_50 = list.Sum(x => x.QuanBuu_HoaToc_B4_Dong);
                        ViewBag.b4_51 = list.Sum(x => x.QuanBuu_CongVan_B4_Ve);
                        ViewBag.b4_52 = list.Sum(x => x.QuanBuu_CongVan_B4_Di);
                        ViewBag.b4_53 = list.Sum(x => x.QuanBuu_CongVan_B4_Dong);
                        ViewBag.b4_54 = list.Sum(x => x.QuanBuu_VanKien_B4_Ve);
                        ViewBag.b4_55 = list.Sum(x => x.QuanBuu_VanKien_B4_Di);
                        ViewBag.b4_56 = list.Sum(x => x.QuanBuu_VanKien_B4_Dong);
                        ViewBag.b4_57 = list.Sum(x => x.QuanBuu_ThuBao_B4_Ve);
                        ViewBag.b4_58 = list.Sum(x => x.QuanBuu_ThuBao_B4_Di);
                        ViewBag.b4_59 = list.Sum(x => x.QuanBuu_ThuBao_B4_Dong);
                        ViewBag.b4_60 = list.Sum(x => x.XeDap);
                        ViewBag.b4_61 = list.Sum(x => x.MoTo);
                        ViewBag.b4_62 = list.Sum(x => x.OTo);
                        ViewBag.b4_63 = list.Sum(x => x.SoChuyen);
                        ViewBag.b4_64 = list.Sum(x => x.TrongLuong);
                        ViewBag.b4_65 = list.Sum(x => x.CuLy);



                        return View(list);

                    }
                    catch (Exception)
                    {
                        ngaybaocao = new DateTime(2020, 3, 11);
                        //var list = db.BaoCaoTongHop().ToList().Where(x => x.NgayBaoCao.Value.Year == ngaybaocao.Year && x.NgayBaoCao.Value.Month
                        //== ngaybaocao.Month && x.NgayBaoCao.Value.Day == ngaybaocao.Day && x.IsGui == true).ToList();
                        //var list = db.BaoCaoTongHop(ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();
                        var list = db.BaoCaoTongHopHuyen(donviID, ngaybaocao.Day, ngaybaocao.Month, ngaybaocao.Year).Where(x => x.IsGui == true).ToList();
                        return View(list);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: BaoCaoNgays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCaoNgay baoCaoNgay = db.BaoCaoNgay.Find(id);
            if (baoCaoNgay == null)
            {
                return HttpNotFound();
            }
            return View(baoCaoNgay);
        }

        // GET: BaoCaoNgays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BaoCaoNgays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BaoCaoID,NguoiBaoCao,NgayBaoCao,DonViID,GhiChu")] BaoCaoNgay baoCaoNgay)
        {
            if (ModelState.IsValid)
            {
                db.BaoCaoNgay.Add(baoCaoNgay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baoCaoNgay);
        }

        // GET: BaoCaoNgays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCaoNgay baoCaoNgay = db.BaoCaoNgay.Find(id);
           
            if (baoCaoNgay == null)
            {
                return HttpNotFound();
            }
            return View(baoCaoNgay);
        }

        // POST: BaoCaoNgays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BaoCaoID,NguoiBaoCao,NgayBaoCao,DonViID,GhiChu")] BaoCaoNgay baoCaoNgay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baoCaoNgay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baoCaoNgay);
        }

        // GET: BaoCaoNgays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCaoNgay baoCaoNgay = db.BaoCaoNgay.Find(id);
            if (baoCaoNgay == null)
            {
                return HttpNotFound();
            }
            return View(baoCaoNgay);
        }

        // POST: BaoCaoNgays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaoCaoNgay baoCaoNgay = db.BaoCaoNgay.Find(id);
            db.BaoCaoNgay.Remove(baoCaoNgay);
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
