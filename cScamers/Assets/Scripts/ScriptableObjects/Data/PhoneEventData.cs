using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneEventData", menuName = "Scriptable Objects/PhoneEventData")]
public class PhoneEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [TextArea(2,5)] public string popup;
    [TextArea(5,10)] public string description;
}
