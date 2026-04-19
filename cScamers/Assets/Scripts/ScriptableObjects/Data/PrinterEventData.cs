using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PrinterEventData", menuName = "Scriptable Objects/PrinterEventData")]
public class PrinterEventData : EventData
{
    [Header("Letter title")]
    [TextArea(1, 2)] public string subject;
}
