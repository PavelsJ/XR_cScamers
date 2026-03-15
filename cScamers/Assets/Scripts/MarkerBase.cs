using UnityEngine;

public class MarkerBase : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private LineRenderer line;
    [SerializeField] private float touchRadius = 0.05f;
    [SerializeField] private LayerMask paintableLayer;

    private LineRenderer currentLine;
    private int pointIndex = 0;
    private bool inHand = false;

    void Update()
    {
        if (!inHand) return;

        Collider[] hits = Physics.OverlapSphere(point.position, touchRadius, paintableLayer);
        if (hits.Length > 0)
        {
            // Берём точку на поверхности через Raycast
            RaycastHit hit;
            if (Physics.Raycast(point.position + Vector3.up * 0.1f, Vector3.down, out hit, 1f, paintableLayer))
            {
                Vector3 hitPoint = hit.point;

                // Проверка, чтобы не добавлять точку на одном месте много раз
                if (pointIndex == 0 || Vector3.Distance(hitPoint, line.GetPosition(pointIndex - 1)) > 0.1f)
                {
                    line.positionCount = pointIndex + 1;
                    line.SetPosition(pointIndex, hitPoint);
                    pointIndex++;
                }
            }
        }
    }

    public void SetInHand(bool state)
    {
        inHand = state;
    }

    private void OnDrawGizmosSelected()
    {
        if (point == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(point.position, touchRadius);
    }
}