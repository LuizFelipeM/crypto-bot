drop table if exists Klines;
create table if not exists Klines (
    Id SERIAL primary key,
    OpenTime bigint not null,
    CloseTime bigint not null,
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
    index (OpenTime),
    index (CloseTime),
    index (OpenTime, CloseTime),
    index (Symbol),
    index (`Interval`),
    index (OpenTime, CloseTime, Symbol, `Interval`)
);

drop table if EXISTS Users;
create TABLE IF NOT EXISTS Users (
    Id SERIAL primary key,
    UserName varchar(256) not null,
    `PasswordHash` varchar(256) not null,
    `Role` LONGTEXT,
    index (UserName),
    unique (UserName)
);