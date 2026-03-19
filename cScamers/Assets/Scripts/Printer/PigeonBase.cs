using System.Collections;
using System.Drawing;
using UnityEngine;

public class PigeonBase : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform grabPoint;
    
    private State currentState;
    private Transform targetItem;
    
    private Coroutine activeCoroutine;
    
    private enum State
    {
        Idle,
        FlyToItem,
        GrabItem,
        FlyToPlayer,
        WaitingForPlayer
    }

    public void ResetState()
    {
        currentState = State.Idle;
        
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        
        activeCoroutine = null;
        targetItem = null;

        activeCoroutine = StartCoroutine(MoveTowardsTarget(startPoint, 4f));
    }

    public void TakeItem(Transform item)
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        targetItem = item;
        currentState = State.FlyToItem;
        activeCoroutine = StartCoroutine(MoveTowardsItem());
    }

    private IEnumerator MoveTowardsItem()
    {
        yield return MoveTowardsTarget(targetItem, 2f);
        activeCoroutine = StartCoroutine(GrabItem());
    }
    
    private IEnumerator GrabItem()
    {
        currentState = State.GrabItem;

        targetItem.SetParent(grabPoint);
        targetItem.localPosition = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        activeCoroutine = StartCoroutine(FlyToPlayer());
    }
    
    private IEnumerator FlyToPlayer()
    {
        currentState = State.FlyToPlayer;

        yield return MoveTowardsTarget(playerPoint, 2f);

        StartWaitingForPlayer();
    }
    
    private void StartWaitingForPlayer()
    {
        currentState = State.WaitingForPlayer;
        activeCoroutine = null;
    }
    
    private IEnumerator MoveTowardsTarget(Transform target, float duration)
    {
        Vector3 start = transform.position;
        Vector3 end = target.position;
        
        float height = Vector3.Distance(start, end) * 0.3f;
        Vector3 mid = (start + end) / 2 + Vector3.up * height;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            Vector3 pos = GetBezierPoint(start, mid, end, t);
            transform.position = pos;
            
            Vector3 nextPos = GetBezierPoint(start, mid, end, t + 0.01f);
            Vector3 dir = (nextPos - pos).normalized;

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    6f * Time.deltaTime
                );
            }

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
    
     public void DropItem()
    {
        if (targetItem != null)
            targetItem.SetParent(null);

        ResetState();
    }
     
    private Vector3 GetBezierPoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return (1 - t) * (1 - t) * a +
               2 * (1 - t) * t * b +
               t * t * c;
    }
}
