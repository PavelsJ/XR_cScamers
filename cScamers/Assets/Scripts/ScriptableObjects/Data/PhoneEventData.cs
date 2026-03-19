using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneEventData", menuName = "Scriptable Objects/PhoneEventData")]
public class PhoneEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [SerializeField] private bool isMessage;
    
    [Header("Popup Description")]
    [TextArea(2,5)] public string popup;
    
    [Header("Phone Description")]
    [TextArea(4, 10)] public string description;
    
    [Header("Next Events")]
    public PhoneEventData nextEventsIfScammer;
    public PhoneEventData nextEventsIfNormal;
}
