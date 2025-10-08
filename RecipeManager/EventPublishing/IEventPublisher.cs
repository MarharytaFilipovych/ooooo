using RecipeManager.Events;

namespace RecipeManager.EventPublishing;

public interface IEventPublisher
{
    void Subscribe(IEventSubscriber subscriber);
    void Publish(IEvent eventData);
}