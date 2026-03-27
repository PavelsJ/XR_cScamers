using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneEventManager : MonoBehaviour, Interaface.IGameEvent
{
    [Header("UI")]
    public GameObject eventlUI;
    public TextMeshProUGUI descriptionText;
    
    [Header("Phone Interactions")]
    [SerializeField] private PhoneBase phoneBase;

    [Header("Phone Database")]
    public List<PhoneEventData> phoneEvents;
    private EventData currentData;

    public void GenerateEvent()
    {
        if (phoneEvents == null || phoneEvents.Count == 0)
        {
            Debug.LogError("No Email Events assigned!");
            return;
        }

        eventlUI.SetActive(true);
        GenerateCall();
    }
    
    void GenerateCall()
    {
        currentData = phoneEvents[Random.Range(0, phoneEvents.Count)];
        
        descriptionText.text = currentData.description;

        Debug.Log($"Generated Phone: {currentData.name} | Scam: {currentData.IsScam}");

        PhoneEventData eventData = currentData as PhoneEventData;
        bool isMessage = eventData != null && eventData.IsMessage;

        if (isMessage) phoneBase.SpawnPhoneMessage(currentData);
        else phoneBase.SpawnPhoneCall(currentData);
    }
    
    public void UpdateEvent(EventData data)
    {
        currentData = data;
        
        descriptionText.text = currentData.description;
        
        PhoneEventData eventData = currentData as PhoneEventData;
        bool isMessage = eventData != null && eventData.IsMessage;

        if (isMessage) phoneBase.UpdatePhoneMessage(currentData);
        else phoneBase.UpdatePhoneCall(currentData);
        
        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        phoneBase.ClearPhone();
    }
    
    public EventData GetCurrentEvent()
    {
        return currentData;
    }
}