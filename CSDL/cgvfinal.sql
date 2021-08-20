USE [master]
GO
/****** Object:  Database [CGV]    Script Date: 8/20/2021 12:42:45 PM ******/
CREATE DATABASE [CGV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CGV', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CGV.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CGV_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CGV_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CGV] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CGV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CGV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CGV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CGV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CGV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CGV] SET ARITHABORT OFF 
GO
ALTER DATABASE [CGV] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CGV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CGV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CGV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CGV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CGV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CGV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CGV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CGV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CGV] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CGV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CGV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CGV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CGV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CGV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CGV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CGV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CGV] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CGV] SET  MULTI_USER 
GO
ALTER DATABASE [CGV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CGV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CGV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CGV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CGV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CGV] SET QUERY_STORE = OFF
GO
USE [CGV]
GO
/****** Object:  Table [dbo].[booking]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[booking](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[film_id] [int] NOT NULL,
	[schedule_id] [int] NOT NULL,
	[showtime_id] [int] NOT NULL,
	[room_id] [int] NOT NULL,
	[seat_id] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[status] [int] NOT NULL,
	[create_time] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category_film]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category_film](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category_post]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category_post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[films]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[films](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[director] [nvarchar](255) NULL,
	[actor] [nvarchar](255) NOT NULL,
	[duration] [nvarchar](255) NULL,
	[film_name] [nvarchar](255) NULL,
	[image] [nvarchar](255) NULL,
	[trailer] [nvarchar](max) NULL,
	[id_cfilm] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](250) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[image] [nvarchar](250) NULL,
	[created_at] [datetime] NOT NULL,
	[id_cpost] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ratings]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ratings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[film_id] [int] NULL,
	[rate] [nvarchar](max) NULL,
	[id_user] [int] NULL,
	[created_time] [datetime] NOT NULL,
	[name_user] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[room]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[room](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[room_name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[schedules]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schedules](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[film_id] [int] NULL,
	[dateschedule] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[seats]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[seats](
	[id] [int] IDENTITY(10,1) NOT NULL,
	[seat_name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[showtimes]    Script Date: 8/20/2021 12:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[showtimes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[schedule_id] [int] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usercgv]    Script Date: 8/20/2021 12:42:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usercgv](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NULL,
	[is_active] [int] NULL,
	[password] [nvarchar](255) NULL,
	[phonenumber] [nvarchar](255) NULL,
	[role_id] [int] NULL,
	[username] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[booking] ON 

INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1119, 40, 1, 24, 11, 1, 10, 3, 0, N'637650110251474020')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1120, 40, 1, 24, 11, 1, 11, 3, 0, N'637650110251474020')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1121, 40, 1, 24, 11, 1, 12, 3, 0, N'637650110251474020')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1122, 40, 1, 24, 11, 1, 13, 3, 0, N'637650110251474020')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1123, 40, 8, 1, 28, 1, 10, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1124, 40, 8, 2, 28, 1, 11, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1125, 40, 8, 3, 28, 1, 12, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1126, 40, 8, 4, 28, 1, 13, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1127, 40, 8, 5, 28, 1, 14, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1128, 40, 8, 6, 28, 1, 15, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1129, 40, 8, 7, 28, 1, 16, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1130, 40, 8, 8, 28, 1, 17, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1131, 40, 8, 9, 28, 1, 18, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1132, 40, 8, 10, 28, 1, 19, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1133, 40, 8, 11, 28, 1, 26, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1134, 40, 8, 12, 28, 1, 27, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1135, 40, 8, 13, 28, 1, 28, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1136, 40, 8, 14, 28, 1, 29, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1137, 40, 8, 15, 28, 1, 30, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1138, 40, 8, 16, 28, 1, 31, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1139, 40, 8, 17, 28, 1, 32, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1140, 40, 8, 18, 28, 1, 33, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1141, 40, 8, 19, 28, 1, 34, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1142, 40, 8, 20, 28, 1, 35, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1143, 40, 8, 21, 28, 1, 36, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1144, 40, 8, 22, 28, 1, 37, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1145, 40, 8, 23, 28, 1, 38, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1146, 40, 8, 23, 28, 1, 39, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1147, 40, 8, 22, 28, 1, 40, 3, 0, N'637650588007557689')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1148, 40, 8, 21, 28, 2, 10, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1149, 40, 8, 20, 28, 2, 11, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1150, 40, 8, 19, 28, 2, 12, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1151, 40, 8, 18, 28, 2, 13, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1152, 40, 8, 17, 28, 2, 14, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1153, 40, 8, 16, 28, 2, 15, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1154, 40, 8, 15, 28, 2, 16, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1155, 40, 8, 14, 28, 2, 17, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1156, 40, 8, 13, 28, 2, 18, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1157, 40, 8, 12, 28, 2, 19, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1158, 40, 8, 11, 28, 2, 26, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1159, 40, 8, 10, 28, 2, 27, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1160, 40, 8, 9, 28, 2, 28, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1161, 40, 8, 8, 28, 2, 29, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1162, 40, 8, 7, 28, 2, 30, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1163, 40, 8, 6, 28, 2, 31, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1164, 40, 8, 5, 28, 2, 32, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1165, 40, 8, 30, 28, 2, 33, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1166, 40, 8, 30, 28, 2, 34, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1167, 40, 8, 30, 28, 2, 35, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1168, 40, 8, 30, 28, 2, 36, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1169, 40, 8, 30, 28, 2, 37, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1170, 40, 8, 30, 28, 2, 38, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1171, 40, 8, 30, 28, 2, 39, 3, 0, N'637650588741688375')
INSERT [dbo].[booking] ([id], [id_user], [film_id], [schedule_id], [showtime_id], [room_id], [seat_id], [amount], [status], [create_time]) VALUES (1172, 40, 8, 30, 28, 2, 40, 3, 0, N'637650588741688375')
SET IDENTITY_INSERT [dbo].[booking] OFF
GO
SET IDENTITY_INSERT [dbo].[category_film] ON 

INSERT [dbo].[category_film] ([id], [name]) VALUES (1, N'Phim tình cảm')
INSERT [dbo].[category_film] ([id], [name]) VALUES (2, N'Phim hành động')
INSERT [dbo].[category_film] ([id], [name]) VALUES (3, N'Phim kinh dị')
SET IDENTITY_INSERT [dbo].[category_film] OFF
GO
SET IDENTITY_INSERT [dbo].[category_post] ON 

INSERT [dbo].[category_post] ([id], [name]) VALUES (1, N'Khuyến mãi')
INSERT [dbo].[category_post] ([id], [name]) VALUES (2, N'Giới thiệu')
SET IDENTITY_INSERT [dbo].[category_post] OFF
GO
SET IDENTITY_INSERT [dbo].[films] ON 

INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (1, N'Hay', N'Trấn Thành', N'Trấn Thành , Tuấn Trần', N'120 phút', N'Bố Già', N'bogia.jpg', N'https://www.youtube.com/embed/jluSu8Rw6YE', 1)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (2, N'Hay', N'Quang Dũng', N'Wowy', N'120 phút', N'Ròm', N'rom.jpg', N'https://www.youtube.com/embed/XRm1P7oGpMQ', 2)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (3, N'Hay', N'Nước ngoài', N'Wowy', N'120 phút', N'Ấn Quỷ', N'anquy.jpg', N'https://www.youtube.com/embed/NmQiJPLYzPI', 2)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (4, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N' Minions - Sự Trỗi Dậy Của Gru ', N'minion.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 2)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (5, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N'Veldom', N'venom2.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 3)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (6, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N'Veldom', N'camtu.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 3)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (7, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N'hotel', N'hotel.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 3)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (8, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N'kinh dị', N'kinhdi.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 3)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (9, N'Hay', N'Nước ngoài', N'Nước ngoài', N'120 phút', N'Baby', N'baby.jpg', N'https://www.youtube.com/embed/54yAKyNkK7w', 3)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (11, N'Chìa Khoá Trăm Tỷ bắt đầu khi một sát thủ khét tiếng vô tình bị mất trí vì một tai nạn bất ngờ, rồi bắt đầu một cuộc sống mới bằng nghề diễn viên quần chúng. Chuyện gì sẽ xảy ra nếu chàng diễn viên quần chúng này quên hẳn cuộc đời sát thủ để trở thành một ngôi sao hành động? Liệu sự nghiệp diễn viên và cô quản lý bất đắc dĩ có giúp hắn thay đổi quá khứ mãi mãi và sống trọn vẹn một cuộc đời mới? Một bộ phim hài-hành động nhưng cũng đầy sự ấm áp về hành trình "đổi đời" của một kẻ giết thuê để mưu sinh.', N'Võ Thanh Hoà', N'Kiều Minh Tuấn, Thu Trang, Jun Vũ', N'90 phút', N'Chìa khóa trăm tỷ', N'film1534469493.jpg', N'https://www.youtube.com/watch?v=gVI2rTSgeVA', 2)
INSERT [dbo].[films] ([id], [description], [director], [actor], [duration], [film_name], [image], [trailer], [id_cfilm]) VALUES (12, N'Shang-Chi và Huyền Thoại Thập Nhẫn là bộ phim thuộc giai đoạn 4 của Vũ trụ điện ảnh Marvel. Nhân vật này được biết đến như một bậc thầy Kung Fu, tinh thông võ thuật. Sức mạnh của Shang-Chi đến từ hàng ngàn giờ luyện tập miệt mài và sự kỷ luật cao độ với bản thân. Siêu anh hùng võ thuật này được chính bố dạy dỗ để trở thành một sát thủ chuyên nghiệp và kế thừa tập đoàn tội ác của ông.', N'Destin Daniel Cretton', N'Simu Liu, Awkwafina, Tony Chiu-Wai Leung', N'120 phút', N'Shang-Chi và Huyền Thoại Thập Nhẫn', N'film491300553.jpg', N'https://youtu.be/X2vXkO4n9Cs', 2)
SET IDENTITY_INSERT [dbo].[films] OFF
GO
SET IDENTITY_INSERT [dbo].[post] ON 

INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (1, N'Giới thiệu', N'<p>\"<strong>Le Do Cin</strong>, cụm rạp 5 phòng chiếu lần đầu tiên xuất hiện tại Đà Nẵng, đem đến cho bạn những trải nghiệm điện ảnh tươi mới tuyệt hảo với mức giá ưu đãi nhất.\"<br>Chào mừng bạn đến với <strong>Le Do Cinema!</strong><br>Chúng tôi muốn kể cho bạn câu chuyện về mình là ai, giá trị nào là cốt lõi và cách chúng tôi đem đến cho khách hàng những cảm xúc trọn vẹn nhất.&nbsp;<br>Bạn có thể từng trải nghiệm nhiều rạp chiếu phim chuyên nghiệp tại Đà Nẵng, phục vụ đáp ứng nhu cầu của phần đông khán giả trẻ những năm gần đây. Nhưng có lẽ bạn chưa biết về một <strong>Le Do Cinema</strong>, cụm rạp trẻ hứa hẹn đem đến cho bạn trải nghiệm tuyệt vời nhất với mức giá vô cùng ưu đãi.<br>Lần đầu tiên xuất hiện tại Đà Nẵng, LeDo Cinema đem đến 5 phòng chiếu phim được trang bị những công nghệ hiện đại nhất. Thưởng thức phim tại LeDo Cinema là bạn được sống trong không gian hình ảnh chân thực với thiết kế màn hình uốn cong cực đại, phản chiếu tốt từ mọi hướng nhìn. Âm thanh cực đã đến từ công nghệ Dolby danh tiếng cho trải nghiệm thính giác vô cùng sống động. Phòng chiếu sang trọng với công nghệ ghế da cao cấp cùng các tiện nghi được đáp ứng phù hợp nhất.<br><strong>Le Do Cinema</strong>, được thiết kế với phong cách chủ đạo mang hơi thở của những thập kỷ trước đem đến cảm giác mới lạ nhưng lại vô cùng thân thuộc. Và tất nhiên rồi, bạn sẽ luôn có những bức ảnh tuyệt vời trong không gian đậm chất vintage tại đây. Thông qua hệ thống website, bạn có thể cập nhật thông tin của những bộ phim mới nhất, đầy đủ và chi tiết cho từng suất chiếu và nhanh chóng đặt vé trực tuyến để xem những suất chiếu đầu tiên của các siêu phẩm điện ảnh.<br><strong>Le Do Cinema</strong> trân trọng giá trị của sự kết nối và đề cao trải nghiệm của khách hàng. Chúng tôi được truyền cảm hứng và sẽ lan tỏa cảm hứng đó đến khán giả của mình thông qua những thước phim tuyệt phẩm và chất lượng phục vụ chu đáo.<br>&nbsp;</p>', N'1627654886493banner-digi4home.com-5.jpg', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 2)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (2, N'Điều khoản chung', N' Nếu bạn muốn khoác lên mình bộ đồng phục <b>Le Do Cinema</b>, đem tinh thần vui tươi của <b>Le Do Cinema</b>\r\n            đến với khách hàng và học hỏi nhiều hơn nữa trong môi trường rạp phim đầy những trải nghiệm mới mẻ. Hãy nộp\r\n            đơn ngay đợt Tuyển dụng tháng 2 năm 2019 này.<br>\r\n            <b>Le Do Cinema</b> đặt nền móng ở con người, mỗi nhân viên làm việc tại đây đều sẽ cảm nhận được sự gắn\r\n            kết, chuyên nghiệp cũng như được đào tạo và nâng cao kỹ năng chuyên môn. Để làm được những điều đó, <b>Le Do\r\n                Cinema</b> Cinema mong muốn tìm người đồng hành với phẩm chất và tính cách phù hợp ở những vị trí\r\n            sau:<br>\r\n            <b>1. Part time cashier</b><br>\r\n            Số lượng: 15<br>\r\n            Mô tả công việc<br>\r\n            - Trực quầy thu ngân, bán hàng tại quầy bán bắp nước, quầy vé.<br>\r\n\r\n            - Tư vấn chọn phim và chỗ ngồi, các gói combo.<br>\r\n\r\n            - Trông giữ quầy đồ tại Rạp.<br>\r\n\r\n            - Soát vé, hướng dẫn chỗ ngồi, kiểm tra vệ sinh rạp.<br>\r\n\r\n            - Xuất vé khi khách hàng đặt online trên hệ thống.<br>\r\n\r\n            - Trả lời các câu hỏi của khách về lịch chiếu, đặt vé qua điện thoại.<br>\r\n\r\n            <b>Yêu cầu</b><br>\r\n            - Là Sinh viên các trường Đại học tại Đà Nẵng.<br - Nhanh nhẹn, giao tiếp tốt, ngoại hình ưa nhìn.<br>\r\n\r\n            - Trung thực, yêu thích các công việc ngành dịch vụ.<br>\r\n\r\n            - Sẵn sàng làm các ca linh động từ 8h00-24h00, làm việc vào các ngày cuối tuần, Lễ, Tết.<br>\r\n\r\n            - Độ tuổi từ 19 đến 22 tuổi.<br>', N'singin.jpg', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 2)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (3, N'Chính Sách Bảo Mật ', N' <b>1. Mục đích và phạm vi thu thập thông tin</b><br>\r\n            - Thông tin cá nhân các tài khoản thành viên của <b>Le Do Cinema</b> được thực hiện trên cơ sở khách hàng tự\r\n            nguyện cung cấp tại website <b>Le Do Cinema</b> với các nội dung: Họ tên, giới tính, năm sinh, số CMND, địa\r\n            chỉ, số điện thoại di động, email. <br>\r\n            - Mục đích thu thập thông tin khách hàng gồm: <br>\r\n            + Cung cấp sản phẩm, dịch vụ theo nhu cầu của khách hàng<br>\r\n            + Liên hệ xác nhận khi khách hàng đăng ký sử dụng dịch vụ, xác lập giao dịch trên website <b>Le Do\r\n                Cinema</b><br>\r\n            + Thực hiện việc quản lý website <b>Le Do Cinema</b>: gửi thông tin cập nhật về website, các chương trình\r\n            khuyến mại, ưu đãi/ tri ân tới khách hàng<br>\r\n            + Bảo đảm quyền lợi của khách hàng khi phát hiện các hành động giả mạo, phá hoại tài khoản, lừa đảo khách\r\n            hàng<br>\r\n            + Liên lạc, hỗ trợ, giải quyết với khách hàng trong các trường hợp đặc biệt<br>\r\n            - Để tránh nghi ngờ, trong quá trình giao dịch thanh toán tại website <b>Le Do Cinema</b>, chúng tôi chỉ lưu\r\n            giữ thông tin chi tiết về đơn hàng đã thanh toán của khách hàng, các thông tin về tài khoản ngân hàng của\r\n            khách hàng sẽ không được lưu giữ.<br>\r\n\r\n            <b>2. Phạm vi sử dụng thông tin</b><br>\r\n            -<b>Le Do Cinema</b> chỉ sử dụng thông tin của khách hàng cho các mục đích đã nêu rõ và sẽ thông báo để có\r\n            được sự đồng ý của khách hàng khi sử dụng với mục đích khác. <br>\r\n            -<b>Le Do Cinema</b> cam kết sẽ không sử dụng thông tin của khách hàng để gửi quảng cáo, giới thiệu dịch vụ\r\n            khi chưa được sự cho phép của khách hàng. <br>\r\n            - Trong trường hợp có sự can thiệp từ Pháp luật, <b>Le Do Cinema</b> buộc phải tuân thủ và cung cấp thông\r\n            tin dữ liệu khách hàng được yêu cầu. <b>Le Do Cinema</b> hoàn toàn được miễn trừ trách nhiệm liên quan đến\r\n            bảo mật thông tin.<br>\r\n\r\n            <b>3. Thời gian lưu trữ</b><br>\r\n            Dữ liệu cá nhân cơ bản của khách hàng đăng ký thành viên <b>Le Do Cinema</b> sẽ được lưu trữ cho đến khi có\r\n            yêu cầu hủy bỏ hoặc tự thành viên đăng nhập và thực hiện đóng tài khoản.<br>\r\n\r\n            <b>4. Cách thức điều chỉnh dữ liệu cá nhân</b><br>\r\n            Để chỉnh sửa dữ liệu cá nhân của mình trên hệ thống <b>Le Do Cinema</b>, khách hàng phải đăng nhập vào tài\r\n            khoản và chỉnh sửa thông tin, dữ liệu cá nhân bao gồm: họ tên, giới tính, ngày sinh, chứng minh nhân dân, số\r\n            điện thoại, địa chỉ, email. <br>\r\n\r\n            <b>5. Cam kết</b><br>\r\n            - Mọi thông tin cá nhân của khách hàng thu thập được từ website <b>Le Do Cinema</b> sẽ được lưu giữ an toàn;\r\n            chỉ có khách hàng mới có thể truy cập vào tài khoản cá nhân của mình bằng tên đăng nhập và mật khẩu do khách\r\n            hàng chọn.<br>\r\n            - Chúng tôi cam kết bảo mật thông tin, không chia sẻ, tiết lộ, chuyển giao thông tin cá nhân của khách hàng,\r\n            thông tin giao dịch trực tuyến trên website <b>Le Do Cinema</b> cho bất kỳ bên thứ ba nào khi chưa được sự\r\n            đồng ý của khách hàng, trừ trường hợp phải thực hiện theo yêu cầu của các cơ quan Nhà nước có thẩm quyền,\r\n            hoặc theo quy định của pháp luật hoặc việc cung cấp thông tin đó là cần thiết để <b>Le Do Cinema</b> cung\r\n            cấp dịch vụ/ tiện ích cho khách hàng<br>\r\n            - <b>Le Do Cinema</b> trong khả năng tốt nhất của mình sẽ cố hết sức bảo vệ cũng như ngăn chặn các hành vi\r\n            phá hoại, chỉnh sửa trái phép hoặc đánh cắp thông tin của khách hàng. Tuy nhiên, chúng tôi không thể kiểm\r\n            soát được mọi công nghệ, hình thức, mánh khóe mà đơn vị, cá nhân phá hoại sử dụng; trong trường hợp đó <b>Le\r\n                Do Cinema</b> sẽ không chịu trách nhiệm với các thiệt hại, mất mát vê thông tin, tài khoản của khách\r\n            hàng. <br>\r\n            - Trường hợp máy chủ lưu trữ thông tin bị hacker tấn công dẫn đến mất mát dữ liệu cá nhân, gây ảnh hưởng xấu\r\n            đến khách hàng, <b>Le Do Cinema</b> sẽ ngay lập tức thông báo cho khách hàng và trình vụ việc cho cơ quan\r\n            chức năng điều tra xử lý.<br>\r\n            - Đối với các giao dịch trực tuyến được thực hiện thông qua website, <b>Le Do Cinema</b> không lưu trữ thông\r\n            tin thẻ thanh toán của khách hàng. Thông tin tài khoản, thẻ thanh toán của khách hàng sẽ được các đối tác\r\n            cổng thanh toán của LeDo bảo vệ theo tiêu chuẩn quốc tế.<br>\r\n            - Khách hàng có nghĩa vụ bảo mật tên đăng ký, mật khẩu và hộp thư điện tử của mình. <b>Le Do Cinema</b> sẽ\r\n            không chịu trách nhiệm dưới bất kỳ hình thức nào đối với các thiệt hại, tổn thất (nếu có) do khách hàng\r\n            không tuân thủ quy định bảo mật này.<br>\r\n            - Khách hàng tuyệt đối không được có các hành vi sử dụng công cụ, chương trình để can thiệp trái phép vào hệ\r\n            thống hay làm thay đổi dữ liệu của <b>Le Do Cinema</b>. Trong trường hợp <b>Le Do Cinema</b> phát hiện khách\r\n            hàng có hành vi cố tình giả mạo, gian lận, phát tán thông tin cá nhân trái phép… <b>Le Do Cinema</b> có\r\n            quyền chuyển thông tin cá nhân của khách hàng cho các cơ quan có thẩm quyền để xử lý theo quy định của pháp\r\n            luật.<br>', N'singin.jpg', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 2)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (4, N'Khởi Nghiệp Cùng Le Do Cinema', N'Nếu bạn muốn khoác lên mình bộ đồng phục Le Do Cinema, đem tinh thần vui tươi của Le Do Cinema đến với khách hàng và học hỏi nhiều hơn nữa trong môi trường rạp phim đầy những trải nghiệm mới mẻ. Hãy nộp đơn ngay đợt Tuyển dụng tháng 2 năm 2019 này.\r\nLe Do Cinema đặt nền móng ở con người, mỗi nhân viên làm việc tại đây đều sẽ cảm nhận được sự gắn kết, chuyên nghiệp cũng như được đào tạo và nâng cao kỹ năng chuyên môn. Để làm được những điều đó, Le Do Cinema Cinema mong muốn tìm người đồng hành với phẩm chất và tính cách phù hợp ở những vị trí sau:\r\n1. Part time cashier\r\nSố lượng: 15\r\nMô tả công việc\r\n- Trực quầy thu ngân, bán hàng tại quầy bán bắp nước, quầy vé.\r\n- Tư vấn chọn phim và chỗ ngồi, các gói combo.\r\n- Trông giữ quầy đồ tại Rạp.\r\n- Soát vé, hướng dẫn chỗ ngồi, kiểm tra vệ sinh rạp.\r\n- Xuất vé khi khách hàng đặt online trên hệ thống.\r\n- Trả lời các câu hỏi của khách về lịch chiếu, đặt vé qua điện thoại.\r\nYêu cầu\r\n- Là Sinh viên các trường Đại học tại Đà Nẵng.\r\n- Nhanh nhẹn, giao tiếp tốt, ngoại hình ưa nhìn.\r\n- Trung thực, yêu thích các công việc ngành dịch vụ.\r\n- Sẵn sàng làm các ca linh động từ 8h00-24h00, làm việc vào các ngày cuối tuần, Lễ, Tết.\r\n- Độ tuổi từ 19 đến 22 tuổi.Chào mừng bạn đến với Website chính thức của <b>Le Do Cinema</b> www.ledocinema.vn. Việc sử dụng website này\r\n            đồng nghĩa với việc bạn đồng ý theo những thỏa thuận dưới đây. Nếu bạn không đồng ý, xin vui lòng ngưng sử\r\n            dụng website. <br>\r\n\r\n            <b>1. Rủi ro cá nhân khi truy cập</b><br>\r\n            Khi truy cập trang web này, bạn đồng ý chấp nhận mọi rủi ro. <b>Le Do Cinema</b> cũng như các bên đối tác\r\n            xây dựng trang web sẽ không chịu trách nhiệm về bất kỳ tổn thất nào do hậu quả trực tiếp, hay gián tiếp;\r\n            những thất thoát, chi phí (bao gồm chi phí pháp lý, chi phí tư vấn hoặc các khoản chi tiêu khác) có thể phát\r\n            sinh trực tiếp hay gián tiếp do truy cập trang web hoặc tải dữ liệu về máy; những tổn hại gặp phải do virus,\r\n            hành động phá hoại trực tiếp hay gián tiếp của hệ thống máy tính khác, đường dây điện thoại, phần cứng, phần\r\n            mềm, các lỗi kỹ thuật khác gây cản trở việc truyền tải qua máy vi tính hoặc kết nối mạng.<br>\r\n\r\n            <b>2. Sử dụng thông tin</b><br>\r\n            Mọi thông tin, dữ liệu cá nhân bạn chuyển đến trang web này dưới bất kỳ lí do, hình thức nào đều trở thành\r\n            tài sản của <b>Le Do Cinema</b> và sẽ được bảo mật. Thông tin của bạn sẽ chỉ được sử dụng với mục đích liên\r\n            lạc hoặc cập nhật lịch chiếu, khuyến mại của <b>Le Do Cinema</b> qua email hoặc bưu điện. <br>\r\n\r\n            <b>3. Quyền sử dụng của thành viên</b><br>\r\n            Thành viên tham gia website <b>Le Do Cinema</b> không được đăng tải những nội dung hình ảnh, từ ngữ mang\r\n            tính khiêu dâm, đồi trụy, tục tĩu; phỉ báng hoặc hăm dọa người khác; vi phạm luật pháp hoặc dẫn tới trách\r\n            nhiệm pháp lý. <b>Le Do Cinema</b> sẽ không chịu trách nhiệm hay có nghĩa vụ đối với các nội dung này và sẽ\r\n            phối hợp với cơ quan pháp luật nếu được yêu cầu. <br>\r\n\r\n            <b>4. Nội dung </b><br>\r\n            <b>Le Do Cinema</b> là một website được cung cấp vì lợi ích của khách hàng và mang tính phi thương mại. Tất\r\n            cả những thông tin được đăng tải đều phản ánh trung thực tính chất sự việc, tuy nhiên <b>Le Do Cinema</b> và\r\n            các bên liên quan không đảm bảo tính chính xác, độ tin cậy cũng như việc sử dụng kết quả của sự việc trên\r\n            trang web. <b>Le Do Cinema</b> luôn cố gắng cập nhật thường xuyên nội dung trang website cũng như luôn có\r\n            thể thay đổi bất kì lúc nào để phù hợp. <br>\r\n\r\n            <b>5. Bản quyền và sửa đổi</b><br>\r\n            <b>Le Do Cinema</b> nắm giữ bản quyền của trang web này. Việc chỉnh sửa, sắp xếp, loại bỏ thông tin trang\r\n            thuộc về thẩm quyền của LeDo. Mọi chỉnh sửa, thay đổi, phân phối hoặc tái sử dụng những nội dung trong trang\r\n            này vì bất kì mục đích nào khác được xem như vi phạm quyền lợi hợp pháp của Le Do Cinema. <br>\r\n\r\n            <b>6. Liên kết ngoài</b><br>\r\n            Trang web này có thể được liên kết với những trang khác, <b>Le Do Cinema</b> không trực tiếp hoặc gián tiếp\r\n            tán thành, tổ chức, tài trợ, đứng sau hoặc sát nhập với những trang đó, trừ khi điều này được nêu ra rõ\r\n            ràng. Khi truy cập vào trang web, bạn phải hiểu và chấp nhận rằng <b>Le Do Cinema</b> không thể kiểm soát và\r\n            chịu trách nhiệm nội dung của tất cả những trang liên kết với website này. <br>\r\n\r\n            <b>7. Luật áp dụng</b><br>\r\n            Mọi hoạt động phát sinh từ trang web có thể sẽ được phân tích và đánh giá theo luật pháp Việt Nam và toà án\r\n            Thành phố Đà Nẵng. Vì vậy bạn phải đồng ý tuân theo các điều khoản riêng của các toà án này.<br>', N'singin.jpg', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 2)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (5, N'THÊM 03 THÁNG ĐỂ TRỞ THÀNH VIP & VVIP 2021', N'Nhân dịp đầu năm 2021, xin chân thành cám ơn tất cả Quý khách hàng thành viên đã đồng hành cùng Lê Độ trong 2020 vừa qua. Như một lời tri ân và chào mừng năm mới, Lê Độ mang đến chương trình cực kỳ hấp dẫn: “THÊM 03 THÁNG ĐỂ NÂNG HẠNG VIP / VVIP 2021”<br><br><b>A. DUY TRÌ HẠNG THÀNH VIÊN DÀNH CHO KHÁCH HÀNG VIP/ VVIP 2020</b><br><br><b>1. KHÁCH HÀNG VIP CỦA NĂM 2020</b> có tổng chi tiêu trong 12 tháng (từ 01/01/2020 – 31/12/2020) <b>CHƯA ĐỦ ĐIỀU KIỆN ĐỂ TRỞ THÀNH KHÁCH HÀNG VIP CỦA NĂM 2021</b> vẫn sẽ được sử dụng quyền lợi VIP trong 03 tháng đầu của năm 2021 (từ 01/01/2021 đến 31/03/2021), bao gồm:<br>\r\n                - Quà sinh nhật dành cho VIP (đối với khách hàng có sinh nhật trong tháng 1, 2 & 3 năm 2021)<br>\r\n                - Tích luỹ điểm thưởng theo tỷ lệ của hạng VIP<br>\r\n                - Giảm giá 15% thức ăn nóng<br>\r\n                *Khách hàng thuộc nhóm này nhận 08 vé xem phim 2D/3D miễn phí từ ngày 04/01/2021 do đạt điều kiện trở thành VIP vào 01/01/2021.<br><br><b>B. CƠ HỘI NÂNG HẠNG VIP VVIP 2021 DÀNH CHO TẤT CẢ CÁC KHÁCH HÀNG:</b><br>\r\n                Tất cả các khách hàng thành viên Lê Độ sẽ được xét tổng chi tiêu vào 01/04/2021 để có cơ hội nâng hạng VIP/VVIP 2021. Tổng chi tiêu được sử dụng để xét nâng hạng bao gồm 15 tháng, từ 01/01/2020 đến 31/03/2021.<br>\r\n                Chi tiết về điều kiện và quyền lợi của từng nhóm thành viên sau khi xét hạng ngày 01/04/2021, vui lòng liên hệ hotline để biết thêm thông tin chi tiết!<br>', N'event1.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (6, N'BỘ SƯU TẬP DORAEMON', N'Còn gì tuyệt hơn khi vừa ôm Mon ú trong tay, vừa nhâm nhi bắp rang giòn tan và thưởng thức phim bom tấn tại hệ thống rạp Lê Độ nè. Cùng Lê Độ sưu tập đủ bộ sản phẩm Doraemon siêu đáng yêu ngay nhé.<br>\r\n                <b>Thông tin sản phẩm:</b><br>\r\n                - Pouch Combo 109k bao gồm: 1 Doraemon Pouch + 1 nước ngọt siêu lớn 32oz<br>\r\n                - Pillow Combo 179k bao gồm: 1 Doraemon Pillow + 1 nước ngọt siêu lớn 32oz<br>\r\n                - Dinosaur Combo 339k bao gồm: 1 Doraemon Dinosaur + 1 nước ngọt siêu lớn 32oz<br>\r\n                (*) Mua thêm một phần bắp ngọt lớn với giá 29k thui nhé\r\n                Sản phẩm được bán độc quyền tại Lê Độ khu vực Hồ Chí Minh, Hà Nội, Bình Dương và Đồng Nai. Số lượng có hạn.', N'event2.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (7, N'HAPPY OX YEAR – HAPPY OX COMBO', N'Tiếp tục thông lệ đón năm mới hằng năm, Lê Độ mở bán “đặc sản” ly thiết kế theo năm <b>Happy Ox Combo</b> mừng năm Tân Sửu 2021.<br><br>\r\n                Rinh ngay ly Trâu vui vẻ để cả năm mọi việc đều suông sẻ nhé cả nhà.<br><br>\r\n                Sản phẩm hiện đang được bán độc quyền tại các cụm rạp Lê Độ khu vực Hồ Chí Minh, Hà Nội, Bình Dương, Biên Hòa , Cần Thơ, Trà Vinh.<br><br>\r\n                Happy OX Combo giá 249k bao gồm: 1 ly Trâu vui vẻ 44oz (kèm nước) + 1 bắp ngọt lớn 44oz<br><br>\r\n                <b>Tải app Lê Độ để mua sản phẩm sớm hơn, nhanh hơn:</b><br>\r\n                <a href=\"https://apps.apple.com/us/app/cgv-cinemas/id1067166194\">- Tải Lê Độ Cinemas cho iOS</a><br>\r\n                <a href=\"https://play.google.com/store/apps/details?id=com.cgv.cinema.vn\">- Tải Lê Độ Cinemas cho Android</a>', N'event3.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (8, N'CHƯƠNG TRÌNH AN TOÀN CÙNG Lê Độ', N'<b>Thông tin sản phẩm:</b><br>\r\n                Khi mua N95 Combo giá 139k bao gồm 1 bắp Caramel 44oz, 2 Coke 32oz, và 1 snack, khách hàng sẽ được tặng 1 lần refill nước miễn phí và 1 khẩu trang kháng khuẩn cao cấp 3P-N95.<br>\r\n                Thời gian diễn ra từ 25/3 – 25/4/2021<br>\r\n                Địa điểm áp dụng: tại các cụm rạp Lê Độ Cinemas Hà Nội, Hồ Chí Minh, Đà Nẵng, Bình Dương, Đồng Nai, Cần Thơ.<br>\r\n                <b>Điều kiện & Điều khoản:</b><br>\r\n                - Chương trình áp dụng cho cả giao dịch online và offline<br>\r\n                - Chương trình có thể kết thúc sớm hơn dự kiến khi số lượng quà tặng đã phát hết<br>\r\n                - Một thành viên được tham gia mua nhiều combo để nhận số lượng quà tương ứng<br>\r\n                - Chương trình không áp dụng đồng thời đối với các khuyến mại khác<br>\r\n                - Phụ thu khi đổi vị bắp và nước theo qui định của Lê Độ', N'event4.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (9, N'Lê Độ TAROT SERIES TRỞ LẠI VỚI MÙA 2!', N'Tarot Series đã trở lại YouTube Lê Độ Cinemas Vietnam với lịch phát sóng vào tối Thứ Tư và Thứ Bảy mỗi tuần.\r\n\r\n                Nội dung này đã được bắt đầu vào đầu năm 2021 và những phản hồi tích cực từ người xem đã tạo động lực cho Lê Độ tiếp tục phát triển mùa 2.\r\n                \r\n                Hy vọng rằng, Lê Độ Tarot Series sẽ mang đến những thông điệp, năng lượng tích cực đến toàn thể mọi người.\r\n                \r\n                Bây giờ, hãy hít thở thật sâu. Chọn 1 tụ và tìm hiểu Tài năng của bạn là gì? trong tập 1 nhé!<br><br>\r\n\r\n                Tập 1: <a href=\"https://www.youtube.com/watch?v=lOMOYaWVGS4&ab_channel=CGVCinemasVietnam\">- Bạn sinh ra với tài năng gì?</a><br>\r\n                Tập 2: <a href=\"https://www.youtube.com/watch?v=C5wPjQO0nyw&ab_channel=CGVCinemasVietnam\">- Đi tìm con đường sự nghiệp đích thực của bạn</a>', N'event5.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (10, N'VALENTINE ĐẾN Lê Độ XEM PHIM RINH GẤU', N' <b>Chi tiết :</b><br>\r\n                Lễ tình nhân Valentine 14/2 năm nay bạn đã có ý tưởng gì chưa? Nếu chưa thì đưa \"một nửa thân yêu\" của mình đến Lê Độ xem phim sẵn tiện rinh luôn một em gấu bé bé, xinh xinh về nào. Duy nhất trong ngày Valentine 14/2/2019, tất cả các khách hàng khi đến xem phim tại Lê Độ Cinema mua vé xem phim ghế Sweetbox sẽ được tặng 1 móc khóa thú bông vô cùng dễ thương. Điều kiện & điều khoản:<br>\r\n                - Chương trình áp dụng cho tất cả các khách hàng đến xem phim tại Lê Độ Cinema.<br>\r\n                - Chương trình được áp dụng duy nhất vào Thứ năm (14/02/2019).<br>\r\n                - Chương trình chỉ áp dụng cho hình thức mua trực tiếp tại quầy.<br>\r\n                - Với mỗi một hóa đơn/ mua vé xem phim ghế Sweetbox, khách hàng sẽ nhận được 01 móc khóa thú bông.<br>\r\n                - Nhân viên của Lê Độ Cinema không được tham gia chương trình này.<br>\r\n                - Trong mọi trường hợp, mọi quyết định của Lê Độ Cinema là quyết định cuối cùng.<br>\r\n                (*) Mua thêm một phần bắp ngọt lớn với giá 29k thui nhé\r\n                Sản phẩm được bán độc quyền tại Lê Độ khu vực Hồ Chí Minh, Hà Nội, Bình Dương và Đồng Nai. Số lượng có hạn.', N'event6.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
INSERT [dbo].[post] ([id], [title], [description], [image], [created_at], [id_cpost]) VALUES (11, N'QUÀ TẶNG SINH NHẬT THÀNH VIÊN', N'<b>Chi tiết :</b><br>\r\n                Khi sở hữu thẻ thành viên Lê Độ, bạn sẽ nhận được quà tặng trong tháng sinh nhật của mình. Phần quà của bạn có giá trị tăng theo hạng thẻ sở hữu:<br>\r\n                - Thành viên hạng thẻ M-Star: tặng 01 Combo bắp nước.<br>\r\n                - Thành viên hạng thẻ M-Gold: tặng 01 cặp vé xem phim.<br>\r\n                - Thành viên hạng thẻ M-Diamond: tặng 01 Combo bắp nước và 01 cặp vé xem phim.<br>\r\n                <b> Điều kiện & điều khoản: </b><br>\r\n                - 1. Chương trình chỉ áp dụng cho thành viên Lê Độ Cinema. Vui lòng xuất trình thẻ thành viên để nhận ưu đãi.<br>\r\n                - 2. Khách hàng có thể nhận phần quà sinh nhật vào ngày bất kì của tháng sinh nhật.<br>\r\n                - 3. Thông tin về ngày tháng sinh nhật của thẻ thành viên phải khớp với ngày tháng sinh nhật thực tế.<br>\r\n                - 4. Một khách hàng chỉ có thể nhận 01 phần quà trong tháng sinh nhật của mình.<br>\r\n                - 5. Trong mọi trường hợp, quyết định của Lê Độ Cinema là quyết định cuối cùng.<br>\r\n                (*) Mua thêm một phần bắp ngọt lớn với giá 29k thui nhé\r\n                Sản phẩm được bán độc quyền tại Lê Độ khu vực Hồ Chí Minh, Hà Nội, Bình Dương và Đồng Nai. Số lượng có hạn.', N'event7.png', CAST(N'2021-07-19T12:11:54.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[post] OFF
GO
SET IDENTITY_INSERT [dbo].[ratings] ON 

INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (18, 1, N'a', 6, CAST(N'2021-08-05T15:37:04.753' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (19, 1, N'hi', 6, CAST(N'2021-08-05T15:40:27.403' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (20, 1, N'helo', 6, CAST(N'2021-08-05T15:45:29.973' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (21, 1, N'hi', 6, CAST(N'2021-08-05T15:46:59.783' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (22, 1, N'helo', 6, CAST(N'2021-08-05T15:47:28.880' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (23, 1, N'a', 6, CAST(N'2021-08-05T16:16:16.333' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (24, 1, N'Hay dó', 6, CAST(N'2021-08-05T16:16:20.377' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (25, 2, N'a', 6, CAST(N'2021-08-05T16:17:40.000' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (26, 2, N'hi', 6, CAST(N'2021-08-05T16:17:42.637' AS DateTime), N'minhnha120')
INSERT [dbo].[ratings] ([id], [film_id], [rate], [id_user], [created_time], [name_user]) VALUES (27, 1, N'hay', 7, CAST(N'2021-08-05T22:33:12.870' AS DateTime), N'minhnha120')
SET IDENTITY_INSERT [dbo].[ratings] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([id], [role_name]) VALUES (1, N'Admin')
INSERT [dbo].[role] ([id], [role_name]) VALUES (2, N'Nhân Viên')
INSERT [dbo].[role] ([id], [role_name]) VALUES (3, N'Khách Hàng')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[room] ON 

INSERT [dbo].[room] ([id], [room_name]) VALUES (1, N'STD')
INSERT [dbo].[room] ([id], [room_name]) VALUES (2, N'SWEETBOX')
INSERT [dbo].[room] ([id], [room_name]) VALUES (3, N'IMAX')
SET IDENTITY_INSERT [dbo].[room] OFF
GO
SET IDENTITY_INSERT [dbo].[schedules] ON 

INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (1, 1, CAST(N'2021-01-22' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (2, 1, CAST(N'2021-02-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (3, 3, CAST(N'2021-03-23' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (4, 5, CAST(N'2021-04-22' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (5, 4, CAST(N'2021-05-26' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (6, 4, CAST(N'2021-06-27' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (7, 5, CAST(N'2021-07-28' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (8, 1, CAST(N'2021-01-27' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (9, 3, CAST(N'2021-02-27' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (10, 4, CAST(N'2021-03-27' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (11, 1, CAST(N'2021-04-28' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (12, 3, CAST(N'2021-05-29' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (13, 1, CAST(N'2021-06-30' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (14, 3, CAST(N'2021-07-30' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (15, 5, CAST(N'2021-05-31' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (16, 4, CAST(N'2021-04-30' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (17, 5, CAST(N'2021-03-29' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (18, 5, CAST(N'2021-03-05' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (19, 5, CAST(N'2021-01-30' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (20, 6, CAST(N'2021-02-28' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (21, 1, CAST(N'2021-03-17' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (22, 2, CAST(N'2021-04-17' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (23, 1, CAST(N'2021-05-18' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (24, 1, CAST(N'2021-08-19' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (27, 2, CAST(N'2021-08-19' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (28, 3, CAST(N'2021-08-19' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (29, 1, CAST(N'2021-08-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (30, 8, CAST(N'2021-08-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (31, 2, CAST(N'2021-08-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (32, 3, CAST(N'2021-08-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (33, 5, CAST(N'2021-08-20' AS Date))
INSERT [dbo].[schedules] ([id], [film_id], [dateschedule]) VALUES (34, 9, CAST(N'2021-08-20' AS Date))
SET IDENTITY_INSERT [dbo].[schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[seats] ON 

INSERT [dbo].[seats] ([id], [seat_name]) VALUES (10, N'A1')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (11, N'A2')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (12, N'A3')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (13, N'A4')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (14, N'A5')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (15, N'B1')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (16, N'B2')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (17, N'B3')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (18, N'B4')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (19, N'B5')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (26, N'C1')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (27, N'C2')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (28, N'C3')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (29, N'C4')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (30, N'C5')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (31, N'D1')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (32, N'D2')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (33, N'D3')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (34, N'D4')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (35, N'D5')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (36, N'E1')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (37, N'E2')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (38, N'E3')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (39, N'E4')
INSERT [dbo].[seats] ([id], [seat_name]) VALUES (40, N'E5')
SET IDENTITY_INSERT [dbo].[seats] OFF
GO
SET IDENTITY_INSERT [dbo].[showtimes] ON 

INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (1, 1, CAST(N'18:45:00' AS Time), CAST(N'19:45:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (2, 2, CAST(N'18:45:00' AS Time), CAST(N'19:45:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (3, 1, CAST(N'20:15:00' AS Time), CAST(N'21:15:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (4, 2, CAST(N'20:30:00' AS Time), CAST(N'21:30:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (5, 3, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (6, 3, CAST(N'16:41:23' AS Time), CAST(N'20:07:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (7, 14, CAST(N'12:45:00' AS Time), CAST(N'13:45:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (8, 14, CAST(N'14:46:00' AS Time), CAST(N'16:46:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (9, 19, CAST(N'04:29:00' AS Time), CAST(N'07:29:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (10, 22, CAST(N'12:54:00' AS Time), CAST(N'14:54:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (11, 24, CAST(N'20:57:00' AS Time), CAST(N'22:57:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (12, 21, CAST(N'20:02:00' AS Time), CAST(N'22:03:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (13, 21, CAST(N'10:45:00' AS Time), CAST(N'11:45:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (14, 21, CAST(N'09:18:00' AS Time), CAST(N'10:18:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (15, 22, CAST(N'16:36:00' AS Time), CAST(N'17:36:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (16, 23, CAST(N'18:47:00' AS Time), CAST(N'20:47:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (18, 24, CAST(N'15:54:00' AS Time), CAST(N'16:54:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (19, 24, CAST(N'13:12:00' AS Time), CAST(N'14:13:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (20, 24, CAST(N'02:24:00' AS Time), CAST(N'04:24:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (21, 27, CAST(N'14:09:00' AS Time), CAST(N'16:09:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (24, 24, CAST(N'16:11:00' AS Time), CAST(N'17:12:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (25, 29, CAST(N'08:53:00' AS Time), CAST(N'10:53:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (26, 29, CAST(N'09:53:00' AS Time), CAST(N'11:53:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (27, 29, CAST(N'00:56:00' AS Time), CAST(N'01:56:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (28, 30, CAST(N'14:19:00' AS Time), CAST(N'16:19:00' AS Time))
INSERT [dbo].[showtimes] ([id], [schedule_id], [start_time], [end_time]) VALUES (29, 31, CAST(N'18:30:00' AS Time), CAST(N'20:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[showtimes] OFF
GO
SET IDENTITY_INSERT [dbo].[usercgv] ON 

INSERT [dbo].[usercgv] ([id], [email], [is_active], [password], [phonenumber], [role_id], [username]) VALUES (1, N'admin@gmail.com', 1, N'25f9e794323b453885f5181f1b624db', N'0394073758', 1, N'Quản trị viên')
INSERT [dbo].[usercgv] ([id], [email], [is_active], [password], [phonenumber], [role_id], [username]) VALUES (3, N'nhanvien12@gmail.com', 1, N'2a2fa4fe2fa737f129ef2d127b861b7e', N'0379135465', 2, N'Nhân Viên')
INSERT [dbo].[usercgv] ([id], [email], [is_active], [password], [phonenumber], [role_id], [username]) VALUES (6, N'minhminhnha2308@gmail.com', 1, N'25f9e794323b453885f5181f1b624db', N'0379572434', 3, N'minhnha120')
INSERT [dbo].[usercgv] ([id], [email], [is_active], [password], [phonenumber], [role_id], [username]) VALUES (7, N'nminhnha2308@gmail.com', 1, N'25f9e794323b453885f5181f1b624db', N'0379572434', 3, N'minhnha120')
INSERT [dbo].[usercgv] ([id], [email], [is_active], [password], [phonenumber], [role_id], [username]) VALUES (40, N'nguyencaonguyen@gmail.com', 1, N'e9b654fb4686c0daf12e9472a65c477', N'0394073759', 3, N'Lê Van A')
SET IDENTITY_INSERT [dbo].[usercgv] OFF
GO
ALTER TABLE [dbo].[booking] ADD  DEFAULT ('0') FOR [status]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [description]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [director]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [duration]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [film_name]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [image]
GO
ALTER TABLE [dbo].[films] ADD  DEFAULT (NULL) FOR [trailer]
GO
ALTER TABLE [dbo].[post] ADD  DEFAULT (NULL) FOR [image]
GO
ALTER TABLE [dbo].[post] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[ratings] ADD  DEFAULT (NULL) FOR [film_id]
GO
ALTER TABLE [dbo].[ratings] ADD  DEFAULT (NULL) FOR [rate]
GO
ALTER TABLE [dbo].[ratings] ADD  DEFAULT (NULL) FOR [id_user]
GO
ALTER TABLE [dbo].[ratings] ADD  DEFAULT (getdate()) FOR [created_time]
GO
ALTER TABLE [dbo].[role] ADD  DEFAULT (NULL) FOR [role_name]
GO
ALTER TABLE [dbo].[room] ADD  DEFAULT (NULL) FOR [room_name]
GO
ALTER TABLE [dbo].[schedules] ADD  DEFAULT (NULL) FOR [film_id]
GO
ALTER TABLE [dbo].[schedules] ADD  DEFAULT (NULL) FOR [dateschedule]
GO
ALTER TABLE [dbo].[seats] ADD  DEFAULT (NULL) FOR [seat_name]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [is_active]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [password]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [phonenumber]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [role_id]
GO
ALTER TABLE [dbo].[usercgv] ADD  DEFAULT (NULL) FOR [username]
GO
ALTER TABLE [dbo].[booking]  WITH CHECK ADD  CONSTRAINT [booking_ibfk_1] FOREIGN KEY([film_id])
REFERENCES [dbo].[films] ([id])
GO
ALTER TABLE [dbo].[booking] CHECK CONSTRAINT [booking_ibfk_1]
GO
ALTER TABLE [dbo].[booking]  WITH CHECK ADD  CONSTRAINT [booking_ibfk_4] FOREIGN KEY([showtime_id])
REFERENCES [dbo].[showtimes] ([id])
GO
ALTER TABLE [dbo].[booking] CHECK CONSTRAINT [booking_ibfk_4]
GO
ALTER TABLE [dbo].[films]  WITH CHECK ADD  CONSTRAINT [fk_ten] FOREIGN KEY([id_cfilm])
REFERENCES [dbo].[category_film] ([id])
GO
ALTER TABLE [dbo].[films] CHECK CONSTRAINT [fk_ten]
GO
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [fk_post] FOREIGN KEY([id_cpost])
REFERENCES [dbo].[category_post] ([id])
GO
ALTER TABLE [dbo].[post] CHECK CONSTRAINT [fk_post]
GO
ALTER TABLE [dbo].[ratings]  WITH CHECK ADD  CONSTRAINT [fk_film] FOREIGN KEY([film_id])
REFERENCES [dbo].[films] ([id])
GO
ALTER TABLE [dbo].[ratings] CHECK CONSTRAINT [fk_film]
GO
ALTER TABLE [dbo].[ratings]  WITH CHECK ADD  CONSTRAINT [fk_user] FOREIGN KEY([id_user])
REFERENCES [dbo].[usercgv] ([id])
GO
ALTER TABLE [dbo].[ratings] CHECK CONSTRAINT [fk_user]
GO
ALTER TABLE [dbo].[schedules]  WITH CHECK ADD  CONSTRAINT [fk_filmid] FOREIGN KEY([film_id])
REFERENCES [dbo].[films] ([id])
GO
ALTER TABLE [dbo].[schedules] CHECK CONSTRAINT [fk_filmid]
GO
ALTER TABLE [dbo].[usercgv]  WITH CHECK ADD  CONSTRAINT [fk_userrole] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[usercgv] CHECK CONSTRAINT [fk_userrole]
GO
USE [master]
GO
ALTER DATABASE [CGV] SET  READ_WRITE 
GO
