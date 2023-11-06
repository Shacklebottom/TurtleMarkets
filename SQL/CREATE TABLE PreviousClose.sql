CREATE TABLE PreviousClose(
	Id int primary key identity(1,1),
	Ticker varchar(50), 
	[Date] datetime,
	[Open] money,
	[Close] money,
	[High] money,
	[Low] money,
	[Volume] bigint)