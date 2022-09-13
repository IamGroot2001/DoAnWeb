USE [master]
GO
/****** Object:  Database [WebQuanAoNam]    Script Date: 11/09/2022 20:48:14 ******/
CREATE DATABASE [WebQuanAoNam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebQuanAoNam_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebQuanAoNam.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebQuanAoNam_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebQuanAoNam.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WebQuanAoNam] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebQuanAoNam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebQuanAoNam] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebQuanAoNam] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebQuanAoNam] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebQuanAoNam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebQuanAoNam] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebQuanAoNam] SET  MULTI_USER 
GO
ALTER DATABASE [WebQuanAoNam] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebQuanAoNam] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebQuanAoNam] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebQuanAoNam] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebQuanAoNam] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WebQuanAoNam] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WebQuanAoNam] SET QUERY_STORE = OFF
GO
USE [WebQuanAoNam]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[TaiKhoanAdmin] [nchar](50) NOT NULL,
	[MatKhauAdmin] [nchar](100) NULL,
	[HoTenAdmin] [nvarchar](50) NULL,
 CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED 
(
	[TaiKhoanAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETDONDATHANG]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETDONDATHANG](
	[MaDDH] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[DonGia] [decimal](18, 0) NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_CHITIETDONDATHANG] PRIMARY KEY CLUSTERED 
(
	[MaDDH] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONDATHANG]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONDATHANG](
	[MaDDH] [int] IDENTITY(1,1) NOT NULL,
	[TinhTrangGiaoHang] [bit] NULL,
	[DaThanhToan] [bit] NULL,
	[NgayDat] [date] NULL,
	[NgayGiao] [date] NULL,
	[MaKH] [int] NULL,
	[ThanhTien] [decimal](18, 0) NULL,
	[DiaChi] [nvarchar](200) NULL,
	[MaVC] [int] NULL,
 CONSTRAINT [PK_DONDATHANG] PRIMARY KEY CLUSTERED 
(
	[MaDDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOTRO]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOTRO](
	[MaKH] [int] NOT NULL,
	[LyDo] [nvarchar](500) NULL,
	[MaHoTen] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_HOTRO_1] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[HoTenKH] [nvarchar](100) NULL,
	[EmailKH] [nvarchar](100) NULL,
	[DiaChiKH] [nvarchar](100) NULL,
	[DienThoaiKH] [varchar](50) NULL,
	[NgaySinhKH] [date] NULL,
	[TaiKhoanKH] [varchar](50) NULL,
	[MatKhauKH] [varchar](100) NOT NULL,
 CONSTRAINT [PK_KHACHHANG] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISANPHAM]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISANPHAM](
	[MaLSP] [int] IDENTITY(1,1) NOT NULL,
	[TenLSP] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LOAISANPHAM] PRIMARY KEY CLUSTERED 
