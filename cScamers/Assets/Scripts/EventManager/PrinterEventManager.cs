using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrinterEventManager : MonoBehaviour, Interaface.IGameEvent
{
    [Header("UI")]
    [SerializeField] private GameObject eventlUI;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Printer Interactions")]
    [SerializeField] private PrinterBase printerBase;

    [Header("Printer DataBase")]
    [SerializeField] private List<PrinterEventData> printerEvents;

    public bool IsScam { get; private set; }
    public bool IsChainActive { get; private set; } = false;
    
    private int currentIndex = 0;
    private Queue<string> currentEventQueue = new Queue<string>();
    private PrinterEventData currentData;

    public void StartEvent()
    {
        if (IsChainActive)
        {
            return;
        }
        
        if (printerEvents == null || printerEvents.Count == 0)
        {
            Debug.LogError("No Email Events assigned!");
            return;
        }

        eventlUI.SetActive(true);
        GenerateLetter();
    }
    
    void GenerateLetter()
    {
        currentData = printerEvents[Random.Range(0, printerEvents.Count)];
        IsScam = currentData.IsScam;

        descriptionText.text = currentData.description;
        Debug.Log($"Generated Letter: {currentData.name} | Scam: {IsScam}");
        printerBase.SpawnPaper(currentData);

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
        
        printerBase.SpawnPaper(currentData);
        descriptionText.text = currentData.description;
        
        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        printerBase.ClearPrinter();
    }
}
