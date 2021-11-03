USE [TTQK7]
GO

/****** Object:  StoredProcedure [dbo].[sp_Thanh_BaoCaoThang_DonVi]    Script Date: 5/14/2021 11:30:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_Thanh_BaoCaoThang_DonVi]
@thang int, @nam int, @idDonVi int
as 
begin
	declare @Test table (NgayBaoCao smalldatetime,
						TBBD_A1_CanhBTTM int,  TBBD_A1_CanhBTTM_Dut int,
                          TBBD_A1_CanhQK int,  TBBD_A1_CanhQK_Dut int,  TBBD_A1_Lanphatchuan int,  TBBD_A1_ThuTinHieuBo int, 
                          TBBD_A1_ThuTinHieuQK int,  TBBD_A1_QKPhatTinHieu int, 
						  DonBien_A2_SoDoiTuong int,  DonBien_A2_TongSoPhien int,  DonBien_A2_TongSoPhien_Dut int,  DonBien_A2_SoPhienCT int, 
                          DonBien_A2_SoPhienCT_Dut int,  DonBien_A2_DienS int,  DonBien_A2_DienT int, 
                          SongNgan_BTTM_A3_TongSoPhien int,  SongNgan_BTTM_A3_TongSoPhien_Dut int, 
                          SongNgan_BTTM_A3_GoiCanh int,  SongNgan_BTTM_A3_GoiCanh_Dut int,  SongNgan_BTTM_A3_TraLoiCanh int, 
                          SongNgan_BTTM_A3_TraLoiCanh_Dut int,  SongNgan_BTTM_A3_DienS int,  SongNgan_BTTM_A3_DienR int, 
                          SongNgan_NoiBo_A3_SoDoiTuong int,  SongNgan_NoiBo_A3_TongSoPhien int, 
                          SongNgan_NoiBo_A3_TongSoPhien_Dut int,  SongNgan_NoiBo_A3_SoPhienCT int, 
                          SongNgan_NoiBo_A3_SoPhienCT_Dut int,  SongNgan_NoiBo_A3_TraLoiCanh int, 
                          SongNgan_NoiBo_A3_TraLoiCanh_Dut int,  SongNgan_NoiBo_A3_GoiCanh int, 
                          SongNgan_NoiBo_A3_GoiCanh_Dut int,  SongNgan_NoiBo_A3_DienS int,  SongNgan_NoiBo_A3_DienR int, 
                          SongNgan_NoiBo_A3_DienDong int, 
						  SongNgan_HiepDong_A3_TraLoiCanh int, 
						  SongNgan_HiepDong_A3_TraLoiCanh_Dut int,  SongNgan_HiepDong_A3_DienS int, 
						  SongNgan_HiepDong_A3_DienR int, 
						  SCN_A4_SoDoiTuong int,  SCN_A4_TongSoPhien int,  SCN_A4_TongSoPhien_Dut int, 
						  SCN_A4_SoPhienCT int,  SCN_A4_SoPhienCT_Dut int,  SCN_A4_DienS int,  SCN_A4_DienR int, 
						  ViBa_A6_SoDoiTuong int,  ViBa_A6_GioLienLac int,  ViBa_A6_GioPhatLienLac int, 
						  ViBa_A6_GioPhat int,  ViBa_A6_GioPhat_Dut int,
						  HDT_B2_SoDoiTuong int,  HDT_B2_LanChuyenTiep int, 
						  HDT_B2_SoLanDut int,  HDT_B2_DayTieuHao int,  HDT_B2_GioLienLac int,  HDT_B2_PhutLienLac int, 
                          HTD_B3_MayDung int,  HTD_B3_MaySua int,  HTD_B3_MayDong int,  HTD_B3_DatMayMoi int, 
						  HTD_B3_TongSoTrKe int,  HTD_B3_TongSoTrKe_Dut int,  HTD_B3_TongSoTrKeNB int, 
						  HTD_B3_TongSoTrKeNB_Dut int, 
						  QuanBuu_47_B4_TongVanHanh int,  QuanBuu_47_B4_TongVanHanh_Dut int, 
                          QuanBuu_HoaToc_B4_Ve int,  QuanBuu_HoaToc_B4_Di int,  QuanBuu_HoaToc_B4_Dong int, 
                          QuanBuu_CongVan_B4_Ve int,  QuanBuu_CongVan_B4_Di int,  QuanBuu_CongVan_B4_Dong int, 
                          QuanBuu_VanKien_B4_Ve int,  QuanBuu_VanKien_B4_Di int,  QuanBuu_VanKien_B4_Dong int, 
                          QuanBuu_ThuBao_B4_Ve int,  QuanBuu_ThuBao_B4_Di int,  QuanBuu_ThuBao_B4_Dong int,  
                           QuanBuu_PhuongTien_B4_XeDap int,  QuanBuu_PhuongTien_B4_MoTo int,  QuanBuu_PhuongTien_B4_OTo int,  QuanBuu_PhuongTien_B4_SoChuyen int,  QuanBuu_PhuongTien_B4_TrongLuong int,  QuanBuu_PhuongTien_B4_CuLy int,
						   GhiChu nvarchar(500) null
	)
	insert into @Test
	select t.* 
	from
	(
	--ghi chú chung
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong,  
                          0 as QuanBuu_PhuongTien_B4_XeDap, 0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.BaoCaoNgay.GhiChu IS NOT NULL, N'Ghi chú chung: ' + dbo.BaoCaoNgay.GhiChu,'') as GhiChu
	from dbo.BaoCaoNgay
	where dbo.BaoCaoNgay.DonViID = @idDonVi
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	--bang A1
	union all
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, dbo.TBBD_A1.CanhBTTM AS TBBD_A1_CanhBTTM, dbo.TBBD_A1.CanhBTTM_Dut AS TBBD_A1_CanhBTTM_Dut,
                         dbo.TBBD_A1.CanhQK AS TBBD_A1_CanhQK, dbo.TBBD_A1.CanhQK_Dut AS TBBD_A1_CanhQK_Dut, dbo.TBBD_A1.Lanphatchuan AS TBBD_A1_Lanphatchuan, 
						 iif(dbo.TBBD_A1.ThuTinHieuBo=0,1,0) AS TBBD_A1_ThuTinHieuBo, 
                         iif(dbo.TBBD_A1.ThuTinHieuQK=0,1,0) AS TBBD_A1_ThuTinHieuQK, 
						 iif(dbo.TBBD_A1.QKPhatTinHieu=0,1,0) AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
                         0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
                         0 AS SongNgan_HiepDong_A3_DienR, 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 
                         0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
                         0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.TBBD_A1.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.TBBD_A1.GhiChu,'') as GhiChu
	from dbo.TBBD_A1, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.TBBD_A1.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang 
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--bảng A2
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						dbo.DonBien_A2.SoDoiTuong AS DonBien_A2_SoDoiTuong, dbo.DonBien_A2.TongSoPhien AS DonBien_A2_TongSoPhien, dbo.DonBien_A2.TongSoPhien_Dut AS DonBien_A2_TongSoPhien_Dut, dbo.DonBien_A2.SoPhienCT AS DonBien_A2_SoPhienCT, 
                         dbo.DonBien_A2.SoPhienCT_Dut AS DonBien_A2_SoPhienCT_Dut, dbo.DonBien_A2.DienS AS DonBien_A2_DienS, dbo.DonBien_A2.DienT AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
                         0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
                         0 AS SongNgan_HiepDong_A3_DienR, 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 
                         0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
                         0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.DonBien_A2.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.DonBien_A2.GhiChu,'') as GhiChu
	from dbo.DonBien_A2, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.DonBien_A2.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--bảng A3-BTTM
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         dbo.SongNgan_BTTM_A3.TongSoPhien AS SongNgan_BTTM_A3_TongSoPhien, dbo.SongNgan_BTTM_A3.TongSoPhien_Dut AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         dbo.SongNgan_BTTM_A3.GoiCanh AS SongNgan_BTTM_A3_GoiCanh, dbo.SongNgan_BTTM_A3.GoiCanh_Dut AS SongNgan_BTTM_A3_GoiCanh_Dut, dbo.SongNgan_BTTM_A3.TraLoiCanh AS SongNgan_BTTM_A3_TraLoiCanh, 
                         dbo.SongNgan_BTTM_A3.TraLoiCanh_Dut AS SongNgan_BTTM_A3_TraLoiCanh_Dut, dbo.SongNgan_BTTM_A3.DienS AS SongNgan_BTTM_A3_DienS, dbo.SongNgan_BTTM_A3.DienR AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
                         0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
                         0 AS SongNgan_HiepDong_A3_DienR, 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 
                         0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
                         0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.SongNgan_BTTM_A3.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.SongNgan_BTTM_A3.GhiChu,'') as GhiChu
	from dbo.SongNgan_BTTM_A3, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.SongNgan_BTTM_A3.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--bảng A3-Noi Bo
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         dbo.SongNgan_NoiBo_A3.SoDoiTuong AS SongNgan_NoiBo_A3_SoDoiTuong, dbo.SongNgan_NoiBo_A3.TongSoPhien AS SongNgan_NoiBo_A3_TongSoPhien, 
                         dbo.SongNgan_NoiBo_A3.TongSoPhien_Dut AS SongNgan_NoiBo_A3_TongSoPhien_Dut, dbo.SongNgan_NoiBo_A3.SoPhienCT AS SongNgan_NoiBo_A3_SoPhienCT, 
                         dbo.SongNgan_NoiBo_A3.SoPhienCT_Dut AS SongNgan_NoiBo_A3_SoPhienCT_Dut, dbo.SongNgan_NoiBo_A3.TraLoiCanh AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         dbo.SongNgan_NoiBo_A3.TraLoiCanh_Dut AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, dbo.SongNgan_NoiBo_A3.GoiCanh AS SongNgan_NoiBo_A3_GoiCanh, 
                         dbo.SongNgan_NoiBo_A3.GoiCanh_Dut AS SongNgan_NoiBo_A3_GoiCanh_Dut, dbo.SongNgan_NoiBo_A3.DienS AS SongNgan_NoiBo_A3_DienS, dbo.SongNgan_NoiBo_A3.DienR AS SongNgan_NoiBo_A3_DienR, 
                         dbo.SongNgan_NoiBo_A3.DienDong AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
                         0 AS SongNgan_HiepDong_A3_DienR, 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.SongNgan_NoiBo_A3.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.SongNgan_NoiBo_A3.GhiChu,'') as GhiChu
	from dbo.SongNgan_NoiBo_A3, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.SongNgan_NoiBo_A3.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--A3-HiepDong
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 dbo.SongNgan_HiepDong_A3.TraLoiCanh AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 dbo.SongNgan_HiepDong_A3.TraLoiCanh_Dut AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, dbo.SongNgan_HiepDong_A3.DienS AS SongNgan_HiepDong_A3_DienS, 
						 dbo.SongNgan_HiepDong_A3.DienR AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.SongNgan_HiepDong_A3.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.SongNgan_HiepDong_A3.GhiChu,'') as GhiChu
	from dbo.SongNgan_HiepDong_A3, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.SongNgan_HiepDong_A3.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--A4
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 dbo.SCN_A4.SoDoiTuong AS SCN_A4_SoDoiTuong, dbo.SCN_A4.TongSoPhien AS SCN_A4_TongSoPhien, dbo.SCN_A4.TongSoPhien_Dut AS SCN_A4_TongSoPhien_Dut, 
						 dbo.SCN_A4.SoPhienCT AS SCN_A4_SoPhienCT, dbo.SCN_A4.SoPhienCT_Dut AS SCN_A4_SoPhienCT_Dut, dbo.SCN_A4.DienS AS SCN_A4_DienS, dbo.SCN_A4.DienR AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.SCN_A4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.SCN_A4.GhiChu,'') as GhiChu
	from dbo.SCN_A4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.SCN_A4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--Viba-A6
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 dbo.ViBa_A6.SoDoiTuong AS ViBa_A6_SoDoiTuong, dbo.ViBa_A6.GioLienLac AS ViBa_A6_GioLienLac, dbo.ViBa_A6.GioPhatLienLac AS ViBa_A6_GioPhatLienLac, 
						 dbo.ViBa_A6.GioPhat AS ViBa_A6_GioPhat, dbo.ViBa_A6.GioPhat_Dut AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.ViBa_A6.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.ViBa_A6.GhiChu,'') as GhiChu
	from dbo.ViBa_A6, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.ViBa_A6.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--HDT-B2
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 dbo.HDT_B2.SoDoiTuong AS HDT_B2_SoDoiTuong, dbo.HDT_B2.LanChuyenTiep AS HDT_B2_LanChuyenTiep, 
						 dbo.HDT_B2.SoLanDut AS HDT_B2_SoLanDut, dbo.HDT_B2.DayTieuHao AS HDT_B2_DayTieuHao, dbo.HDT_B2.GioLienLac AS HDT_B2_GioLienLac, dbo.HDT_B2.PhutLienLac AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.HDT_B2.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.HDT_B2.GhiChu,'') as GhiChu
	from dbo.HDT_B2, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.HDT_B2.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--HDT-A3
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         dbo.HTD_B3.MayDung AS HTD_B3_MayDung, dbo.HTD_B3.MaySua AS HTD_B3_MaySua, dbo.HTD_B3.MayDong AS HTD_B3_MayDong, dbo.HTD_B3.DatMayMoi AS HTD_B3_DatMayMoi, 
						 dbo.HTD_B3.TongSoTrKe AS HTD_B3_TongSoTrKe, dbo.HTD_B3.TongSoTrKe_Dut AS HTD_B3_TongSoTrKe_Dut, dbo.HTD_B3.TongSoTrKeNB AS HTD_B3_TongSoTrKeNB, 
						 dbo.HTD_B3.TongSoTrKeNB_Dut AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.HTD_B3.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.HTD_B3.GhiChu,'') as GhiChu
	from dbo.HTD_B3, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.HTD_B3.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quan buu 47_B4
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 dbo.QuanBuu_47_B4.TongVanHanh AS QuanBuu_47_B4_TongVanHanh, dbo.QuanBuu_47_B4.TongVanHanh_Dut AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_47_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_47_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_47_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_47_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quan buu hoa toc
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         dbo.QuanBuu_HoaToc_B4.Ve AS QuanBuu_HoaToc_B4_Ve, dbo.QuanBuu_HoaToc_B4.Di AS QuanBuu_HoaToc_B4_Di, dbo.QuanBuu_HoaToc_B4.Dong AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_HoaToc_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_HoaToc_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_HoaToc_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_HoaToc_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quân bưu cong van
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         dbo.QuanBuu_CongVan_B4.Ve AS QuanBuu_CongVan_B4_Ve, dbo.QuanBuu_CongVan_B4.Di AS QuanBuu_CongVan_B4_Di, dbo.QuanBuu_CongVan_B4.Dong AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_CongVan_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_CongVan_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_CongVan_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_CongVan_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quân bưu văn kiện
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         dbo.QuanBuu_VanKien_B4.Ve AS QuanBuu_VanKien_B4_Ve, dbo.QuanBuu_VanKien_B4.Di AS QuanBuu_VanKien_B4_Di, dbo.QuanBuu_VanKien_B4.Dong AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 0 as QuanBuu_PhuongTien_B4_XeDap, 
                         0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_VanKien_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_VanKien_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_VanKien_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_VanKien_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quân bưu thư báo
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0 AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
						 0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
						 0 AS SongNgan_HiepDong_A3_DienR, 
						 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 0 AS SCN_A4_TongSoPhien_Dut, 
						 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 0 AS SCN_A4_DienR, 
						 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
						 0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut,
						 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
						 0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
						 0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
						 0 AS HTD_B3_TongSoTrKeNB_Dut, 
						 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         dbo.QuanBuu_ThuBao_B4.Ve AS QuanBuu_ThuBao_B4_Ve, dbo.QuanBuu_ThuBao_B4.Di AS QuanBuu_ThuBao_B4_Di, dbo.QuanBuu_ThuBao_B4.Dong AS QuanBuu_ThuBao_B4_Dong,  
                         0 as QuanBuu_PhuongTien_B4_XeDap, 0 as QuanBuu_PhuongTien_B4_MoTo, 0 as QuanBuu_PhuongTien_B4_OTo, 0 as QuanBuu_PhuongTien_B4_SoChuyen, 0 as QuanBuu_PhuongTien_B4_TrongLuong, 0 as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_ThuBao_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_ThuBao_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_ThuBao_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_ThuBao_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	--quân bưu phuong tiện
	union all 
	SELECT   dbo.BaoCaoNgay.NgayBaoCao, 0AS TBBD_A1_CanhBTTM, 0 AS TBBD_A1_CanhBTTM_Dut,
                         0 AS TBBD_A1_CanhQK, 0 AS TBBD_A1_CanhQK_Dut, 0 AS TBBD_A1_Lanphatchuan, 0 AS TBBD_A1_ThuTinHieuBo, 
                         0 AS TBBD_A1_ThuTinHieuQK, 0 AS TBBD_A1_QKPhatTinHieu, 
						 0 AS DonBien_A2_SoDoiTuong, 0 AS DonBien_A2_TongSoPhien, 0 AS DonBien_A2_TongSoPhien_Dut, 0 AS DonBien_A2_SoPhienCT, 
                         0 AS DonBien_A2_SoPhienCT_Dut, 0 AS DonBien_A2_DienS, 0 AS DonBien_A2_DienT, 
                         0 AS SongNgan_BTTM_A3_TongSoPhien, 0 AS SongNgan_BTTM_A3_TongSoPhien_Dut, 
                         0 AS SongNgan_BTTM_A3_GoiCanh, 0 AS SongNgan_BTTM_A3_GoiCanh_Dut, 0 AS SongNgan_BTTM_A3_TraLoiCanh, 
                         0 AS SongNgan_BTTM_A3_TraLoiCanh_Dut, 0 AS SongNgan_BTTM_A3_DienS, 0 AS SongNgan_BTTM_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_SoDoiTuong, 0 AS SongNgan_NoiBo_A3_TongSoPhien, 
                         0 AS SongNgan_NoiBo_A3_TongSoPhien_Dut, 0 AS SongNgan_NoiBo_A3_SoPhienCT, 
                         0 AS SongNgan_NoiBo_A3_SoPhienCT_Dut, 0 AS SongNgan_NoiBo_A3_TraLoiCanh, 
                         0 AS SongNgan_NoiBo_A3_TraLoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_GoiCanh, 
                         0 AS SongNgan_NoiBo_A3_GoiCanh_Dut, 0 AS SongNgan_NoiBo_A3_DienS, 0 AS SongNgan_NoiBo_A3_DienR, 
                         0 AS SongNgan_NoiBo_A3_DienDong, 0 AS SongNgan_HiepDong_A3_TraLoiCanh, 
                         0 AS SongNgan_HiepDong_A3_TraLoiCanh_Dut, 0 AS SongNgan_HiepDong_A3_DienS, 
                         0 AS SongNgan_HiepDong_A3_DienR, 0 AS SCN_A4_SoDoiTuong, 0 AS SCN_A4_TongSoPhien, 
                         0 AS SCN_A4_TongSoPhien_Dut, 0 AS SCN_A4_SoPhienCT, 0 AS SCN_A4_SoPhienCT_Dut, 0 AS SCN_A4_DienS, 
                         0 AS SCN_A4_DienR, 0 AS ViBa_A6_SoDoiTuong, 0 AS ViBa_A6_GioLienLac, 0 AS ViBa_A6_GioPhatLienLac, 
                         0 AS ViBa_A6_GioPhat, 0 AS ViBa_A6_GioPhat_Dut, 0 AS HDT_B2_SoDoiTuong, 0 AS HDT_B2_LanChuyenTiep, 
                         0 AS HDT_B2_SoLanDut, 0 AS HDT_B2_DayTieuHao, 0 AS HDT_B2_GioLienLac, 0 AS HDT_B2_PhutLienLac, 
                         0 AS HTD_B3_MayDung, 0 AS HTD_B3_MaySua, 0 AS HTD_B3_MayDong, 0 AS HTD_B3_DatMayMoi, 
                         0 AS HTD_B3_TongSoTrKe, 0 AS HTD_B3_TongSoTrKe_Dut, 0 AS HTD_B3_TongSoTrKeNB, 
                         0 AS HTD_B3_TongSoTrKeNB_Dut, 0 AS QuanBuu_47_B4_TongVanHanh, 0 AS QuanBuu_47_B4_TongVanHanh_Dut, 
                         0 AS QuanBuu_HoaToc_B4_Ve, 0 AS QuanBuu_HoaToc_B4_Di, 0 AS QuanBuu_HoaToc_B4_Dong, 
                         0 AS QuanBuu_CongVan_B4_Ve, 0 AS QuanBuu_CongVan_B4_Di, 0 AS QuanBuu_CongVan_B4_Dong, 
                         0 AS QuanBuu_VanKien_B4_Ve, 0 AS QuanBuu_VanKien_B4_Di, 0 AS QuanBuu_VanKien_B4_Dong, 
                         0 AS QuanBuu_ThuBao_B4_Ve, 0 AS QuanBuu_ThuBao_B4_Di, 0 AS QuanBuu_ThuBao_B4_Dong, 
						 dbo.QuanBuu_PhuongTien_B4.XeDap as QuanBuu_PhuongTien_B4_XeDap, 
                         dbo.QuanBuu_PhuongTien_B4.MoTo as QuanBuu_PhuongTien_B4_MoTo, dbo.QuanBuu_PhuongTien_B4.OTo as QuanBuu_PhuongTien_B4_OTo, dbo.QuanBuu_PhuongTien_B4.SoChuyen as QuanBuu_PhuongTien_B4_SoChuyen, dbo.QuanBuu_PhuongTien_B4.TrongLuong as QuanBuu_PhuongTien_B4_TrongLuong, dbo.QuanBuu_PhuongTien_B4.CuLy as QuanBuu_PhuongTien_B4_CuLy,
						 IIF(dbo.QuanBuu_PhuongTien_B4.GhiChu IS NOT NULL, dbo.tblDonVi.TenDonVi + ': ' + dbo.QuanBuu_PhuongTien_B4.GhiChu,'') as GhiChu
	from dbo.QuanBuu_PhuongTien_B4, dbo.BaoCaoNgay, dbo.tblDonVi
	where dbo.QuanBuu_PhuongTien_B4.BaoCaoID = dbo.BaoCaoNgay.BaoCaoID
	and dbo.BaoCaoNgay.DonViID = dbo.tblDonVi.DonViID
	and month(dbo.BaoCaoNgay.NgayBaoCao)= @thang
	and year(dbo.BaoCaoNgay.NgayBaoCao)= @nam
	and dbo.tblDonVi.DonViID =@idDonVi
	) as t
--format lại dữ liệu cho chuẩn
update @Test 
set GhiChu = NULL
where GhiChu =''
update @Test
set NgayBaoCao = cast(NgayBaoCao as date)
--set NgayBaoCao = datefromparts(year(NgayBaoCao), month(NgayBaoCao), day(NgayBaoCao))
--bắt đầu tính tổng
select NgayBaoCao, sum( TBBD_A1_CanhBTTM) as  TBBD_A1_CanhBTTM, 
sum(TBBD_A1_CanhBTTM_Dut) as TBBD_A1_CanhBTTM_Dut, 
sum(TBBD_A1_CanhQK) as TBBD_A1_CanhQK, 
sum(TBBD_A1_CanhQK_Dut) as TBBD_A1_CanhQK_Dut, 
sum(TBBD_A1_Lanphatchuan) as TBBD_A1_Lanphatchuan, 
sum(TBBD_A1_ThuTinHieuBo) as TBBD_A1_ThuTinHieuBo, 
sum(TBBD_A1_ThuTinHieuQK) as TBBD_A1_ThuTinHieuQK, 
sum(TBBD_A1_QKPhatTinHieu) as TBBD_A1_QKPhatTinHieu, 
sum( DonBien_A2_SoDoiTuong) as  DonBien_A2_SoDoiTuong, 
sum(DonBien_A2_TongSoPhien) as DonBien_A2_TongSoPhien, 
sum(DonBien_A2_TongSoPhien_Dut) as DonBien_A2_TongSoPhien_Dut, 
sum(DonBien_A2_SoPhienCT) as DonBien_A2_SoPhienCT, 
sum(DonBien_A2_SoPhienCT_Dut) as DonBien_A2_SoPhienCT_Dut, 
sum(DonBien_A2_DienS) as DonBien_A2_DienS, 
sum( DonBien_A2_DienT) as  DonBien_A2_DienT, 
sum(SongNgan_BTTM_A3_TongSoPhien) as SongNgan_BTTM_A3_TongSoPhien, 
sum(SongNgan_BTTM_A3_TongSoPhien_Dut) as SongNgan_BTTM_A3_TongSoPhien_Dut, 
sum(SongNgan_BTTM_A3_GoiCanh) as SongNgan_BTTM_A3_GoiCanh, 
sum(SongNgan_BTTM_A3_GoiCanh_Dut) as SongNgan_BTTM_A3_GoiCanh_Dut, 
sum(SongNgan_BTTM_A3_TraLoiCanh) as SongNgan_BTTM_A3_TraLoiCanh, 
sum(SongNgan_BTTM_A3_TraLoiCanh_Dut) as SongNgan_BTTM_A3_TraLoiCanh_Dut, 
sum(SongNgan_BTTM_A3_DienS) as SongNgan_BTTM_A3_DienS, 
sum(SongNgan_BTTM_A3_DienR) as SongNgan_BTTM_A3_DienR, 
sum(SongNgan_NoiBo_A3_SoDoiTuong) as SongNgan_NoiBo_A3_SoDoiTuong, 
sum(SongNgan_NoiBo_A3_TongSoPhien) as SongNgan_NoiBo_A3_TongSoPhien, 
sum(SongNgan_NoiBo_A3_TongSoPhien_Dut) as SongNgan_NoiBo_A3_TongSoPhien_Dut, 
sum(SongNgan_NoiBo_A3_SoPhienCT) as SongNgan_NoiBo_A3_SoPhienCT, 
sum(SongNgan_NoiBo_A3_SoPhienCT_Dut) as SongNgan_NoiBo_A3_SoPhienCT_Dut, 
sum(SongNgan_NoiBo_A3_TraLoiCanh) as SongNgan_NoiBo_A3_TraLoiCanh, 
sum(SongNgan_NoiBo_A3_TraLoiCanh_Dut) as SongNgan_NoiBo_A3_TraLoiCanh_Dut, 
sum(SongNgan_NoiBo_A3_GoiCanh) as SongNgan_NoiBo_A3_GoiCanh, 
sum(SongNgan_NoiBo_A3_GoiCanh_Dut) as SongNgan_NoiBo_A3_GoiCanh_Dut, 
sum(SongNgan_NoiBo_A3_DienS) as SongNgan_NoiBo_A3_DienS, 
sum(SongNgan_NoiBo_A3_DienR) as SongNgan_NoiBo_A3_DienR, 
sum(SongNgan_NoiBo_A3_DienDong) as SongNgan_NoiBo_A3_DienDong, 
sum( SongNgan_HiepDong_A3_TraLoiCanh) as  SongNgan_HiepDong_A3_TraLoiCanh, 
sum( SongNgan_HiepDong_A3_TraLoiCanh_Dut) as  SongNgan_HiepDong_A3_TraLoiCanh_Dut, 
sum(SongNgan_HiepDong_A3_DienS) as SongNgan_HiepDong_A3_DienS, 
sum( SongNgan_HiepDong_A3_DienR) as  SongNgan_HiepDong_A3_DienR, 
sum( SCN_A4_SoDoiTuong) as  SCN_A4_SoDoiTuong, 
sum(SCN_A4_TongSoPhien) as SCN_A4_TongSoPhien, 
sum(SCN_A4_TongSoPhien_Dut) as SCN_A4_TongSoPhien_Dut, 
sum( SCN_A4_SoPhienCT) as  SCN_A4_SoPhienCT, 
sum(SCN_A4_SoPhienCT_Dut) as SCN_A4_SoPhienCT_Dut, 
sum(SCN_A4_DienS) as SCN_A4_DienS, 
sum( SCN_A4_DienR) as  SCN_A4_DienR, 
sum( ViBa_A6_SoDoiTuong) as  ViBa_A6_SoDoiTuong, 
sum(ViBa_A6_GioLienLac) as ViBa_A6_GioLienLac, 
sum(ViBa_A6_GioPhatLienLac) as ViBa_A6_GioPhatLienLac, 
sum( ViBa_A6_GioPhat) as  ViBa_A6_GioPhat, 
sum(ViBa_A6_GioPhat_Dut) as ViBa_A6_GioPhat_Dut, 
sum( HDT_B2_SoDoiTuong) as  HDT_B2_SoDoiTuong, 
sum(HDT_B2_LanChuyenTiep) as HDT_B2_LanChuyenTiep, 
sum( HDT_B2_SoLanDut) as  HDT_B2_SoLanDut, 
sum(HDT_B2_DayTieuHao) as HDT_B2_DayTieuHao, 
sum(HDT_B2_GioLienLac) as HDT_B2_GioLienLac, 
sum(HDT_B2_PhutLienLac) as HDT_B2_PhutLienLac, 
sum(HTD_B3_MayDung) as HTD_B3_MayDung, 
sum( HTD_B3_MaySua) as  HTD_B3_MaySua, 
sum(HTD_B3_MayDong) as HTD_B3_MayDong, 
sum(HTD_B3_DatMayMoi) as HTD_B3_DatMayMoi, 
sum( HTD_B3_TongSoTrKe) as  HTD_B3_TongSoTrKe, 
sum(HTD_B3_TongSoTrKe_Dut) as HTD_B3_TongSoTrKe_Dut, 
sum(HTD_B3_TongSoTrKeNB) as HTD_B3_TongSoTrKeNB, 
sum( HTD_B3_TongSoTrKeNB_Dut) as  HTD_B3_TongSoTrKeNB_Dut, 
sum( QuanBuu_47_B4_TongVanHanh) as  QuanBuu_47_B4_TongVanHanh, 
sum( QuanBuu_47_B4_TongVanHanh_Dut) as  QuanBuu_47_B4_TongVanHanh_Dut, 
sum(QuanBuu_HoaToc_B4_Ve) as QuanBuu_HoaToc_B4_Ve, 
sum(QuanBuu_HoaToc_B4_Di) as QuanBuu_HoaToc_B4_Di, 
sum(QuanBuu_HoaToc_B4_Dong) as QuanBuu_HoaToc_B4_Dong, 
sum(QuanBuu_CongVan_B4_Ve) as QuanBuu_CongVan_B4_Ve, 
sum(QuanBuu_CongVan_B4_Di) as QuanBuu_CongVan_B4_Di, 
sum(QuanBuu_CongVan_B4_Dong) as QuanBuu_CongVan_B4_Dong, 
sum(QuanBuu_VanKien_B4_Ve) as QuanBuu_VanKien_B4_Ve, 
sum(QuanBuu_VanKien_B4_Di) as QuanBuu_VanKien_B4_Di, 
sum(QuanBuu_VanKien_B4_Dong) as QuanBuu_VanKien_B4_Dong, 
sum(QuanBuu_ThuBao_B4_Ve) as QuanBuu_ThuBao_B4_Ve, 
sum(QuanBuu_ThuBao_B4_Di) as QuanBuu_ThuBao_B4_Di, 
sum(QuanBuu_ThuBao_B4_Dong) as QuanBuu_ThuBao_B4_Dong, 
sum( QuanBuu_PhuongTien_B4_XeDap) as  QuanBuu_PhuongTien_B4_XeDap, 
sum(QuanBuu_PhuongTien_B4_MoTo) as QuanBuu_PhuongTien_B4_MoTo, 
sum(QuanBuu_PhuongTien_B4_OTo) as QuanBuu_PhuongTien_B4_OTo, 
sum(QuanBuu_PhuongTien_B4_SoChuyen) as QuanBuu_PhuongTien_B4_SoChuyen, 
sum(QuanBuu_PhuongTien_B4_TrongLuong) as QuanBuu_PhuongTien_B4_TrongLuong, 
sum(QuanBuu_PhuongTien_B4_CuLy) as QuanBuu_PhuongTien_B4_CuLy, 
STUFF((Select ', '+ GhiChu
from @Test T1
where T1.NgayBaoCao=T2.NgayBaoCao
FOR XML PATH('')),1,2,'') as GhiChu
from @Test T2
group by NgayBaoCao
end

GO


