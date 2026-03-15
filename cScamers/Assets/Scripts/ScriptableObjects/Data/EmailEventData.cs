using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "EmailEventData", menuName = "Scriptable Objects/EmailEventData")]
public class EmailEventData : ScriptableObject, IEventData
{
    [SerializeField] private bool isScam;
    public bool IsScam => isScam;  
    
    [TextArea(2,5)] public string popup;
    [TextArea(5,10)] public string description;
}
