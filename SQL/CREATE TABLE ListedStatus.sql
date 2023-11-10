CREATE TABLE ListedStatus (
	Id int PRIMARY KEY identity(1,1),
	Ticker VARCHAR(50) null,
	[Name] text null,
	Exchange VARCHAR(50) null,
	[Type] VARCHAR(50) null,
	IPOdate date null,
	DelistingDate date null,
	[Status] VARCHAR(50) null,
)
declare @MarketData int