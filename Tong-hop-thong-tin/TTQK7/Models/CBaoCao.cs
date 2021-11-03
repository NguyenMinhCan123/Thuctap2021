using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTQK7.Models
{
    public partial class CBaoCao
    {
        public int BaoCaoID { get; set; }

        public int A1_ID { get; set; }
        public Nullable<int> A1_CanhBTTM { get; set; }
        public Nullable<int> A1_CanhBTTM_Dut { get; set; }
        public Nullable<int> A1_CanhQK { get; set; }
        public Nullable<int> A1_CanhQK_Dut { get; set; }
        public Nullable<int> A1_Lanphatchuan { get; set; }
        public Nullable<int> A1_ThuTinHieuBo { get; set; }
        public Nullable<int> A1_ThuTinHieuQK { get; set; }
        public Nullable<int> A1_QKPhatTinHieu { get; set; }
        public string A1_GhiChu { get; set; }

        public int A2_ID { get; set; }
        public Nullable<int> A2_SoDoiTuong { get; set; }
        public Nullable<int> A2_TongSoPhien { get; set; }
        public Nullable<int> A2_TongSoPhien_Dut { get; set; }
        public Nullable<int> A2_SoPhienCT { get; set; }
        public Nullable<int> A2_SoPhienCT_Dut { get; set; }
        public Nullable<int> A2_DienS { get; set; }
        public Nullable<int> A2_DienT { get; set; }
        public string A2_GhiChu { get; set; }

        public int A3_BTTM_ID { get; set; }
        public Nullable<int> A3_BTTM_TongSoPhien { get; set; }
        public Nullable<int> A3_BTTM_TongSoPhien_Dut { get; set; }
        public Nullable<int> A3_BTTM_GoiCanh { get; set; }
        public Nullable<int> A3_BTTM_GoiCanh_Dut { get; set; }
        public Nullable<int> A3_BTTM_TraLoiCanh { get; set; }
        public Nullable<int> A3_BTTM_TraLoiCanh_Dut { get; set; }
        public Nullable<int> A3_BTTM_DienS { get; set; }
        public Nullable<int> A3_BTTM_DienR { get; set; }
        public string A3_BTTM_GhiChu { get; set; }

        public int A3_NoiBo_ID { get; set; }
        public Nullable<int> A3_NoiBo_SoDoiTuong { get; set; }
        public Nullable<int> A3_NoiBo_TongSoPhien { get; set; }
        public Nullable<int> A3_NoiBo_TongSoPhien_Dut { get; set; }
        public Nullable<int> A3_NoiBo_SoPhienCT { get; set; }
        public Nullable<int> A3_NoiBo_SoPhienCT_Dut { get; set; }
        public Nullable<int> A3_NoiBo_TraLoiCanh { get; set; }
        public Nullable<int> A3_NoiBo_TraLoiCanh_Dut { get; set; }
        public Nullable<int> A3_NoiBo_GoiCanh { get; set; }
        public Nullable<int> A3_NoiBo_GoiCanh_Dut { get; set; }
        public Nullable<int> A3_NoiBo_DienS { get; set; }
        public Nullable<int> A3_NoiBo_DienR { get; set; }
        public Nullable<int> A3_NoiBo_DienDong { get; set; }
        public string A3_NoiBo_GhiChu { get; set; }

        public int A3_HiepDong_ID { get; set; }
        public Nullable<int> A3_HiepDong_TraLoiCanh { get; set; }
        public Nullable<int> A3_HiepDong_TraLoiCanh_Dut { get; set; }
        public Nullable<int> A3_HiepDong_DienS { get; set; }
        public Nullable<int> A3_HiepDong_DienR { get; set; }
        public string A3_HiepDong_GhiChu { get; set; }

        public int A4_ID { get; set; }
        public Nullable<int> A4_SoDoiTuong { get; set; }
        public Nullable<int> A4_TongSoPhien { get; set; }
        public Nullable<int> A4_TongSoPhien_Dut { get; set; }
        public Nullable<int> A4_SoPhienCT { get; set; }
        public Nullable<int> A4_SoPhienCT_Dut { get; set; }
        public Nullable<int> A4_DienS { get; set; }
        public Nullable<int> A4_DienR { get; set; }
        public string A4_GhiChu { get; set; }

        public int A6_ID { get; set; }
        public Nullable<int> A6_SoDoiTuong { get; set; }
        public Nullable<int> A6_GioLienLac { get; set; }
        public Nullable<int> A6_GioPhatLienLac { get; set; }
        public Nullable<int> A6_GioPhat { get; set; }
        public Nullable<int> A6_GioPhat_Dut { get; set; }
        public string A6_GhiChu { get; set; }

        public int B2_ID { get; set; }
        public Nullable<int> B2_SoDoiTuong { get; set; }
        public Nullable<int> B2_LanChuyenTiep { get; set; }
        public Nullable<int> B2_SoLanDut { get; set; }
        public Nullable<int> B2_DayTieuHao { get; set; }
        public Nullable<int> B2_GioLienLac { get; set; }
        public Nullable<int> B2_PhutLienLac { get; set; }
        public string B2_GhiChu { get; set; }

        public int B3_ID { get; set; }
        public Nullable<int> B3_MayDung { get; set; }
        public Nullable<int> B3_MaySua { get; set; }
        public Nullable<int> B3_MayDong { get; set; }
        public Nullable<int> B3_DatMayMoi { get; set; }
        public Nullable<int> B3_TongSoTrKe { get; set; }
        public Nullable<int> B3_TongSoTrKe_Dut { get; set; }
        public Nullable<int> B3_TongSoTrKeNB { get; set; }
        public Nullable<int> B3_TongSoTrKeNB_Dut { get; set; }
        public string B3_GhiChu { get; set; }

        public int B4_47_ID { get; set; }
        public Nullable<int> B4_47_TongVanHanh { get; set; }
        public Nullable<int> B4_47_TongVanHanh_Dut { get; set; }
        public string B4_47_GhiChu { get; set; }

        public int B4_HoaToc_ID { get; set; }
        public Nullable<int> B4_HoaToc_Ve { get; set; }
        public Nullable<int> B4_HoaToc_Di { get; set; }
        public Nullable<int> B4_HoaToc_Dong { get; set; }
        public string B4_HoaToc_GhiChu { get; set; }

        public int B4_CongVan_ID { get; set; }
        public Nullable<int> B4_CongVan_Ve { get; set; }
        public Nullable<int> B4_CongVan_Di { get; set; }
        public Nullable<int> B4_CongVan_Dong { get; set; }
        public string B4_CongVan_GhiChu { get; set; }

        public int B4_VanKien_ID { get; set; }
        public Nullable<double> B4_VanKien_Ve { get; set; }
        public Nullable<double> B4_VanKien_Di { get; set; }
        public Nullable<double> B4_VanKien_Dong { get; set; }
        public string B4_VanKien_GhiChu { get; set; }

        public int B4_ThuBao_ID { get; set; }
        public Nullable<double> B4_ThuBao_Ve { get; set; }
        public Nullable<double> B4_ThuBao_Di { get; set; }
        public Nullable<double> B4_ThuBao_Dong { get; set; }
        public string B4_ThuBao_GhiChu { get; set; }

        public int B4_PhuongTien_ID { get; set; }
        public Nullable<int> B4_PhuongTien_XeDap { get; set; }
        public Nullable<int> B4_PhuongTien_MoTo { get; set; }
        public Nullable<int> B4_PhuongTien_OTo { get; set; }
        public Nullable<int> B4_PhuongTien_SoChuyen { get; set; }
        public Nullable<double> B4_PhuongTien_TrongLuong { get; set; }
        public Nullable<int> B4_PhuongTien_CuLy { get; set; }
        public string B4_PhuongTien_GhiChu { get; set; }

        public string GhiChuChung { get; set; }
        public string NguoiBaoCao { get; set; }
        public string TrucBanTruong { get; set; }
        public string TrucBanPho { get; set; }

        public DateTime NgayBaoCao { get; set; }
    }
}