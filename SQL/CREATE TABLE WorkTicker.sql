CREATE TABLE WorkTicker (
	Id int PRIMARY KEY identity(1,1),
	Ticker VARCHAR(50) null,
	[Open] FLOAT null,
	[Close] FLOAT null,
	High FLOAT null,
	Low FLOAT null,
	Buy int null,
	Sell int null,
	Hold int null
)