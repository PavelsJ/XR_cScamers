namespace Interaface
{
    public interface IGameEvent
    {
        bool IsScam { get; }
        bool IsChainActive { get; }
        void PlayerChoice(bool choseTrue);
        void StartEvent();
        void EndEvent();
    }
    
    public interface IEventData
    {
        bool IsScam { get; }
    }
    
}