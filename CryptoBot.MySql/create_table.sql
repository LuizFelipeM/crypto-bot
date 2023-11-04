drop table if exists kline;
create table if not exists Kline (
    Id SERIAL primary key,
    OpenTime TIMESTAMP not null,
    CloseTime TIMESTAMP not null,
    Symbol varchar(10) not null,
    `Interval` varchar(2) not null,
    OpenPrice numeric not null,
    ClosePrice numeric not null,
    HighPrice numeric not null,
    LowPrice numeric not null,
    BaseAssetVolume numeric not null,
    NumberOfTrades bigint not null,
    IsKlineClosed boolean not null,
    QuoteAssetVolume numeric not null,
    TakerBuyBaseAssetVolume numeric not null,
    TakerBuyQuoteAssetVolume numeric not null,
    unique (OpenTime, CloseTime),
    index (OpenTime),
    index (CloseTime),
    index (OpenTime, CloseTime)
);