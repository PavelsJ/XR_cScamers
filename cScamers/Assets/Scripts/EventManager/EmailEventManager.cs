using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmailEventManager : MonoBehaviour, Interaface.IGameEvent
{
    [Header("UI")]
    public GameObject eventlUI;
    public TextMeshProUGUI descriptionText;
    
    [Header("Email Interactions")]
    [SerializeField] private EmailBase emailBase;

    [Header("Email Database")]
    public List<EmailEventData> emailEvents;

    public bool IsScam { get; private set; }
    private EmailEventData currentData;

    public void StartEvent()
    {
        if (emailEvents == null || emailEvents.Count == 0)
        {
            Debug.LogError("No Email Events assigned!");
            return;
        }

        eventlUI.SetActive(true);
        GenerateEmail();
    }

    void GenerateEmail()
    {
        currentData = emailEvents[Random.Range(0, emailEvents.Count)];
        IsScam = currentData.IsScam;
        
        descriptionText.text = currentData.description;
        Debug.Log($"Generated Email: {currentData.name} | Scam: {IsScam}");
        
        emailBase.SpawnEmail(currentData);
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        emailBase.ClearEmail();
    }
}
