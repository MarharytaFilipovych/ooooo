using RecipeManager.Events;

namespace RecipeManager.EventPublishing;

public class EventPublisher : IEventPublisher
{
    private readonly List<IEventSubscriber> _subscribers = new();

    public void Subscribe(IEventSubscriber subscriber)
    {
        if (!_subscribers.Contains(subscriber))
        {
            _subscribers.Add(subscriber);
        }
    }

    public void Publish(IEvent eventData)
    {
        foreach (var s in _subscribers)
        {
            s.OnEvent(eventData);
        }
    }
}