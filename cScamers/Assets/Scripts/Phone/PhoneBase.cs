using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBase : MonoBehaviour
{
    [Header("Phone Action")]
    private bool isCalling;
    private Coroutine phoneRoutine;

    [Header("Email Description")]
    [SerializeField] private Image popupImage;
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private Animator animator;
    
    [Header("Message Description")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    
    [Header("Screen Action")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material screenMaterial;
    [SerializeField] private Rigidbody[] rbs;

    private Material currentScreenMaterial;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    
    [SerializeField] private AudioSource callSource;
    [SerializeField] private AudioClip callClip;

    
    private void Awake()
    {
        if (screenMaterial == null) return;
        currentScreenMaterial = new Material(screenMaterial);
        meshRenderer.material = currentScreenMaterial;
        currentScreenMaterial.color = Color.black;
    }
    
    public void SpawnPhoneCall(EventData data)
    {
        ClearPhone();
        
        popupText.text = data.popup;
        popupImage.enabled = true;
        animator.SetTrigger("Call");

        if (callSource != null && callClip != null)
        {
            if(callSource.clip == null)
                callSource.clip = callClip;
            callSource.Play();
        }
        
        currentScreenMaterial.color = Color.white;

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);

        phoneRoutine = StartCoroutine(PhoneCallRoutine());
    }

    public void SpawnPhoneMessage(EventData data)
    {
        ClearPhone();

        PlaySelectedSound(0);
        
        popupText.text = data.popup;
        popupImage.enabled = true;
        animator.SetTrigger("Message");
        
        messagePanel.SetActive(true);
        messageText.text = data.description;
        
        currentScreenMaterial.color = Color.white;
    }
    
    public void UpdatePhoneCall(EventData data)
    {
        ClearPhone();
        
        popupText.text = data.popup;
        popupImage.enabled = true;
        currentScreenMaterial.color = Color.white;
        
        if (callSource != null && callClip != null)
        {
            if(callSource.clip == null)
                callSource.clip = callClip;
            callSource.Play();
        }

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
        
        phoneRoutine = StartCoroutine(PhoneCallRoutine());
    }
    
    public void UpdatePhoneMessage(EventData data)
    {
        ClearPhone();
        
        PlaySelectedSound(0);
            
        messagePanel.SetActive(true);
        messageText.text = data.description;

        screenMaterial.color = Color.white;
    }

    
    public void ClearPhone()
    {
        isCalling = false;

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
        
        if (callSource != null )
            callSource.Stop();

        currentScreenMaterial.color = Color.black;

        popupText.text = "";
        popupImage.enabled = false;
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

            foreach (var rb in rbs)
            {
                rb.AddForce(force, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    public void AnswerCall()
    {
        isCalling = false;

        if (phoneRoutine != null)
            StopCoroutine(phoneRoutine);
        
        if (callSource != null )
            callSource.Stop();
    }
    
    private void PlaySelectedSound(int index)
    {
        if (audioSource != null  && audioClips.Length > 0)
            audioSource.PlayOneShot(audioClips[index]);
    }
}
