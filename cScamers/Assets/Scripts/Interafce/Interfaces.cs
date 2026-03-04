namespace Interaface
{
    public interface IGameEvent
    {
        bool IsScam { get; }
        void StartEvent();
        void EndEvent();
    }
    
    public interface IEventData
    {
        bool IsScam { get; }
    }
}