(
	[MaLSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHACUNGCAP]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHACUNGCAP](
	[MaNCC] [int] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](150) NOT NULL,
	[DiaChiNCC] [nvarchar](150) NULL,
	[SDTNCC] [varchar](15) NULL,
 CONSTRAINT [PK_NHACUNGCAP] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](150) NOT NULL,
	[MoTa] [nvarchar](150) NULL,
	[AnhBia] [nchar](200) NULL,
	[NgayCapNhat] [date] NULL,
	[SoLuongTon] [int] NULL,
	[GiaBan] [decimal](18, 0) NULL,
	[MaLSP] [int] NULL,
	[MaNCC] [int] NULL,
 CONSTRAINT [PK_SANPHAM] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VANCHUYEN]    Script Date: 11/09/2022 20:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VANCHUYEN](
	[MaVC] [int] IDENTITY(1,1) NOT NULL,
	[TenVanChuyen] [nvarchar](50) NULL,
 CONSTRAINT [PK_SIZE] PRIMARY KEY CLUSTERED 
(
	[MaVC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ADMIN] ([TaiKhoanAdmin], [MatKhauAdmin], [HoTenAdmin]) VALUES (N'admin                                             ', N'123                                                                                                 ', N'Admin')
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (2, 3, CAST(99000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (2, 5, CAST(66666 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (4, 5, CAST(66666 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (5, 18, CAST(111222 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (6, 8, CAST(225000 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (6, 10, CAST(155000 AS Decimal(18, 0)), 2)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (15, 10, CAST(155000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (16, 4, CAST(450000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (17, 26, CAST(109000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (17, 30, CAST(309000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (17, 32, CAST(209000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (19, 37, CAST(29000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (19, 38, CAST(109000 AS Decimal(18, 0)), 2)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (20, 37, CAST(29000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (21, 38, CAST(1000 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (22, 39, CAST(99000 AS Decimal(18, 0)), 5)
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDDH], [MaSP], [DonGia], [SoLuong]) VALUES (22, 40, CAST(109000 AS Decimal(18, 0)), 5)
GO
SET IDENTITY_INSERT [dbo].[DONDATHANG] ON 
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (2, 1, 1, CAST(N'2022-05-31' AS Date), CAST(N'2022-05-31' AS Date), 1, CAST(990000 AS Decimal(18, 0)), N'aaa', 1)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (4, 1, 1, CAST(N'2022-05-31' AS Date), CAST(N'2022-05-31' AS Date), 1, CAST(99999 AS Decimal(18, 0)), N'bbbbb', 1)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (5, 0, 0, CAST(N'2022-06-27' AS Date), CAST(N'2022-06-27' AS Date), 4, CAST(1000111 AS Decimal(18, 0)), N'dfg', 3)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (6, 0, 0, CAST(N'2022-06-27' AS Date), CAST(N'2022-06-27' AS Date), 4, CAST(985000 AS Decimal(18, 0)), N'dfg', 2)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (15, 0, 0, CAST(N'2022-06-28' AS Date), CAST(N'2022-06-28' AS Date), 1, CAST(155000 AS Decimal(18, 0)), NULL, NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (16, 1, 1, CAST(N'2022-07-02' AS Date), CAST(N'2022-07-06' AS Date), 5, CAST(438222 AS Decimal(18, 0)), NULL, NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (17, 1, 1, CAST(N'2022-07-04' AS Date), CAST(N'2022-07-06' AS Date), 1, CAST(627000 AS Decimal(18, 0)), N'Việt nam', NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (19, 0, 0, CAST(N'2022-07-04' AS Date), CAST(N'2022-07-04' AS Date), 6, CAST(247000 AS Decimal(18, 0)), NULL, NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (20, 0, 1, CAST(N'2022-07-04' AS Date), CAST(N'2022-07-04' AS Date), 4, CAST(29000 AS Decimal(18, 0)), NULL, NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (21, 0, 1, CAST(N'2022-07-04' AS Date), CAST(N'2022-07-04' AS Date), 4, CAST(1000 AS Decimal(18, 0)), NULL, NULL)
GO
INSERT [dbo].[DONDATHANG] ([MaDDH], [TinhTrangGiaoHang], [DaThanhToan], [NgayDat], [NgayGiao], [MaKH], [ThanhTien], [DiaChi], [MaVC]) VALUES (22, 0, 0, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-05' AS Date), 10, CAST(1040000 AS Decimal(18, 0)), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[DONDATHANG] OFF
GO
INSERT [dbo].[HOTRO] ([MaKH], [LyDo], [MaHoTen], [Email]) VALUES (6, N'Mua áo cho chồng mặt rất là đẹp', N'Lê Ngọc Huế Trân', N'buiducvjnk@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[KHACHHANG] ON 
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (1, N'Bùi Đức Vinh', N'vinhducbui16@gmail.com', N'Việt Nam', N'0396108981', CAST(N'2001-09-04' AS Date), N'vinhdz', N'81dc9bdb52d04dc20036dbd8313ed055')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (4, N'Đào Hồ Anh', N'buivanty15@gmail.com', N'Việt Nam', N'1234567890', CAST(N'2001-09-04' AS Date), N'anhdz', N'202cb962ac59075b964b07152d234b70')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (5, N'Nguyễn Tấn Chọn', N'suboyvinhbui@gmail.com', N'Việt Nam', N'0123456789', CAST(N'2016-07-05' AS Date), N'chondz', N'202cb962ac59075b964b07152d234b70')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (6, N'Lê Ngọc Huế Trân', N'buiducvjnk@gmail.com', N'Việt Nam', N'0123456789', CAST(N'2001-06-27' AS Date), N'trandg', N'202cb962ac59075b964b07152d234b70')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (9, N'Lê Ngọc Huế Trân', N'huetran@gmail.com', N'Quận 7', N'0123456789', CAST(N'2001-05-05' AS Date), N'min', N'202cb962ac59075b964b07152d234b70')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTenKH], [EmailKH], [DiaChiKH], [DienThoaiKH], [NgaySinhKH], [TaiKhoanKH], [MatKhauKH]) VALUES (10, N'Hutech', N'vanhuynhthanh98@gmail.com', N'Hutech', N'0123456789', CAST(N'1999-01-01' AS Date), N'hutech1', N'202cb962ac59075b964b07152d234b70')
GO
SET IDENTITY_INSERT [dbo].[KHACHHANG] OFF
GO
SET IDENTITY_INSERT [dbo].[LOAISANPHAM] ON 
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (1, N'Áo Thun')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (2, N'Áo Sơ Mi')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (3, N'Áo Khoác')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (4, N'Quần Kaki')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (5, N'Quần Tây')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (6, N'Quần Short')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (7, N'Giày')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (8, N'Dép')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (10, N'Túi Hiệu')
GO
INSERT [dbo].[LOAISANPHAM] ([MaLSP], [TenLSP]) VALUES (11, N'Kính Mắt')
GO
SET IDENTITY_INSERT [dbo].[LOAISANPHAM] OFF
GO
SET IDENTITY_INSERT [dbo].[NHACUNGCAP] ON 
GO
INSERT [dbo].[NHACUNGCAP] ([MaNCC], [TenNCC], [DiaChiNCC], [SDTNCC]) VALUES (1, N'Gucci', N'88 Đồng Khởi, Quận 1 Thành phố Hồ Chí Minh , Thành phố Hồ Chí Minh, Việt Nam', N'0985408822     ')
GO
INSERT [dbo].[NHACUNGCAP] ([MaNCC], [TenNCC], [DiaChiNCC], [SDTNCC]) VALUES (2, N'Star Fashion', N'Hồ Chí Minh', N'0321456541     ')
GO
INSERT [dbo].[NHACUNGCAP] ([MaNCC], [TenNCC], [DiaChiNCC], [SDTNCC]) VALUES (3, N'Sky', N'Hà Nội', N'0985467154     ')
GO
INSERT [dbo].[NHACUNGCAP] ([MaNCC], [TenNCC], [DiaChiNCC], [SDTNCC]) VALUES (4, N'Handsome''s guy', N'Kiên Giang', N'0423116752     ')
GO
INSERT [dbo].[NHACUNGCAP] ([MaNCC], [TenNCC], [DiaChiNCC], [SDTNCC]) VALUES (5, N'levents2', N'bình thạnh2', N'1234567        ')
GO
SET IDENTITY_INSERT [dbo].[NHACUNGCAP] OFF
GO
SET IDENTITY_INSERT [dbo].[SANPHAM] ON 
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (3, N'Áo Sơ Mi Kaki Đen', N'Lụa Nhung gân  mềm mịn, siêu mát, thấm hút mồ hôi nhanh', N'aosomi1.jfif                                                                                                                                                                                            ', CAST(N'2022-06-03' AS Date), 50, CAST(339000 AS Decimal(18, 0)), 2, 3)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (4, N'Áo Khoác 1', N'Áo chất liệu kaki cực bền', N'aokhoac1.jpg                                                                                                                                                                                            ', CAST(N'2022-06-02' AS Date), 25, CAST(450000 AS Decimal(18, 0)), 3, 4)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (5, N'Quần Tây Đen', N'Thiết kế mang phong cách phóng khoáng và tự do', N'quantayden1.jpg                                                                                                                                                                                         ', CAST(N'2022-06-01' AS Date), 75, CAST(320000 AS Decimal(18, 0)), 5, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (6, N'Quần Short Nâu Thể Thao', N'Quần trẻ trung, thoáng mát và năng động', N'quanshort1.jpg                                                                                                                                                                                          ', CAST(N'2022-05-31' AS Date), 24, CAST(325000 AS Decimal(18, 0)), 6, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (7, N'Giày 1', N'Giày đi bộ và đi học', N'giay1.jpg                                                                                                                                                                                               ', CAST(N'2022-05-30' AS Date), 100, CAST(425000 AS Decimal(18, 0)), 7, 3)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (8, N'Áo Khoác 2', N'Thật đẹp với 1 chiếc áo đơn giản', N'aokhoac2.jpg                                                                                                                                                                                            ', CAST(N'2022-06-12' AS Date), 23, CAST(225000 AS Decimal(18, 0)), 3, 4)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (9, N'Dép 1', N'Dép thời trang', N'dep1.jfif                                                                                                                                                                                               ', CAST(N'2022-06-11' AS Date), 21, CAST(99000 AS Decimal(18, 0)), 8, 3)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (10, N'Áo thun 1', N'Áo thun thời trang', N'aothun1.jfif                                                                                                                                                                                            ', CAST(N'2022-06-10' AS Date), 22, CAST(155000 AS Decimal(18, 0)), 1, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (11, N'Quần kaki 1', N'Quần kaki vải dày dặn', N'quankaki1.jpg                                                                                                                                                                                           ', CAST(N'2022-04-10' AS Date), 39, CAST(425000 AS Decimal(18, 0)), 4, 4)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (15, N'11Áo khoác 4', N'sdadadsas', N'aokhoac4.jpg                                                                                                                                                                                            ', CAST(N'2022-06-19' AS Date), 32, CAST(111111 AS Decimal(18, 0)), 3, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (18, N'12Áo thun', N'ádassds', N'aothun2.jfif                                                                                                                                                                                            ', CAST(N'2022-06-20' AS Date), 12, CAST(111222 AS Decimal(18, 0)), 1, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (19, N'13Giày thể thao23', N'vong', N'giay2.jpg                                                                                                                                                                                               ', CAST(N'2022-07-02' AS Date), 55, CAST(231331 AS Decimal(18, 0)), 7, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (24, N'Túi LV', N'Túi Đẹp', N'tuixach1.png                                                                                                                                                                                            ', CAST(N'2022-07-03' AS Date), 21, CAST(109000 AS Decimal(18, 0)), 10, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (25, N'Áo Thun Xanh', N'Áo thun màu xanh lá cây đẹp trai', N'ao4.png                                                                                                                                                                                                 ', CAST(N'2022-07-04' AS Date), 20, CAST(109000 AS Decimal(18, 0)), 1, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (26, N'Áo Thun Đỏ', N'Áo thun đỏ chứng tỏ yêu em', N'ao8.png                                                                                                                                                                                                 ', CAST(N'2022-07-04' AS Date), 20, CAST(109000 AS Decimal(18, 0)), 1, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (27, N'Áo thun đen', N'Áo thun đen cực đen', N'ao5.png                                                                                                                                                                                                 ', CAST(N'2022-07-04' AS Date), 20, CAST(99000 AS Decimal(18, 0)), 1, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (28, N'Quần Tây Xám', N'Quân Tây Xám Chói', N'quan2.png                                                                                                                                                                                               ', CAST(N'2022-07-04' AS Date), 17, CAST(209000 AS Decimal(18, 0)), 5, 4)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (29, N'Giày Nai', N'Giày Nikeee', N'giaymlb.png                                                                                                                                                                                             ', CAST(N'2022-07-04' AS Date), 20, CAST(409000 AS Decimal(18, 0)), 7, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (30, N'Áo khoác hades', N'Áo khoác chống nắng', N'ao6.png                                                                                                                                                                                                 ', CAST(N'2022-07-04' AS Date), 109000, CAST(309000 AS Decimal(18, 0)), 3, 3)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (31, N'Kính', N'Kính Mắt', N'kinh1.png                                                                                                                                                                                               ', CAST(N'2022-07-04' AS Date), 20, CAST(109000 AS Decimal(18, 0)), 11, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (32, N'Dép', N'Dép lào', N'dep1.png                                                                                                                                                                                                ', CAST(N'2022-07-04' AS Date), 17, CAST(209000 AS Decimal(18, 0)), 8, 5)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (33, N'Quần kaki 2', N'Quần Kaki đẹp', N'quan3.png                                                                                                                                                                                               ', CAST(N'2022-07-04' AS Date), 10, CAST(109000 AS Decimal(18, 0)), 4, 3)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (34, N'Quần Short Nam', N'QUần Short NAm đẹp trai', N'quanshort.jpg                                                                                                                                                                                           ', CAST(N'2022-07-04' AS Date), 22, CAST(99000 AS Decimal(18, 0)), 6, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (35, N'Túi Chéo', N'Túi Đeo Chéo', N'tui nam chéo.jpg                                                                                                                                                                                        ', CAST(N'2022-07-04' AS Date), 20, CAST(89000 AS Decimal(18, 0)), 10, 2)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (37, N'Kính Mắt nam', N'Kính đeo mắt', N'kinh3.png                                                                                                                                                                                               ', CAST(N'2022-07-04' AS Date), 20, CAST(29000 AS Decimal(18, 0)), 11, 5)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (38, N'Áo Sơ Mi', N'Áo Sơ Mi Nam', N'ao2.png                                                                                                                                                                                                 ', CAST(N'2022-07-04' AS Date), 20, CAST(1000 AS Decimal(18, 0)), 2, 1)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (39, N'IT1', N'IT1', N'tainghe1.png                                                                                                                                                                                            ', CAST(N'2022-07-05' AS Date), 21, CAST(99000 AS Decimal(18, 0)), 6, 4)
GO
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [MoTa], [AnhBia], [NgayCapNhat], [SoLuongTon], [GiaBan], [MaLSP], [MaNCC]) VALUES (40, N'IT2', N'IT2', N'vong1.png                                                                                                                                                                                               ', CAST(N'2022-07-05' AS Date), 20, CAST(109000 AS Decimal(18, 0)), 8, 1)
GO
SET IDENTITY_INSERT [dbo].[SANPHAM] OFF
GO
SET IDENTITY_INSERT [dbo].[VANCHUYEN] ON 
GO
INSERT [dbo].[VANCHUYEN] ([MaVC], [TenVanChuyen]) VALUES (1, N'Grap')
GO
INSERT [dbo].[VANCHUYEN] ([MaVC], [TenVanChuyen]) VALUES (2, N'Uber')
GO
INSERT [dbo].[VANCHUYEN] ([MaVC], [TenVanChuyen]) VALUES (3, N'Gojek')
GO
SET IDENTITY_INSERT [dbo].[VANCHUYEN] OFF
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETDONDATHANG_DONDATHANG] FOREIGN KEY([MaDDH])
REFERENCES [dbo].[DONDATHANG] ([MaDDH])
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG] CHECK CONSTRAINT [FK_CHITIETDONDATHANG_DONDATHANG]
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK_CHITIETDONDATHANG_SANPHAM] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([MaSP])
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG] CHECK CONSTRAINT [FK_CHITIETDONDATHANG_SANPHAM]
GO
ALTER TABLE [dbo].[DONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK_DONDATHANG_KHACHHANG] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[DONDATHANG] CHECK CONSTRAINT [FK_DONDATHANG_KHACHHANG]
GO
ALTER TABLE [dbo].[DONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK_DONDATHANG_VANCHUYEN] FOREIGN KEY([MaVC])
REFERENCES [dbo].[VANCHUYEN] ([MaVC])
GO
ALTER TABLE [dbo].[DONDATHANG] CHECK CONSTRAINT [FK_DONDATHANG_VANCHUYEN]
GO
ALTER TABLE [dbo].[HOTRO]  WITH CHECK ADD  CONSTRAINT [FK_HOTRO_KHACHHANG] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOTRO] CHECK CONSTRAINT [FK_HOTRO_KHACHHANG]
GO
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD  CONSTRAINT [FK_SANPHAM_LOAISANPHAM] FOREIGN KEY([MaLSP])
REFERENCES [dbo].[LOAISANPHAM] ([MaLSP])
GO
ALTER TABLE [dbo].[SANPHAM] CHECK CONSTRAINT [FK_SANPHAM_LOAISANPHAM]
GO
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD  CONSTRAINT [FK_SANPHAM_NHACUNGCAP] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NHACUNGCAP] ([MaNCC])
GO
ALTER TABLE [dbo].[SANPHAM] CHECK CONSTRAINT [FK_SANPHAM_NHACUNGCAP]
GO
USE [master]
GO
ALTER DATABASE [WebQuanAoNam] SET  READ_WRITE 
GO
