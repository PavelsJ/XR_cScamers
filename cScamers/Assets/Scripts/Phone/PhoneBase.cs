using System.Collections;
using TMPro;
using UnityEngine;

public class PhoneBase : MonoBehaviour
{
    [Header("Phone Action")]
    private bool isCalling;
    private Rigidbody rigidbody;
    private Coroutine phoneRoutine;
    
    [Header("Email Description")]
    [SerializeField] private TMP_Text popupText;
    
    [Header("Message Description")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    
    [Header("Screen Action")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material screenMaterial;
    private Material currentScreenMaterial;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        if (screenMaterial == null) return;
        currentScreenMaterial = new Material(screenMaterial);
        meshRenderer.material = currentScreenMaterial;
    }
    
    public void SpawnPhoneCall(EventData data)
    {
        popupText.text = data.popup;
        
        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);

        phoneRoutine = StartCoroutine(PhoneCallRoutine());
    }

    public void SpawnPhoneMessage(EventData data)
    {
        messagePanel.SetActive(true);
        messageText.text = data.description;
    }
    
    public void UpdatePhoneCall(EventData data)
    {
        ClearPhone();
        
        popupText.text = data.popup;
        
        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
        
        phoneRoutine = StartCoroutine(PhoneCallRoutine());
    }
    
    public void UpdatePhoneMessage(EventData data)
    {
        ClearPhone();
            
        messagePanel.SetActive(true);
        messageText.text = data.description;
    }

    
    public void ClearPhone()
    {
        isCalling = false;

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
        
        popupText.text = "";
        messagePanel.SetActive(false);
    }
    
    private IEnumerator PhoneCallRoutine()
    {
        isCalling = true;

        while (isCalling)
        {
            Vector3 force = new Vector3(
                Random.Range(-1f, 1f), 0,
                Random.Range(-1f, 1f)
            );

            rigidbody.AddForce(force, ForceMode.Impulse);

            yield return new WaitForSeconds(0.15f);
        }
    }

    public void AnswerCall()
    {
        isCalling = false;

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
    }
}
