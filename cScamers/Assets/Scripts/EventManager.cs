using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaface;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    [Header("Event Managers")]
    public EmailEventManager emailManager;
    public PhoneEventManager phoneManager;
    public PrinterEventManager printerManager;

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
    public void PlayerPressedA()
    {
        if (!isWaitingForDecision) return;
        HandleDecision(true);
    }

    public void PlayerPressedB()
    {
        if (!isWaitingForDecision) return;
        HandleDecision(false);
    }

    private void HandleDecision(bool playerThinksScam)
    {
        isWaitingForDecision = false;

        bool correct = currentEvent.IsScam == playerThinksScam;
        Debug.Log($"{currentEvent.GetType().Name}: {(correct ? "Correct!" : "Wrong!")}");
        
        currentEvent.EndEvent();
        
        if (decisionCoroutine != null) StopCoroutine(decisionCoroutine);
        float delay = Random.Range(1f, 2f);
        decisionCoroutine = StartCoroutine(DecisionCoroutine(delay));
    }

    private IEnumerator DecisionCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartEvent();
    }
}