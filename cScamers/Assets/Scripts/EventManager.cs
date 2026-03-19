using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaface;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("Event Managers")]
    [SerializeField] private EmailEventManager emailManager;
    [SerializeField] private PhoneEventManager phoneManager;
    [SerializeField] private PrinterEventManager printerManager;

    private IGameEvent currentEvent;
    private EventType? lastEventType = null;
    private bool isWaitingForDecision = false;

    private Coroutine decisionCoroutine; 

    public enum EventType
    {
        Email,
        PhoneCall,
        Printer
    }

    private void Awake()
    {
        //instance adj.
    }

    private void Start()
    {
        StartEvent();
    }

    public void StartEvent()
    {
        currentEvent = GetRandomEvent();

        if (currentEvent != null)
        {
            currentEvent.StartEvent();
            isWaitingForDecision = true;
        }
    }

    private IGameEvent GetRandomEvent()
    {
        List<EventType> possibleEvents = new List<EventType>
        {
            EventType.Email,
            EventType.PhoneCall,
            EventType.Printer
        };

        // Чтобы не повторялось подряд
        if (lastEventType.HasValue)
            possibleEvents.Remove(lastEventType.Value);

        EventType selected = possibleEvents[Random.Range(0, possibleEvents.Count)];
        lastEventType = selected;

        switch (selected)
        {
            case EventType.Email:
                return emailManager;

            case EventType.PhoneCall:
                return phoneManager;

            case EventType.Printer:
                return printerManager;
        }

        return null;
    }
    
    // Общие кнопки для всех эвентов
    public void PlayerPressedKey1()
    {
        HandleDecision(false);
    }

    public void PlayerPressedKey2()
    {
        HandleDecision(true);
    }

    private void HandleDecision(bool playerThinksScam)
    {
        if (!isWaitingForDecision) return;

        bool correct = currentEvent.IsScam == playerThinksScam;
        Debug.Log($"{currentEvent.GetType().Name}: {(correct ? "Correct!" : "Wrong!")}");
        
        currentEvent.PlayerChoice(playerThinksScam);
        
        if (!currentEvent.IsChainActive)
        {
            if (decisionCoroutine != null) StopCoroutine(decisionCoroutine);

            float delay = Random.Range(1f, 2f);
            decisionCoroutine = StartCoroutine(DecisionCoroutine(delay));
        }
    }

    private IEnumerator DecisionCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartEvent();
    }
}