CREATE TABLE MarketHoliday (
	Id int PRIMARY KEY identity(1,1),
	Exchange VARCHAR(50) null,
	[Date] datetime null,
	Holiday VARCHAR(50) null,
	[Status] VARCHAR(50) null,
	[Open] VARCHAR(50) null,
	[Close] VARCHAR(50) null
	)
	DECLARE @MarketHoliday int