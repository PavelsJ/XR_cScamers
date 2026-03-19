using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PrinterEventData", menuName = "Scriptable Objects/PrinterEventData")]
public class PrinterEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [Header("Popup Description")]
    [TextArea(2,5)] public string popup;
    
    [Header("Printer Description")]
    [TextArea(4, 10)] public string description;
    
    [Header("Next Events")]
    public PrinterEventData nextEventsIfScammer;
    public PrinterEventData nextEventsIfNormal;
}
