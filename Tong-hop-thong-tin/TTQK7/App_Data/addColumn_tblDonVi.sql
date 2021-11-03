--ngày 25/01/2021
--thêm cột ParentID và IsLast để quản lý theo phân cấp
alter table tblDonVi add ParentID int null, IsLast bit null
go
insert into tblDonVi values(N'Quân khu 7', null, 0)
go
update tblDonVi set IsLast = 0
go
