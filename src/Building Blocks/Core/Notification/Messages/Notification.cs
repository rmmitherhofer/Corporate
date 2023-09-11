using Microsoft.Extensions.Logging;

namespace Notifications.Messages;

public class Notification : Message
{
    /// <summary>
    /// Id da notificação
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Data hora da notificação
    /// </summary>
    public DateTime Timestamp { get; private set; }
    public LogLevel Level { get; private set; }
    /// <summary>
    /// Tipo
    /// </summary>
    public string? Key { get; private set; }
    /// <summary>
    /// Valor
    /// </summary>
    public string Value { get; private set; }

    public string Detail { get; private set; }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="type">Tipo</param>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(LogLevel level, string type, string key, string value, string detail) : base(type)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Timestamp = DateTime.Now;
        Level = level;
        Key = key;
        Value = value;
        Detail = detail;
    }
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="type">Tipo</param>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(string type, string key, string value, string detail) : base(type)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Timestamp = DateTime.Now;
        Level = LogLevel.Information;
        Key = key;
        Value = value;
        Detail = detail;
    }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="type">Tipo</param>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(LogLevel level, string type, string key, string value) : base(type)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Timestamp = DateTime.Now;
        Level = level;
        Key = key;
        Value = value;        
    }
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="type">Tipo</param>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(string type, string key, string value) : base(type)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Timestamp = DateTime.Now;
        Level = LogLevel.Information;
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(LogLevel level, string key, string value)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Level = level;
        Timestamp = DateTime.Now;
        Key = key;
        Value = value;
    }
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="key">Chave</param>
    /// <param name="value">Valor</param>
    public Notification(string key, string value)
    {
        Id = Guid.NewGuid();

        AggregateId = Id;
        Level = LogLevel.Information;
        Timestamp = DateTime.Now;
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="value">Valor</param>
    public Notification(LogLevel level, string value)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Level = level;
        Timestamp = DateTime.Now;
        Value = value;
    }
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="value">Valor</param>
    public Notification(string value)
    {
        Id = Guid.NewGuid();
        AggregateId = Id;
        Timestamp = DateTime.Now;
        Value = value;
    }
}

public abstract class Message
{
    /// <summary>
    /// Tipo de mensagem
    /// </summary>
    public string Type { get; protected set; }
    /// <summary>
    /// Aggregate Id
    /// </summary>
    public Guid AggregateId { get; protected set; }
    /// <summary>
    /// Construtor
    /// </summary>
    protected Message() => Type = GetType().Name;

    protected Message(string type) => Type = type;
}
