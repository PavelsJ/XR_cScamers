using UnityEngine;
using UnityEngine.Events;

public class KeyBase : MonoBehaviour
{
    [SerializeField] private UnityEvent keyAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            keyAction?.Invoke();
        }
    }
}
