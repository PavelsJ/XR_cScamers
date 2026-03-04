using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneEventManager : MonoBehaviour, Interaface.IGameEvent
{
    [Header("UI")]
    public GameObject eventlUI;
    public TextMeshProUGUI descriptionText;

    [Header("Phone Database")]
    public List<PhoneEventData> phoneEvents;

    public bool IsScam { get; private set; }
    private PhoneEventData currentData;

    public void StartEvent()
    {
        if (phoneEvents == null || phoneEvents.Count == 0)
        {
            Debug.LogError("No Email Events assigned!");
            return;
        }

        eventlUI.SetActive(true);
        GenerateEmail();
    }

    void GenerateEmail()
    {
        currentData = phoneEvents[Random.Range(0, phoneEvents.Count)];
        IsScam = currentData.IsScam;
        
        descriptionText.text = currentData.description;
        Debug.Log($"Generated Email: {currentData.name} | Scam: {IsScam}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
    }
}