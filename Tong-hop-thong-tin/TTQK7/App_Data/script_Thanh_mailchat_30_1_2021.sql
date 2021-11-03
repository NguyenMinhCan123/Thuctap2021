ALTER FUNCTION [dbo].[Lay_DSNguoiNhan]
(
	@idMail int
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @kq nvarchar(max)
	set @kq = ''
	--Khai báo biến @id, @title để lưu nội dung đọc
	
	DECLARE @hoten_chucvu nvarchar(200)


	DECLARE cursorNguoiNhan CURSOR FOR  -- khai báo con trỏ cursorNguoiNhan
	SELECT dbo.tblUser.FirstName + ' ' + dbo.tblUser.LastName + '(' + dbo.tblChucVu.TenChucVu + ') - ' + isnull(dbo.tblDonVi.TenDonVi,'')
	FROM     dbo.tblUser  LEFT OUTER JOIN
			dbo.tblChucVu ON dbo.tblUser.idChucVu = dbo.tblChucVu.idChucVu  LEFT OUTER JOIN   -- dữ liệu trỏ tới
			dbo.tblDonVi ON dbo.tblDonVi.DonViID = dbo.tblUser.idDonVi
	WHERE dbo.tblUser.idUser IN (SELECT idUser FROM tblMailUser WHERE idMail = @idMail)

	OPEN cursorNguoiNhan                -- Mở con trỏ

	FETCH NEXT FROM cursorNguoiNhan     -- Đọc dòng đầu tiên
		  INTO @hoten_chucvu

	WHILE @@FETCH_STATUS = 0          --vòng lặp WHILE khi đọc Cursor thành công
	BEGIN
		--In kết quả
		set @kq = @kq + char(13) + @hoten_chucvu

		FETCH NEXT FROM cursorNguoiNhan -- Đọc dòng tiếp
			  INTO @hoten_chucvu
	END

	CLOSE cursorNguoiNhan              -- Đóng Cursor
	DEALLOCATE cursorNguoiNhan         -- Giải phóng tài nguyên

	-- Return the result of the function
	if(@kq <>'')		
		set @kq =  substring(@kq,2, len(@kq)-1)
	
	RETURN @kq

END
go
CREATE PROCEDURE [dbo].[Get_DSNguoiNhan]
	@idMail int
AS
BEGIN
	select dbo.Lay_DSNguoiNhan(@idMail)
END

GO
CREATE PROCEDURE [dbo].[Get_ListMailInbox](@idUser int)	
AS
BEGIN
	SELECT dbo.tblMail.idMail, dbo.tblMail.Ngay, dbo.tblMail.TieuDe, dbo.tblMail.NoiDung, dbo.Lay_DSNguoiNhan(dbo.tblMail.idMail) as DSNguoiNhan, tblMailUser.Status
	FROM     dbo.tblMail, dbo.tblMailUser                   
	WHERE  dbo.tblMail.idMail = tblMailUser.idMail and dbo.tblMailUser.idUser = @idUser
END
GO