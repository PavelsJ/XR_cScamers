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
        if (data is PrinterEventData print)
        {
            titleText.text = print.subject;
            descriptionText.text = data.description;
            
            ClearLetter();
        }
    }

    private void ClearLetter()
    {
        descriptionText.text = "";
    }
}
