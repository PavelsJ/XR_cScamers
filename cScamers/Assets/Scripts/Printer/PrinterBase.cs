using TMPro;
using UnityEngine;

public class PrinterBase : MonoBehaviour
{
    [Header("Printer Action")]
    [SerializeField] PrinterLight printLight;
    [SerializeField] GameObject printPrefab;
    [SerializeField] Transform spawnPoint;

    private bool isPrinting = false;
    private EventData currentData;

    [Header("Pigeon Action")]
    [SerializeField] private PigeonBase pigeonEvent;

    [Header("Email Description")]
    [SerializeField] private TMP_Text popupText;
    
    [Header("Screen Action")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material screenMaterial;
    
    private Material currentScreenMaterial;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
    private void Awake()
    {
        if (screenMaterial == null) return;
        currentScreenMaterial = new Material(screenMaterial);
        meshRenderer.material = currentScreenMaterial;
        currentScreenMaterial.color = Color.white;
    }

    public void SpawnPaper(EventData data)
    {
        isPrinting = true;
        currentData = data;
        printLight.SetLight(true);
        
        PrinterEventData printData = data as PrinterEventData;
        if (printData != null && printData.isSpam) StartSpam();
    }

    public void StartLetter()
    {
        if (!isPrinting) return;

        if (currentData == null) return;
        popupText.text = currentData.popup;

        GameObject paper = Instantiate(printPrefab, spawnPoint.position, spawnPoint.rotation);
        var paperBase = paper.GetComponent<LetterBase>();
        if (paperBase == null) return;

        paperBase.UpdateInfo(currentData);
        ThrowLetter(paper, 2);
        isPrinting = false;
    }

    public void StartSpam()
    {
        if (!isPrinting) return;

        if (currentData == null) return;
        popupText.text = currentData.popup;

        GameObject paper = Instantiate(printPrefab, spawnPoint.position, spawnPoint.rotation);
        var paperBase = paper.GetComponent<LetterBase>();
        if (paperBase == null) return;

        paperBase.UpdateImage(currentData);
        ThrowLetter(paper, 5);
        isPrinting = false;
    }
    
    public void ClearPrinter()
    {
        printLight.SetLight(false);
        popupText.text = "";
        isPrinting = false;
    }

    private void ThrowLetter(GameObject paper, float force = 5f)
    {
        if (paper == null) return;
        PlaySelectedSound();

        Rigidbody rb = paper.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 direction = transform.forward + transform.up * 0.3f;

        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
    }
    
    private void PlaySelectedSound()
    {
        if (audioSource != null  && audioClip != null)
            audioSource.PlayOneShot(audioClip);
    }
}
