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
    
    private int currentIndex = 0;
    private Queue<string> currentEventQueue = new Queue<string>();
    private EventData currentData;

    public void GenerateEvent()
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
        
        descriptionText.text = currentData.description;
        Debug.Log($"Generated Email: {currentData.name} | Scam: {currentData.IsScam}");
        emailBase.SpawnEmail(currentData);
        
        currentEventQueue.Clear();
    }
    
    public void UpdateEvent(EventData data)
    {
        currentData = data;
        
        descriptionText.text = currentData.description;
        emailBase.UpdateEmail(currentData);

        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        emailBase.ClearEmail();
    }

    public EventData GetCurrentEvent()
    {
        return currentData;
    }
}
