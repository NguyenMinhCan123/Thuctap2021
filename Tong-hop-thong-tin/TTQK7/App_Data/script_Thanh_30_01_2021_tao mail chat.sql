CREATE TABLE [dbo].[tblMail](
	[idMail] [int] IDENTITY(1,1) NOT NULL,
	[Ngay] [smalldatetime] NULL,
	[TieuDe] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[idUser] [int] NULL,
 CONSTRAINT [PK_tblMail] PRIMARY KEY CLUSTERED 
(
	[idMail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblMail]  WITH CHECK ADD  CONSTRAINT [FK_tblMail_tblUser] FOREIGN KEY([idUser])
REFERENCES [dbo].[tblUser] ([idUser])
GO

ALTER TABLE [dbo].[tblMail] CHECK CONSTRAINT [FK_tblMail_tblUser]
GO
go
CREATE TABLE [dbo].[tblMailFile](
	[idFile] [int] IDENTITY(1,1) NOT NULL,
	[idMail] [int] NULL,
	[TenFile] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblFileMail] PRIMARY KEY CLUSTERED 
(
	[idFile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblMailFile]  WITH CHECK ADD  CONSTRAINT [FK_tblMailFile_tblMail] FOREIGN KEY([idMail])
REFERENCES [dbo].[tblMail] ([idMail])
GO

ALTER TABLE [dbo].[tblMailFile] CHECK CONSTRAINT [FK_tblMailFile_tblMail]
GO
go
CREATE TABLE [dbo].[tblMailUser](
	[idMailUser] [int] IDENTITY(1,1) NOT NULL,
	[idMail] [int] NULL,
	[idUser] [int] NULL,
	[Status] [int] NULL CONSTRAINT [DF_tblMailUser_Status]  DEFAULT ((1)),
	[TraLoi] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblMailUser] PRIMARY KEY CLUSTERED 
(
	[idMailUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblMailUser]  WITH CHECK ADD  CONSTRAINT [FK_tblMailUser_tblMail] FOREIGN KEY([idMail])
REFERENCES [dbo].[tblMail] ([idMail])
GO

ALTER TABLE [dbo].[tblMailUser] CHECK CONSTRAINT [FK_tblMailUser_tblMail]
GO

ALTER TABLE [dbo].[tblMailUser]  WITH CHECK ADD  CONSTRAINT [FK_tblMailUser_tblUser] FOREIGN KEY([idUser])
REFERENCES [dbo].[tblUser] ([idUser])
GO

ALTER TABLE [dbo].[tblMailUser] CHECK CONSTRAINT [FK_tblMailUser_tblUser]
GO
CREATE PROCEDURE [dbo].[Get_ListMailSent](@idUser int)	
AS
BEGIN
	SELECT dbo.tblMail.idMail, dbo.tblMail.Ngay, dbo.tblMail.TieuDe, dbo.tblMail.NoiDung, dbo.Lay_DSNguoiNhan(dbo.tblMail.idMail) as DSNguoiNhan
	FROM     dbo.tblMail                   
	WHERE dbo.tblMail.idUser = @idUser
END
GO
CREATE FUNCTION [dbo].[Lay_DSNguoiNhan]
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
	SELECT dbo.tblUser.FirstName + ' ' + dbo.tblUser.LastName + '(' + dbo.tblChucVu.TenChucVu + ') - ' + dbo.tblDonVi.TenDonVi
	FROM     dbo.tblDonVi RIGHT OUTER JOIN
                  dbo.tblUser ON dbo.tblDonVi.DonViID = dbo.tblUser.idDonVi LEFT OUTER JOIN
                  dbo.tblChucVu ON dbo.tblUser.idChucVu = dbo.tblChucVu.idChucVu     -- dữ liệu trỏ tới
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
GO