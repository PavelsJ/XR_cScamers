using System;
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

    public void UpdateImage(EventData data)
    {
        PrinterEventData printData = data as PrinterEventData;
        if (printData != null && printData.isSpam)
        {
            ClearLetter();
            
            backgroundImage.enabled = true;
            backgroundImage.sprite = printData.spamSprite;
            titleText.text = data.subject;
        }
    }

    private void ClearLetter()
    {
        titleText.text = "";
        descriptionText.text = "";
    }
}
