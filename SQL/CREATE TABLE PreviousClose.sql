CREATE TABLE PreviousClose(
	Id int primary key identity(1,1),
	Ticker varchar(50) null, 
	[Date] datetime null,
	[Open] float null,
	[Close] float null,
	[High] float null,
	[Low] float null,
	[Volume] bigint null
	)
declare @MarketData int