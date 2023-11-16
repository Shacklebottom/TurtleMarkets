use MarketData; 
create table MarketStatus (
	Id int primary key identity(1,1),
	MarketType varchar(50) null,
	Region varchar(50) null,
	Exchange varchar(50) null,
	LocalOpen datetime null,
	LocalClose datetime null,
	TimeOffset int null,
	Status varchar(50) null,
	Notes varchar(max) null
)