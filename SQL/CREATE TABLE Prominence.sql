CREATE TABLE Prominence (
	Id int PRIMARY KEY identity(1,1),
	Ticker VARCHAR(50) null,
	Price float null,
	ChangeAmount float null,
	ChangePercentage VARCHAR(50) null,
	Volume bigint null
)