USE [Inventory]
GO
/****** Object:  StoredProcedure [dbo].[GetProductByCategoryId]    Script Date: 12/01/2020 21:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductByCategoryId]
	@CategoryId int = NULL
AS
BEGIN
	select p.ProductId, p.ProductName, p.CategoryId, p.Price, p.Currency, p.UnitId, p.ProductDescription, c.CategoryName, c.CategoryDescription, u.UnitName, u.UnitDescription 
	from Product p 
	inner join Category c on p.CategoryId = c.CategoryId
	inner join Units u on p.UnitId = u.UnitId 
	where p.CategoryId = @CategoryId
END




GO
/****** Object:  StoredProcedure [dbo].[GetProductByName]    Script Date: 12/01/2020 21:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductByName]
	@ProductName NVARCHAR(100) = NULL
AS
BEGIN
	select p.ProductId, p.ProductName, p.CategoryId, p.Price, p.Currency, p.UnitId, p.ProductDescription, c.CategoryName, c.CategoryDescription, u.UnitName, u.UnitDescription 
	from Product p 
	inner join Category c on p.CategoryId = c.CategoryId
	inner join Units u on p.UnitId = u.UnitId 
	where p.ProductName like '%' + @ProductName + '%'
END




GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/01/2020 21:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[CategoryDescription] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/01/2020 21:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [decimal](10, 4) NOT NULL,
	[Currency] [nvarchar](5) NOT NULL,
	[UnitId] [int] NOT NULL,
	[ProductDescription] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Units]    Script Date: 12/01/2020 21:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](20) NOT NULL,
	[UnitDescription] [nvarchar](100) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (1, N'Soft Drink', N'Drink')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3, N'Milk', N'Milk')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (4, N'Electronic', N'Electronic')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (9, N'Kitchenware', N'Kitchenware')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (12, N'Ornament', N'Necklace')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (1004, N'Laptop', N'Computer')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (1005, N'Vehicle', N'Vehicle')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2004, N'Furniture', N'Furniture')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2006, N'Sports', N'Sports')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2008, N'Dairy product', N'Dairy product')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2009, N'Desktop', N'Computer')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (2010, N'Computer Accessory', N'Computer Accessory')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3009, N'Watch', N'Watch')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3010, N'Television', N'Television')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3011, N'Cell phone', N'Cell phone')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryDescription]) VALUES (3012, N'Camera', N'Digital Camera')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1, N'Amul Butter', 2008, CAST(99.6900 AS Decimal(10, 4)), N'Rs', 2, N'Good product<br />Salty, cheap<br />Tasty')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (4, N'Cake', 2008, CAST(56.0000 AS Decimal(10, 4)), N'€', 2, N'Vanilla flavor<br />Veg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (5, N'Sofa', 2004, CAST(1234.0000 AS Decimal(10, 4)), N'¥', 1007, N'Wooden<br />Durable')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1005, N'Titan', 3009, CAST(2350.0000 AS Decimal(10, 4)), N'Rs', 1007, N'Titan quartz')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1006, N'Titan sonata', 3009, CAST(17398.0000 AS Decimal(10, 4)), N'Rs', 1007, N'Titan sonata')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1007, N'Prestige', 3009, CAST(27398.0000 AS Decimal(10, 4)), N'Rs', 1007, N'Olivin crystal')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1008, N'Sony', 3010, CAST(1000.0000 AS Decimal(10, 4)), N'$', 1007, N'Oled')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1009, N'L.G.', 3010, CAST(2000.0000 AS Decimal(10, 4)), N'$', 1007, N'Oled')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1010, N'Hier', 3010, CAST(500.0000 AS Decimal(10, 4)), N'$', 1007, N'LED')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1011, N'Hitachi', 3010, CAST(100.0000 AS Decimal(10, 4)), N'₣', 1007, N'Oled')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1012, N'Philips', 3010, CAST(100.0000 AS Decimal(10, 4)), N'₣', 1007, N'Oled')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1013, N'Nikon', 3012, CAST(200.0000 AS Decimal(10, 4)), N'$', 1007, N'High Resoluton')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1014, N'Canon', 3012, CAST(250.0000 AS Decimal(10, 4)), N'$', 1007, N'Moderate Resoluton')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1015, N'Kodak', 3012, CAST(250.0000 AS Decimal(10, 4)), N'$', 1007, N'Moderate Resoluton')
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [Price], [Currency], [UnitId], [ProductDescription]) VALUES (1016, N'Sony SLR', 3012, CAST(250.0000 AS Decimal(10, 4)), N'$', 1007, N'Moderate Resoluton')
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Units] ON 

INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (1, N'Kg', N'Kilogram')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (2, N'g', N'Gram')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (4, N'L', N'Litre')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (5, N'm', N'Meter')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (6, N'cm', N'Centi Meter')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (1007, N'Nu', N'Quantity in number')
INSERT [dbo].[Units] ([UnitId], [UnitName], [UnitDescription]) VALUES (1008, N'Pound', N'Pound (UK)')
SET IDENTITY_INSERT [dbo].[Units] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [Uq_CategoryName]    Script Date: 12/01/2020 21:28:01 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [Uq_CategoryName] UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Uq_Product]    Script Date: 12/01/2020 21:28:01 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [Uq_Product] UNIQUE NONCLUSTERED 
(
	[ProductName] ASC,
	[CategoryId] ASC,
	[Currency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Uq_UnitName]    Script Date: 12/01/2020 21:28:01 ******/
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [Uq_UnitName] UNIQUE NONCLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([UnitId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Units]
GO
USE [master]
GO
ALTER DATABASE [Inventory] SET  READ_WRITE 
GO

