USE [TTQK7]
GO

/****** Object:  Table [dbo].[tblUploadFile]    Script Date: 1/27/2021 3:48:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUploadFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NameFile] [nvarchar](50) NULL,
	[NgayTao] [datetime] NULL,
	[DonViID] [int] NULL,
 CONSTRAINT [PK_UploadFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblUploadFile]  WITH CHECK ADD  CONSTRAINT [FK_tblUploadFile_tblDonVi] FOREIGN KEY([DonViID])
REFERENCES [dbo].[tblDonVi] ([DonViID])
GO

ALTER TABLE [dbo].[tblUploadFile] CHECK CONSTRAINT [FK_tblUploadFile_tblDonVi]
GO


