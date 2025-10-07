using RecipeManager.Events;

namespace RecipeManager.EventPublishing;

public interface IEventSubscriber
{
    void OnEvent(IEvent eventData);
}