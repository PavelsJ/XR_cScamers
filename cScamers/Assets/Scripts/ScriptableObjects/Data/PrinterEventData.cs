using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PrinterEventData", menuName = "Scriptable Objects/PrinterEventData")]
public class PrinterEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [TextArea(2,5)] public string popup;
    [TextArea(5,10)] public string description;
}
