using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrinterEventManager : MonoBehaviour, Interaface.IGameEvent
{
    [Header("UI")]
    public GameObject eventlUI;
    public TextMeshProUGUI descriptionText;

    [Header("Printer Database")]
    public List<PrinterEventData> printerEvents;

    public bool IsScam { get; private set; }
    private PrinterEventData currentData;

    public void StartEvent()
    {
        if (printerEvents == null || printerEvents.Count == 0)
        {
            Debug.LogError("No Email Events assigned!");
            return;
        }

        eventlUI.SetActive(true);
        GenerateEmail();
    }

    void GenerateEmail()
    {
        currentData = printerEvents[Random.Range(0, printerEvents.Count)];
        IsScam = currentData.IsScam;
        
        descriptionText.text = currentData.description;
        Debug.Log($"Generated Email: {currentData.name} | Scam: {IsScam}");
    }

    public void EndEvent()
    {
        eventlUI.SetActive(false);
    }
}
