using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterBase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image backgroundImage;
    public void UpdateInfo(EventData data, bool isImage = false)
    {
        if (data is PrinterEventData print)
        {
            titleText.text = print.subject;
            ClearLetter();

            if (isImage)
            {
                backgroundImage.enabled = true;
                backgroundImage.sprite = print.spamSprite;
            }
            else
            {
                descriptionText.text = data.description;
            }
        }
    }

    private void ClearLetter()
    {
        backgroundImage.enabled = false;
        descriptionText.text = "";
    }
}
