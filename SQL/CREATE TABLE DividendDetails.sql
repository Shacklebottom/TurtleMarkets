CREATE TABLE DividendDetails (
	Id int PRIMARY KEY identity(1,1),
	Ticker VARCHAR(50) null,
	PayoutPerShare DECIMAL null,
	DividendType VARCHAR(50) null,
	PayoutFrequency int null,
	DividendDeclaration date null,
	OpenBeforeDividend date null,
	PayoutDate date null,
	OwnBeforeDate date null
	)
	@DividendDetails int