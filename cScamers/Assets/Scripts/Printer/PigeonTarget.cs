using UnityEngine;

public class PigeonTarget : MonoBehaviour
{
    [SerializeField] private float minY = -0.5f;
    private bool isRegistered = false;
    private FallManger fallManager;

    private void Start()
    {
        fallManager = FallManger.Instance;
    }

    private void Update()
    {
        if (isRegistered) return;

        if (transform.position.y < minY)
        {
            isRegistered = true;
            fallManager.RegisterFallenItem(this);
        }
    }

    public void Reset()
    {
        isRegistered = false;
    }
}
