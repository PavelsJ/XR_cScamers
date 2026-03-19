using System.Collections;
using System.Drawing;
using UnityEngine;

public class PigeonBase : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform screenPoint;

    [SerializeField] private LayerMask interactableLayer;

    private Vector3 currentpoint; 
    private bool isVaiting = false;
    private Coroutine vaitingCoroutine;

    public void ResetState()
    {
        float randomNum = Random.Range(0, 1);
        Vector3 resetPoint = Vector3.Lerp(startPoint.position, screenPoint.position, randomNum);

        if (vaitingCoroutine != null)
            StopCoroutine(vaitingCoroutine);

        vaitingCoroutine = StartCoroutine(
            MoveTowardsState(currentpoint, Random.Range(4, 6)));
           
    }

    public void SetState(Vector3 newState)
    {
        if (isVaiting) return;
        currentpoint = newState;

        if (vaitingCoroutine != null)
            StopCoroutine(vaitingCoroutine);

        vaitingCoroutine = StartCoroutine(
           MoveTowardsState(currentpoint, Random.Range(2, 4)));
    }

    private IEnumerator MoveTowardsState(Vector3 state, float speed)
    {
        isVaiting = true;

        while (Vector3.Distance(transform.localPosition, state) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, state,
                Time.deltaTime * speed
            );

            yield return null;
        }

        transform.position = state;
    }

    private void CheckEvent()
    {
        if (isVaiting)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f, interactableLayer);
            if (hits.Length > 0)
            {
                
            }
        }
    }
}
