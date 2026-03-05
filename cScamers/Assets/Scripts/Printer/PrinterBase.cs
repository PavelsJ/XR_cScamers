using UnityEngine;

public class PrinterBase : MonoBehaviour
{
    [SerializeField] GameObject printPrefab;
    private PaperBase currentPaper;

    public void SpawnPaper(PrinterEventData data)
    {
        GameObject paper = Instantiate(printPrefab, transform);
        var paperBase = paper.GetComponent<PaperBase>();
        if (paperBase == null) return;
        
        paperBase.UpdateInfo(data.description);
        ThrowPaper(paper, 2);
    }

    private void ThrowPaper(GameObject paper, float force = 2f)
    {
        if (paper == null) return;

        Rigidbody rb = paper.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 direction = transform.forward + transform.up * 0.3f;

        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
    }
}
