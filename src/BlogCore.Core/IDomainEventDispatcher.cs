namespace BlogCore.Core
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(DomainEventBase domainEvent);
    }
}