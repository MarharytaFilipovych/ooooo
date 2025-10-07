using RecipeManager.Events;
using System.Text.Json;

namespace RecipeManager.EventPublishing;

public class EventWriter : IEventSubscriber
{
    private readonly string _filePath;
    private readonly object _lock = new();

    public EventWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void OnEvent(IEvent eventData)
    {
        var logEntry = new
        {
            @event = eventData.EventName,
            timestamp = eventData.Timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            @params = eventData.GetParams()
        };

        var json = JsonSerializer.Serialize(logEntry);

        lock (_lock)
        {
            File.AppendAllText(_filePath, json + Environment.NewLine);
        }
    }
}