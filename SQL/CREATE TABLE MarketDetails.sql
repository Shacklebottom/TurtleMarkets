create table MarketDetails(
	Id int identity(1,1) primary key,
	Ticker varchar(50) null,
	AdjustedClose float null,
    [Close] float null,
    [Date] datetime null,
    DividendAmount float null,
    [High] float null,
    [Low] float null,
    [Open] float null,
    SplitCoefficient float null,
    Volume float null,
    VolumeWeighted float null
)