using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "SpamEventData", menuName = "Scriptable Objects/SpamEventData")]
public class SpamEventData : EventData
{
    [Header("Spam title")]
    [TextArea(1, 2)] public string subject;

    [Header("Letter spam")]
    public Sprite spamSprite;
}
