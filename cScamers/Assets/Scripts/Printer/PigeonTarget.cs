using NUnit.Framework;
using UnityEngine;

public class PigeonTarget : MonoBehaviour
{
    [SerializeField] private float minY = -0.5f;
    [SerializeField] private Rigidbody[] rbs;

    private bool isRegistered = false;
    private bool canSendFallSignal = true;
    
    private FallManger fallManager;


    private void Start()
    {
        fallManager = FallManger.Instance;
    }

    private void LateUpdate()
    {
        if (isRegistered) return;
        if (!canSendFallSignal) return;

        if (transform.position.y < minY)
        {
            isRegistered = true;
            fallManager.RegisterFallenItem(this);
        }
    }
    
    public void DisableFallSignal()
    {
        canSendFallSignal = false;
        fallManager.GiveItem(this);
    }
    
    public void EnableFallSignal()
    {
        canSendFallSignal = true;
    }

    public void Reset()
    {
        isRegistered = false;
    }

    public Rigidbody[] GetRbs()
    {
        return rbs;
    }
}
