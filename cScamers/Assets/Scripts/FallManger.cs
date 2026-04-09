using System.Collections.Generic;
using UnityEngine;

public class FallManger : MonoBehaviour
{
    public static FallManger Instance { get; private set; }
    
    [SerializeField] private PigeonBase pigeon;

    private List<PigeonTarget> fallQueue = new List<PigeonTarget>();
    private PigeonTarget currentTarget;
    private bool isProcessing = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!isProcessing && fallQueue.Count > 0)
        {
            ProcessNextItem();
        }
    }

    public void RegisterFallenItem(PigeonTarget item)
    {
        if (!fallQueue.Contains(item))
            fallQueue.Add(item);
    }

    private void ProcessNextItem()
    {
        if (fallQueue.Count == 0)
            return;

        PigeonTarget item = fallQueue[0];
        fallQueue.RemoveAt(0);
        currentTarget = item;

        isProcessing = true;

        pigeon.TakeItem(item.transform);
    }

    public void OnPigeonFinished()
    {
        Rigidbody currentRb = currentTarget.GetComponent<Rigidbody>();
        if (currentRb != null) currentRb.isKinematic = false;
        
        currentTarget.Reset();
        
        isProcessing = false;
    }
}
