namespace BlogCore.Core
{
    public interface IHandler<in TEntity> where TEntity : DomainEventBase
    {
        void Handle(TEntity domainEvent);
    }
}