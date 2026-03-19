using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "EmailEventData", menuName = "Scriptable Objects/EmailEventData")]
public class EmailEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [Header("Popup Description")]
    [TextArea(2,5)] public string popup;
    
    [Header("Email Description")]
    [TextArea(4, 10)] public string description;
    
    [Header("Next Events")]
    public EmailEventData nextEventsIfScammer;
    public EmailEventData nextEventsIfNormal;
}
