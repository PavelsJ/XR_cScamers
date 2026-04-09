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
    
    private Coroutine popupAnimationCoroutine;
    
    [Header("Email Description")]
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
        descriptionText.text = data.description;
    }

    public void UpdateEmail(EventData data)
    {
        descriptionText.text = data.description;
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
        currentScreenMaterial.color = isOn ? Color.white : Color.black;
    }
}
