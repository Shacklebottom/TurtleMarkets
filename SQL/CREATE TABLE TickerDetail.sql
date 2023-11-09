create table TickerDetail(
	Id int identity(1, 1) primary key,
	Ticker varchar(50) null,
	Name varchar(50) null,
	Description text null,
	Address varchar(50) null,
	City varchar(50) null,
	[State] varchar(50) null,
	TotalEmployees int null,
	ListDate date null

)

declare @MarketData int