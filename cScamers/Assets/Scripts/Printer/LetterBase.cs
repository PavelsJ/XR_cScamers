using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterBase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image backgroundImage;
    public void UpdateInfo(EventData data)
    {
        ClearLetter();
        
        titleText.text = data.subject;
        descriptionText.text = data.description;
    }

    private void ClearLetter()
    {
        titleText.text = "";
        descriptionText.text = "";
    }
}
