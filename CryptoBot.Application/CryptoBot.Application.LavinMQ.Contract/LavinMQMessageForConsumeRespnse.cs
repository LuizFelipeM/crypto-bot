using System.Text;
using System.Text.Json;
using RabbitMQ.Client.Events;

namespace CryptoBot.Application.LavinMQ.Contract;

public class LavinMQMessageForConsumeResponse<T> where T : class
{
    public SortedList<ulong, LavinMQMessageForConsume<T>> SuccessMessages { get; set; } = new();
    public SortedList<ulong, LavinMQMessageForConsume<T>> ErrorMessages { get; set; } = new();
}