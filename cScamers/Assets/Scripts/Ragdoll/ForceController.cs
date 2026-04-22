using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    [Header("GRAVITY SETTINGS")]
    public Transform pullCenter;

    public List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();

    public Vector3 currentDirection;
    public float currentStrength;
    private float startDrag;
    
    private Quaternion initialRotation;

    private void Start()
    {
        affectedRigidbodies.Clear();
        initialRotation = pullCenter.rotation;
        GatherRigidbodies(pullCenter);
        
        Rigidbody parentRb = pullCenter.GetComponent<Rigidbody>();

        if (parentRb != null)
        {
            affectedRigidbodies.Add(parentRb);
        }
    }
    
    private void GatherRigidbodies(Transform parent)
    {
        RagdollController ragdollController = parent.transform.root.GetComponent<RagdollController>();

        if (ragdollController == null)
        {
            foreach (Transform child in parent)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
            
                if (rb != null)
                {
                    affectedRigidbodies.Add(rb);
                }
            
                GatherRigidbodies(child);
            }
        }
        else
        {
            foreach (Rigidbody rb in ragdollController.rbs)
            {
                affectedRigidbodies.Add(rb);
            }
        }
        
    }

    private void ApplyDirectionalGravity()
    {
        foreach (Rigidbody rb in affectedRigidbodies)
        {
            rb.useGravity = false;
            Vector3 gravitationalForce = currentStrength * rb.mass * currentDirection;
            rb.AddForce(gravitationalForce);
        }

        pullCenter.rotation = RotateTowards(pullCenter, -currentDirection);
    }
    
   
    private void ResetRigidbodies()
    {
        foreach (Rigidbody rb in affectedRigidbodies)
        {
            rb.useGravity = true;
        }
        pullCenter.rotation = Quaternion.Slerp(pullCenter.rotation, initialRotation, Time.fixedDeltaTime * 2f);
    }

    private Quaternion RotateTowards(Transform center, Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(center.up, direction) * center.rotation;
        return Quaternion.Slerp(center.rotation, targetRotation, Time.fixedDeltaTime * 2f);
    }
}
