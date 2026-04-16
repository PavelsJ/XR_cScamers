using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KeyBase : MonoBehaviour
{
    [SerializeField] private UnityEvent keyAction;
    
    [SerializeField] private float pressDepth = 0.02f;
    [SerializeField] private Vector3 direction = Vector3.down;
    [SerializeField] private float pressSpeed = 8f;
    
    private bool isPressed = false;
    
    private Vector3 startPos;
    private Vector3 pressedPos;
    
    private Coroutine keyRoutine;
    
    private void Awake()
    {
        startPos = transform.localPosition;
        pressedPos = startPos + direction * pressDepth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            keyAction?.Invoke();
            isPressed = true;
            
            if (keyRoutine != null) StopCoroutine(keyRoutine);
            keyRoutine = StartCoroutine(SetKey(pressedPos));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            StartCoroutine(Delay(1));
            
            if (keyRoutine != null) StopCoroutine(keyRoutine);
            keyRoutine = StartCoroutine(SetKey(startPos));
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return delay;

        isPressed = false;
    }
    
    private IEnumerator SetKey(Vector3 target)
    {
        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                target,
                Time.deltaTime * pressSpeed
            );

            yield return null;
        }

        transform.localPosition = target;
    }
    
    public UnityEvent GetAction()
    {
        return keyAction;
    }
}
