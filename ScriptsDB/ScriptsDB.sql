USE [master]
GO
/****** Object:  Database [SanaWebShop]    Script Date: 9/22/2023 5:30:14 PM ******/
CREATE DATABASE [SanaWebShop]
GO
USE [SanaWebShop]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CodeProduct] [nvarchar](50) NOT NULL,
	[IdOrder] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomer] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdCategories]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodeProduct] [nvarchar](50) NOT NULL,
	[IdCategory] [int] NOT NULL,
 CONSTRAINT [PK_ProdCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/22/2023 5:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Code] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Price] [numeric](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Manual', N'Herramientas Manuales')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Medición', N'Herramientas de Medición')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Corte', N'Herramientas de Corte')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Impacto', N'Herramientas de Impacto')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (5, N'Neumática', N'Herramientas neumáticas')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name]) VALUES (1, N'WILTAUR')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([Id], [Quantity], [CodeProduct], [IdOrder]) VALUES (1, 2, N'PR01', 1)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [CodeProduct], [IdOrder]) VALUES (3, 3, N'PR10', 1)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [CodeProduct], [IdOrder]) VALUES (4, 2, N'PR12', 1)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [CodeProduct], [IdOrder]) VALUES (5, 4, N'PR21', 1)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [IdCustomer], [Status]) VALUES (1, 1, 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ProdCategories] ON 

INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (1, N'PR01', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (2, N'PR01', 3)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (5, N'PR02', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (6, N'PR02', 4)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (7, N'PR03', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (8, N'PR04', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (9, N'PR05', 5)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (10, N'PR06', 5)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (11, N'PR07', 4)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (14, N'PR08', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (15, N'PR09', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (17, N'PR10', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (16, N'PR10', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (18, N'PR10', 3)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (21, N'PR11', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (19, N'PR11', 4)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (22, N'PR12', 3)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (23, N'PR13', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (24, N'PR14', 5)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (25, N'PR15', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (26, N'PR16', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (27, N'PR17', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (28, N'PR17', 5)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (29, N'PR18', 4)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (30, N'PR19', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (31, N'PR19', 3)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (32, N'PR20', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (33, N'PR21', 2)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (34, N'PR22', 1)
INSERT [dbo].[ProdCategories] ([Id], [CodeProduct], [IdCategory]) VALUES (35, N'PR22', 5)
SET IDENTITY_INSERT [dbo].[ProdCategories] OFF
GO
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR01', N'Product_1', N'ProductDes_1', CAST(5.25 AS Numeric(18, 2)), 50)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR02', N'Product_2', N'ProductDes_2', CAST(7.30 AS Numeric(18, 2)), 100)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR03', N'Product_3', N'ProductDes_3', CAST(2.45 AS Numeric(18, 2)), 80)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR04', N'Product_4', N'ProductDes_4', CAST(22.45 AS Numeric(18, 2)), 60)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR05', N'Product_5', N'ProductDes_5', CAST(26.50 AS Numeric(18, 2)), 160)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR06', N'Product_6', N'ProductDes_6', CAST(16.34 AS Numeric(18, 2)), 130)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR07', N'Product_7', N'ProductDes_7', CAST(15.37 AS Numeric(18, 2)), 150)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR08', N'Product_8', N'ProductDes_8', CAST(30.17 AS Numeric(18, 2)), 10)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR09', N'Product_9', N'ProductDes_9', CAST(3.67 AS Numeric(18, 2)), 100)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR10', N'Product_10', N'ProductDes_10', CAST(9.48 AS Numeric(18, 2)), 90)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR11', N'Product_11', N'ProductDes_11', CAST(19.40 AS Numeric(18, 2)), 111)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR12', N'Product_12', N'ProductDes_12', CAST(3.20 AS Numeric(18, 2)), 5)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR13', N'Product 13', N'ProductDes 13', CAST(13.20 AS Numeric(18, 2)), 55)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR14', N'Product 14', N'ProductDes 14', CAST(21.20 AS Numeric(18, 2)), 110)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR15', N'Product 15', N'ProductDes 15', CAST(11.25 AS Numeric(18, 2)), 10)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR16', N'Product 16', N'ProductDes 16', CAST(16.10 AS Numeric(18, 2)), 107)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR17', N'Product 17', N'ProductDes 17', CAST(10.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR18', N'Product 18', N'ProductDes 18', CAST(18.00 AS Numeric(18, 2)), 56)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR19', N'Product 19', N'ProductDes 19', CAST(19.00 AS Numeric(18, 2)), 40)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR20', N'Product 20', N'ProductDes 20', CAST(9.00 AS Numeric(18, 2)), 94)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR21', N'Product 21', N'ProductDes 21', CAST(4.50 AS Numeric(18, 2)), 20)
INSERT [dbo].[Products] ([Code], [Title], [Description], [Price], [Stock]) VALUES (N'PR22', N'Product 22', N'ProductDes 22', CAST(1.05 AS Numeric(18, 2)), 200)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Categories]    Script Date: 9/22/2023 5:30:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categories] ON [dbo].[Categories]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cust_Name]    Script Date: 9/22/2023 5:30:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cust_Name] ON [dbo].[Customers]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderDetails]    Script Date: 9/22/2023 5:30:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderDetails] ON [dbo].[OrderDetails]
(
	[CodeProduct] ASC,
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProdCategories]    Script Date: 9/22/2023 5:30:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProdCategories] ON [dbo].[ProdCategories]
(
	[CodeProduct] ASC,
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrDet_Orders] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[Orders] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrDet_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrDet_Products] FOREIGN KEY([CodeProduct])
REFERENCES [dbo].[Products] ([Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrDet_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customer] FOREIGN KEY([IdCustomer])
REFERENCES [dbo].[Customers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customer]
GO
ALTER TABLE [dbo].[ProdCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProdCat_Categories] FOREIGN KEY([IdCategory])
REFERENCES [dbo].[Categories] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProdCategories] CHECK CONSTRAINT [FK_ProdCat_Categories]
GO
ALTER TABLE [dbo].[ProdCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProdCat_Products] FOREIGN KEY([CodeProduct])
REFERENCES [dbo].[Products] ([Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProdCategories] CHECK CONSTRAINT [FK_ProdCat_Products]
GO
USE [master]
GO
ALTER DATABASE [SanaWebShop] SET  READ_WRITE 
GO
