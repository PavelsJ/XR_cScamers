using System;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardBase : MonoBehaviour
{
    [SerializeField] private bool leftKey = false;
    [SerializeField] private bool rightKey = false;
    
    [SerializeField] private KeyBase[] keys;

    private void Update()
    {
        if (keys.Length == 0) return;
        
        if (leftKey)
        {
            leftKey = false;
            
            UnityEvent keyEvent = keys[0].GetAction();
            if (keyEvent != null) keyEvent.Invoke();
        }

        if (rightKey)
        {
            rightKey = false;
            
            UnityEvent keyEvent = keys[1].GetAction();
            if (keyEvent != null) keyEvent.Invoke();
        }
    }
}
