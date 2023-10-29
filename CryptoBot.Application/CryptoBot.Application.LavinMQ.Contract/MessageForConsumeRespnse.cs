namespace CryptoBot.Application.LavinMQ.Contract;

public class MessageForConsumeRespnse<T> where T : class
{
    public SortedList<ulong, MessageForConsume<T>> SuccessMessages { get; set; } = new();
    public SortedList<ulong, MessageForConsume<T>> ErrorMessages { get; set; } = new();
}