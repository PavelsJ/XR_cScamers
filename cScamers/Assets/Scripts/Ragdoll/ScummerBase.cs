using UnityEngine;

public class ScammerBase : MonoBehaviour
{
    [Header("DEPENDENCIES")] 
    public Transform animatedModel;
    public Transform physicalModel;
    public Transform pelvisTransform;
    
    private Rigidbody rb;
    private ForceController forceController;
    private RagdollController ragdollController;
    
    private void Start()
    {
        rb = pelvisTransform.GetComponent<Rigidbody>();
        forceController = GetComponent<ForceController>();
        ragdollController = GetComponent<RagdollController>();
        
        Time.timeScale = 1;
    }
}
