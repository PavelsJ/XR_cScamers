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

    public bool IsScam { get; private set; }
    public bool IsChainActive { get; private set; } = false;
    
    private PhoneEventData currentData;

    public void StartEvent()
    {
        if (IsChainActive)
        {
            return;
        }
        
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
        IsScam = currentData.IsScam;

        descriptionText.text = currentData.description;

        Debug.Log($"Generated Phone: {currentData.name} | Scam: {IsScam}");

        phoneBase.SpawnPhoneCall(currentData);
        IsChainActive = true;
    }
    
    public void PlayerChoice(bool choseTrue)
    {
        if (!IsChainActive) return;

        currentData = choseTrue 
            ? currentData.nextEventsIfScammer 
            : currentData.nextEventsIfNormal;
        
        if (currentData == null)
        {
            Debug.Log("Цепочка завершена");
            IsChainActive = false;
            EndEvent();
            return;
        }
        
        phoneBase.UpdatePhoneCall(currentData);
        descriptionText.text = currentData.description;

        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        phoneBase.ClearPhone();
    }
}