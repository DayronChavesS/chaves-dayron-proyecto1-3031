USE [master]
GO
/****** Object:  Database [chaves-dayron-proyecto1-3031]    Script Date: 22/07/2024 16:35:12 ******/
CREATE DATABASE [chaves-dayron-proyecto1-3031]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'chaves-dayron-proyecto1-3031', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chaves-dayron-proyecto1-3031.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'chaves-dayron-proyecto1-3031_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\chaves-dayron-proyecto1-3031_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [chaves-dayron-proyecto1-3031].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ARITHABORT OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET  DISABLE_BROKER 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET RECOVERY FULL 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET  MULTI_USER 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET DB_CHAINING OFF 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'chaves-dayron-proyecto1-3031', N'ON'
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET QUERY_STORE = ON
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [chaves-dayron-proyecto1-3031]
GO
/****** Object:  Table [dbo].[Preferences]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preferences](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Origin] [varchar](50) NULL,
	[Destination] [varchar](50) NULL,
	[DepartureDate] [varchar](10) NULL,
	[ReturnDate] [varchar](10) NULL,
	[Adults] [tinyint] NULL,
	[Children] [tinyint] NULL,
	[Infants] [tinyint] NULL,
	[TravelClass] [varchar](15) NULL,
	[NonStop] [bit] NULL,
	[MaxPrice] [int] NULL,
 CONSTRAINT [PK_Preference] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserve]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserve](
	[RsrvId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Origin] [varchar](150) NOT NULL,
	[Destination] [varchar](150) NOT NULL,
	[DepartureDate] [varchar](10) NOT NULL,
	[ArrivalDate] [varchar](10) NOT NULL,
	[Class] [varchar](15) NOT NULL,
	[Price] [int] NOT NULL,
	[InCart] [bit] NOT NULL,
	[InHistory] [bit] NOT NULL,
 CONSTRAINT [PK_Reserve] PRIMARY KEY CLUSTERED 
(
	[RsrvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Preferences]  WITH CHECK ADD  CONSTRAINT [FK_Preference_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Preferences] CHECK CONSTRAINT [FK_Preference_User]
GO
ALTER TABLE [dbo].[Reserve]  WITH CHECK ADD  CONSTRAINT [FK_Reserve_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Reserve] CHECK CONSTRAINT [FK_Reserve_User]
GO
/****** Object:  StoredProcedure [dbo].[stpAuthUser]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpAuthUser]
	@Email VARCHAR(50),
	@Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT Email FROM [User] WHERE Email = @Email AND Password =  @Password
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpCheckEmail]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpCheckEmail]
	@Email VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT Email FROM [User] WHERE Email = @Email;
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteAllData]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpDeleteAllData]
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?';
	EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
	EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?';
	DBCC CHECKIDENT ('[User]', RESEED, 0);
	DBCC CHECKIDENT ('[Reserve]', RESEED, 0);
	EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
	EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?';
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpGetAllUsers]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpGetAllUsers]
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT * FROM [User]
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpGetPreferences]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpGetPreferences]
	@UserId int
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT [UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],
		   [Adults],[Children],[Infants],[TravelClass],[NonStop],
		   [MaxPrice]
	FROM [Preferences] 
	WHERE [UserId] = @UserId
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpGetReserve]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpGetReserve]
	@UserId int
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT [RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]
	FROM [Reserve] 
	WHERE [UserId] = @UserId
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpGetUser]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpGetUser]
	@Email VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SELECT [UserId], [Name], [Email], [Password] FROM [User] WHERE [Email] = @Email
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertNewPreferences]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpInsertNewPreferences]
	       @UserId bigint,
		   @Origin varchar(50),
           @Destination varchar(50),
           @DepartureDate varchar(10),
           @ReturnDate varchar(10),
           @Adults tinyint,
           @Children tinyint,
           @Infants tinyint,
           @TravelClass varchar(15),
           @NonStop bit,
           @MaxPrice int
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	SET IDENTITY_INSERT [Preferences] ON
	INSERT INTO [dbo].[Preferences]
           ([UserId]
		   ,[Origin]
           ,[Destination]
           ,[DepartureDate]
           ,[ReturnDate]
           ,[Adults]
           ,[Children]
           ,[Infants]
           ,[TravelClass]
           ,[NonStop]
           ,[MaxPrice])
     VALUES
			(
			@UserId,
			@Origin,
			@Destination,
			@DepartureDate,
			@ReturnDate,
			@Adults,
			@Children,
			@Infants,
			@TravelClass,
			@NonStop,
			@MaxPrice
			)
	SET IDENTITY_INSERT [Preferences] OFF
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertNewReserve]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpInsertNewReserve]
	       @UserId bigint,
		   @Origin varchar(50),
           @Destination varchar(50),
           @DepartureDate varchar(10),
           @ArrivalDate varchar(10),
		   @Class varchar(15),
		   @Price integer,
           @InCart bit,
		   @InHistory bit
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	INSERT INTO [dbo].[Reserve]
           (
			[UserId]
		   ,[Origin]
           ,[Destination]
           ,[DepartureDate]
           ,[ArrivalDate]
		   ,[Class]
		   ,[Price]
           ,[InCart]
           ,[InHistory]
		   )
     VALUES
			(
			@UserId,
			@Origin,
			@Destination,
			@DepartureDate,
			@ArrivalDate,
			@Class,
		    @Price,
			@InCart,
			@InHistory
			)
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertNewUser]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpInsertNewUser]
	@Name VARCHAR(50),
	@Email VARCHAR(50),
	@Password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	     INSERT INTO [User]
          (                    
            Name,
            Email,
            Password                 
          ) 
     VALUES 
          ( 
            @Name,
            @Email,
            @Password
          ) 
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpInsertTestValues]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpInsertTestValues]
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	INSERT INTO [dbo].[User] VALUES ('User One','a@a.a','Pass');
	INSERT INTO [dbo].[User] VALUES ('User Two','b@a.a','Pass');
	INSERT INTO [dbo].[User] VALUES ('User Three','c@a.a','Pass');
	INSERT INTO [dbo].[User] VALUES ('User Four','d@a.a','Pass');
	INSERT INTO [dbo].[User] VALUES ('User Five','e@a.a','Pass');
	
	SET IDENTITY_INSERT [dbo].[Preferences] ON;
	INSERT INTO [dbo].[Preferences] ([UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],[Adults],[Children],[Infants],[TravelClass],[NonStop],[MaxPrice]) VALUES (1,'SJO','ATL','2024-07-22','',1,0,0,'',0,0);
	INSERT INTO [dbo].[Preferences] ([UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],[Adults],[Children],[Infants],[TravelClass],[NonStop],[MaxPrice]) VALUES (2,'LIR','DFW','2024-07-22','',1,0,0,'',0,0);
	INSERT INTO [dbo].[Preferences] ([UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],[Adults],[Children],[Infants],[TravelClass],[NonStop],[MaxPrice]) VALUES (3,'LIO','DEN','2024-07-22','',1,0,0,'',0,0);
	INSERT INTO [dbo].[Preferences] ([UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],[Adults],[Children],[Infants],[TravelClass],[NonStop],[MaxPrice]) VALUES (4,'SYQ','LAX','2024-07-22','',1,0,0,'',0,0);
	INSERT INTO [dbo].[Preferences] ([UserId],[Origin],[Destination],[DepartureDate],[ReturnDate],[Adults],[Children],[Infants],[TravelClass],[NonStop],[MaxPrice]) VALUES (5,'SJO','ORD','2024-07-22','',1,0,0,'',1,0);
	SET IDENTITY_INSERT [dbo].[Preferences] OFF;

	SET IDENTITY_INSERT [dbo].[Reserve] ON;
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (1,1,'SJO','ATL','2024-07-22', '2024-07-24', 'ECONOMY',10000000, 1, 0);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (2,1,'LIR','DFW','2024-01-01', '2024-01-02', 'PREMIUM_ECONOMY',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (3,1,'LIO','DEN','2024-01-03', '2024-01-04', 'BUSINESS',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (4,1,'SYQ','LAX','2024-12-01', '2024-12-02', 'FIRST',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (5,1,'SJO','ORD','2024-12-03', '2024-12-04', 'ECONOMY',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (6,2,'LIR','DFW','2024-07-22', '2024-07-24', 'ECONOMY',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (7,3,'LIO','DEN','2024-07-22', '2024-07-24', 'ECONOMY',10000000, 1, 0);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (8,4,'SYQ','LAX','2024-07-22', '2024-07-24', 'ECONOMY',10000000, 0, 1);
	INSERT INTO [dbo].[Reserve] ([RsrvId],[UserId],[Origin],[Destination],[DepartureDate],[ArrivalDate],[Class],[Price],[InCart],[InHistory]) VALUES (9,5,'SJO','ORD','2024-07-22', '2024-07-24', 'ECONOMY',10000000, 1, 0);
	SET IDENTITY_INSERT [Reserve] OFF;
	-- SQL CODE HERE
END

GO
/****** Object:  StoredProcedure [dbo].[stpUpdatePreferences]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpUpdatePreferences]
	       @UserId bigint,
		   @Origin varchar(50),
           @Destination varchar(50),
           @DepartureDate varchar(10),
           @ReturnDate varchar(10),
           @Adults tinyint,
           @Children tinyint,
           @Infants tinyint,
           @TravelClass varchar(15),
           @NonStop bit,
           @MaxPrice int
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	UPDATE [dbo].[Preferences]
	   SET 
		   [Origin] = @Origin
           ,[Destination] = @Destination
           ,[DepartureDate] = @DepartureDate
           ,[ReturnDate] = @ReturnDate
           ,[Adults] = @Adults
           ,[Children] = @Children
           ,[Infants] = @Infants
           ,[TravelClass] = @TravelClass
           ,[NonStop] = @NonStop
           ,[MaxPrice] = @MaxPrice
	 WHERE [UserId] = @UserId
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateReserve]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpUpdateReserve]
	       @RsrvId bigint
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	UPDATE [dbo].[Reserve]
	   SET 
			[InCart] = 0
           ,[InHistory] = 1
	 WHERE [RsrvId] = @RsrvId
	-- SQL CODE HERE
END
GO
/****** Object:  StoredProcedure [dbo].[stpUpdateUser]    Script Date: 22/07/2024 16:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stpUpdateUser]
	@UserId bigint,
	@Name varchar(50),
	@Email varchar(50),
	@Password varchar(50)
AS
BEGIN
    SET NOCOUNT ON;
	-- SQL CODE HERE
	UPDATE [dbo].[User]
	   SET [Name] = @Name
		  ,[Email] = @Email
		  ,[Password] = @Password
	 WHERE [UserId] = @UserId
	-- SQL CODE HERE
END
GO
USE [master]
GO
ALTER DATABASE [chaves-dayron-proyecto1-3031] SET  READ_WRITE 
GO
