using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    private const int maxLength = 20;
    
    public void UpdateText(string message)
    {
        message = message
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Replace("\t", " ")
            .Trim();

        if (message.Length > maxLength)
        {
            messageText.text = message.Substring(0, maxLength - 3) + "...";
        }
        else
        {
            messageText.text = message;
        }
    }
}
