USE [KosarkaDb]
GO

/****** Object:  Index [NonClusteredIndexKosarkas]    Script Date: 02/06/2021 22:11:35 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndexKosarkas] ON [dbo].[Kosarkas]
(
	[BrojDresaKosarkasa] ASC,
	[Klub_IdKluba] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


select * from Kosarkas where BrojDresaKosarkasa = 10 and Klub_IdKluba = 10;