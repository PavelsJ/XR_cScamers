using System.Collections.Generic;
using Interaface;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneEventData", menuName = "Scriptable Objects/PhoneEventData")]
public class PhoneEventData : EventData
{
    [Header("Phone Event")]
    [SerializeField] private bool isMessage;
    public bool IsMessage => isMessage;  
}
