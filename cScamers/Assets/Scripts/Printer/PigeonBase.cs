using UnityEngine;

public class PigeonBase : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public void ResetState()
    {
        transform.position = startPoint.position;
    }

    public void EndState()
    {
        transform.position = endPoint.position;
    }
}
