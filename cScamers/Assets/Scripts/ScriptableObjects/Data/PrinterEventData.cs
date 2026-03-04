using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PrinterEventData", menuName = "Scriptable Objects/PrinterEventData")]
public class PrinterEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [TextArea(5,10)]
    public string description;
}
