USE [Web]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 1/10/2023 1:21:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Fullname] [varchar](250) NULL,
	[phoneNumber] [int] NULL,
	[street] [varchar](250) NULL,
	[districts] [varchar](250) NULL,
	[city] [varchar](250) NULL,
	[province] [varchar](250) NULL,
	[email] [varchar](250) NULL,
	[payment] [varchar](250) NULL,
	[product] [varchar](250) NULL,
	[quantity] [int] NULL,
	[total] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/10/2023 1:21:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](200) NULL,
	[Fullname] [nvarchar](50) NULL,
	[LastLogin] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[ImageURL] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Gender] [nvarchar](50) NULL,
	[DOB] [date] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_fullname] UNIQUE NONCLUSTERED 
(
	[Fullname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_OrderDetails] FOREIGN KEY([Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_OrderDetails]
GO
/****** Object:  StoredProcedure [dbo].[Sp_OrderDetail_Add]    Script Date: 1/10/2023 1:21:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Sp_OrderDetail_Add]
@Fullname Nvarchar(250),
@PhoneNumber int,
@Street nvarchar(250),
@Districts nvarchar(250),
@City nvarchar(250),
@Province nvarchar(250),
@Email nvarchar(250),
@Payment nvarchar(250)
As
Begin
Insert into dbo.OrderDetail
(
	Fullname,
	PhoneNumber,
	Street,
	Districts,
	City,
	Province,
	Email,
	Payment
)
Values(
	@Fullname,
	@PhoneNumber,
	@Street,
	@Districts,
	@City,
	@Province,
	@Email,
	@Payment
)
end
GO
