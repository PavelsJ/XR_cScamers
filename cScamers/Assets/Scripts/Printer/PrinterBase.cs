using TMPro;
using UnityEngine;

public class PrinterBase : MonoBehaviour
{
    [Header("Printer Action")]
    [SerializeField] GameObject printPrefab;
    [SerializeField] Transform spawnPoint;
    private LetterBase currentPaper;

    [Header("Pigeon Action")]
    [SerializeField] private PigeonBase pigeonEvent;

    [Header("Email Description")]
    [SerializeField] private TMP_Text popupText;
    
    [Header("Screen Action")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material screenMaterial;
    
    private Material currentScreenMaterial;
    
    private void Awake()
    {
        if (screenMaterial == null) return;
        currentScreenMaterial = new Material(screenMaterial);
        meshRenderer.material = currentScreenMaterial;
    }

    public void SpawnPaper(PrinterEventData data)
    {
        popupText.text = data.popup;
        
        GameObject paper = Instantiate(printPrefab, spawnPoint.position, spawnPoint.rotation);
        var paperBase = paper.GetComponent<LetterBase>();
        if (paperBase == null) return;
        
        paperBase.UpdateInfo(data.description);
        ThrowLetter(paper, 2);
    }
    
    public void ClearPrinter()
    {
        popupText.text = "";
    }

    private void ThrowLetter(GameObject paper, float force = 5f)
    {
        if (paper == null) return;

        Rigidbody rb = paper.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 direction = transform.forward + transform.up * 0.3f;

        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
    }
}
