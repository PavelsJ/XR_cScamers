using TMPro;
using UnityEngine;

public class EmailBase : MonoBehaviour
{
    [Header("Email Action")] 
    private bool isOn = true;
    
    [Header("Email Description")]
    [SerializeField] private TMP_Text popupText;
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
    
    public void SpawnEmail(EmailEventData data)
    {
        popupText.text = data.popup;
        descriptionText.text = data.description;
    }

    public void ClearEmail()
    {
        popupText.text = "";
        descriptionText.text = "";
    }

    public void ToggleScreen()
    {
        if (currentScreenMaterial == null) return;
        isOn = !isOn;
        currentScreenMaterial.color = isOn ? Color.white : Color.black;
    }
}
