using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PrinterEventData", menuName = "Scriptable Objects/PrinterEventData")]
public class PrinterEventData : EventData
{
    [Header("Spam")] 
    public bool isSpam = false;
    public Sprite spamSprite;
}
