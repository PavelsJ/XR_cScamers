using UnityEngine;

public class ScammerBase : MonoBehaviour
{
    [Header("DEPENDENCIES")] 
    public Transform animatedModel;
    public Transform physicalModel;
    public Transform pelvisTransform;
    public Transform headTransform;
    
    public float moveForce = 50f;
    public float stopDistance = 1f;
    public float rotationForce = 500f;
    public float maxAngle = 180f;
    
    private Rigidbody pelvisRb;
    private Rigidbody headRb;
    
    private RagdollController ragdollController;
    
    private void Start()
    {
        pelvisRb = pelvisTransform.GetComponent<Rigidbody>();
        headRb = headTransform.GetComponent<Rigidbody>();
        
        ragdollController = GetComponent<RagdollController>();
        
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null) return;
        
        MoveTowardsPlayer();
        
        LookAtPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 toPlayerFull = GameManager.Instance.player.position - pelvisTransform.position;
        float distance = toPlayerFull.magnitude;

        if (distance > stopDistance)
        {
            Vector3 moveDir = toPlayerFull.normalized;
            moveDir.y = 0f;

            pelvisRb.AddForce(moveDir * moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            pelvisRb.linearVelocity = Vector3.Lerp(pelvisRb.linearVelocity, Vector3.zero, 0.2f);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 toPlayer = GameManager.Instance.player.position - headTransform.position;
        
        toPlayer.y = 0f;

        if (toPlayer.sqrMagnitude < 0.001f) return;

        Vector3 currentForward = -headTransform.forward;
        currentForward.y = 0f;
        
        toPlayer.Normalize();
        currentForward.Normalize();
        
        float angle = Vector3.SignedAngle(currentForward, toPlayer, Vector3.up);
        
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        
        Vector3 torque = headTransform.up * angle * rotationForce * Time.fixedDeltaTime;

        headRb.AddTorque(torque, ForceMode.Acceleration);
    }
    
    private void OnDrawGizmos()
    {
        if (headTransform == null || GameManager.Instance == null || GameManager.Instance.player == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(headTransform.position, (GameManager.Instance.player.position - headTransform.position).normalized * 2f);
    }
}
