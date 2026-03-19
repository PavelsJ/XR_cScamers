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
    public bool IsChainActive { get; private set; } = false;
    
    private int currentIndex = 0;
    private Queue<string> currentEventQueue = new Queue<string>();
    private EmailEventData currentData;

    public void StartEvent()
    {
        if (IsChainActive)
        {
            return;
        }
        
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
        
        IsChainActive = true;
        currentEventQueue.Clear();
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
        
        descriptionText.text = currentData.description;
        emailBase.UpdateEmail(currentData);

        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        emailBase.ClearEmail();
    }
}
