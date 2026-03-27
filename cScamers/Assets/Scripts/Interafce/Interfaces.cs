namespace Interaface
{
    public interface IGameEvent
    {
        void GenerateEvent();
        void UpdateEvent(EventData data);
        void EndEvent();
        EventData GetCurrentEvent();
    }
    
    public interface IEventData
    {
        bool IsScam { get; }
    }
    
}