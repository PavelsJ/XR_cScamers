using NUnit.Framework;
using UnityEngine;

public class PigeonTarget : MonoBehaviour
{
    [SerializeField] private float minY = -0.5f;
    [SerializeField] private Rigidbody[] rbs;

    private bool isRegistered = false;
    private bool canSendFallSignal = true;
    
    private FallManger fallManager;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip dropClip;
    
    public float minVelocity = 1.0f;

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

    private void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (impact > minVelocity)
        {
            if (audioSource != null && dropClip != null) 
                audioSource.PlayOneShot(dropClip, impact / 10f);
        }
    }
    
    public void DisableFallSignal()
    {
        canSendFallSignal = false;
        fallManager.GiveItem(this);
        PlaySelectedSound();
    }
    
    public void EnableFallSignal()
    {
        canSendFallSignal = true;
    }
    
    private void PlaySelectedSound()
    {
        if (audioSource != null && audioClip != null)
            audioSource.PlayOneShot(audioClip);
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
