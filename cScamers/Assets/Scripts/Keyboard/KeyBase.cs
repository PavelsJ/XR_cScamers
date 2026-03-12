using UnityEngine;
using UnityEngine.Events;

public class KeyBase : MonoBehaviour
{
    [SerializeField] private UnityEvent keyAction;
    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            keyAction?.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            isPressed = false;
        }
    }
}
