USE [ASPCRUD]
GO
/****** Object:  Table [dbo].[detail_cs]    Script Date: 8/6/2018 8:35:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detail_cs](
	[exception] [varchar](50) NULL,
	[material] [varchar](15) NULL,
	[plant] [varchar](4) NULL,
	[storage_location] [varchar](4) NULL,
	[material_description] [varchar](50) NULL,
	[base_unit_of_measure] [varchar](5) NULL,
	[batch] [varchar](10) NULL,
	[unrestricted] [int] NULL,
	[in_quality] [int] NULL,
	[blocked] [int] NULL,
	[total_stock] [int] NULL,
	[market] [varchar](50) NULL,
	[week] [int] NULL,
	[year] [varchar](10) NULL,
	[warehouse] [varchar](10) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_detail_cs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[storage]    Script Date: 8/6/2018 8:35:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[storage](
	[storage_location] [varchar](4) NOT NULL,
	[warehouse] [varchar](10) NULL,
 CONSTRAINT [PK_storage] PRIMARY KEY CLUSTERED 
(
	[storage_location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[detail_cs]  WITH CHECK ADD  CONSTRAINT [FK_detail_cs_storage] FOREIGN KEY([storage_location])
REFERENCES [dbo].[storage] ([storage_location])
GO
ALTER TABLE [dbo].[detail_cs] CHECK CONSTRAINT [FK_detail_cs_storage]
GO
