using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "EmailEventData", menuName = "Scriptable Objects/EmailEventData")]
public class EmailEventData : EventData
{
    [Header("Email Adress")]
    [TextArea(2, 4)] public string adress;
    public Sprite prophile;
}
