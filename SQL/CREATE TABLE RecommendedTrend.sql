CREATE TABLE RecommendedTrend (
	Id int PRIMARY KEY identity(1,1),
	Ticker VARCHAR(50) null,
	Buy int null,
	Hold int null,
	Period date null,
	Sell int null,
	StrongBuy int null,
	StrongSell int null
)