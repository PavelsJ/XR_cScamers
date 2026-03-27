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
    
    private int currentIndex = 0;
    private Queue<string> currentEventQueue = new Queue<string>();
    private EventData currentData;

    public void GenerateEvent()
    {
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

        descriptionText.text = currentData.description;
        Debug.Log($"Generated Letter: {currentData.name} | Scam: {currentData.IsScam}");
        printerBase.SpawnPaper(currentData);
        
        currentEventQueue.Clear();
    }
    
    public void UpdateEvent(EventData data)
    {
        currentData = data;
        
        printerBase.SpawnPaper(currentData);
        descriptionText.text = currentData.description;
        
        Debug.Log($"Next chain step: {currentData.description}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
        printerBase.ClearPrinter();
    }
    
    public EventData GetCurrentEvent()
    {
        return currentData;
    }
}
