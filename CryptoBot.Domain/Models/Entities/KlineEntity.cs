﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoBot.Domain.Models.Entities;

public class KlineEntity
{
    public KlineEntity() => CreatedAt = DateTime.UtcNow;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required DateTime OpenTime { get; set; }
    public required DateTime CloseTime { get; set; }
    public required string Symbol { get; set; }
    public required string Interval { get; set; }
    public required double OpenPrice { get; set; }
    public required double ClosePrice { get; set; }
    public required double HighPrice { get; set; }
    public required double LowPrice { get; set; }
    public required double BaseAssetVolume { get; set; }
    public required long NumberOfTrades { get; set; }
    public required bool IsKlineClosed { get; set; }
    public required double QuoteAssetVolume { get; set; }
    public required double TakerBuyBaseAssetVolume { get; set; }
    public required double TakerBuyQuoteAssetVolume { get; set; }
    public required DateTime CreatedAt { get; set; }
}
