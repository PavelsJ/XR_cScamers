using Interaface;
using UnityEngine;

public class EventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    public EventManager.EventType deviceType;
    
    [Header("Popup Description")]
    [TextArea(2,5)] public string popup;
    
    [Header("Email Description")]
    [TextArea(4, 10)] public string description;
    
    [Header("Next Events")]
    public EventData nextEventsIfScammer;
    public EventData nextEventsIfNormal;
}
