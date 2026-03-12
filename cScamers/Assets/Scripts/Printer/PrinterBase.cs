using UnityEngine;

public class PrinterBase : MonoBehaviour
{
    [SerializeField] GameObject printPrefab;
    [SerializeField] Transform spawnPoint;
    private LetterBase currentPaper;

    public void SpawnPaper(PrinterEventData data)
    {
        GameObject paper = Instantiate(printPrefab, spawnPoint);
        var paperBase = paper.GetComponent<LetterBase>();
        if (paperBase == null) return;
        
        paperBase.UpdateInfo(data.description);
        ThrowLetter(paper, 2);
    }

    private void ThrowLetter(GameObject paper, float force = 10f)
    {
        if (paper == null) return;

        Rigidbody rb = paper.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 direction = transform.forward + transform.up * 0.3f;

        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
    }
}
