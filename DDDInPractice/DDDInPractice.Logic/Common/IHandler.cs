namespace DDDInPractice.Logic.Common
{
    public interface IHandler<TEvent>
        where TEvent : IDomainEvent
    {
        void Handle(TEvent domainEvent);
    }
}
