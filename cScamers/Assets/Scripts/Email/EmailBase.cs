using System.Collections;
using TMPro;
using UnityEngine;

public class EmailBase : MonoBehaviour
{
    [Header("Email Action")] 
    private bool isOn = true;

    [Header("Email UI")] 
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private float existenceTime = 1f;
    
    [SerializeField] private GameObject emailBox;
    [SerializeField] private RectTransform emailBoxTransform;
    
    private Coroutine popupAnimationCoroutine;

    [Header("Email Description")]
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private TMP_Text popupText;
    
    [SerializeField] private TMP_Text subjectText;
    [SerializeField] private TMP_Text descriptionText;

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

    public void SpawnEmail(EventData data)
    {
        popupPanel.SetActive(true);
        
        if (popupAnimationCoroutine != null)
            StopCoroutine(popupAnimationCoroutine);
        
        popupAnimationCoroutine = StartCoroutine(WaitForMessage(data));
    }

    private IEnumerator WaitForMessage(EventData data)
    {
        float elapsed = 0f;
        int dotIndex = 0;
        
        string[] dots = { "   ", ".  ", ".. ", "..." };

        while (elapsed < existenceTime)
        {
            popupText.text = data.popup + dots[dotIndex];
            dotIndex = (dotIndex + 1) % dots.Length;

            elapsed += 0.4f;
            yield return new WaitForSeconds(0.4f);
        }
        
        popupPanel.SetActive(false);
        popupText.text = "";

        UpdateEmail(data);
    }

    public void UpdateEmail(EventData data)
    {
        if (data is EmailEventData email)
        {
            ClearEmail();
            
            subjectText.text = email.subject;
            descriptionText.text = email.description;
        }
        
        GameObject newEmail = Instantiate(emailBox, emailBoxTransform);
        MessageBox messageBox = newEmail.GetComponent<MessageBox>();
        if (messageBox ==null) return;
        messageBox.UpdateText(data.description);
    }

    public void ClearEmail()
    {
        descriptionText.text = "";
        
        if (popupAnimationCoroutine != null)
            StopCoroutine(popupAnimationCoroutine);
        popupPanel.SetActive(false);
    }

    public void ToggleScreen()
    {
        if (currentScreenMaterial == null) return;
        isOn = !isOn;
        emailPanel.SetActive(isOn);
        currentScreenMaterial.color = isOn ? Color.white : Color.black;
    }
}
