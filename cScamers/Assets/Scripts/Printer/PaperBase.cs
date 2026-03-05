using TMPro;
using UnityEngine;

public class PaperBase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    public void UpdateInfo(string description)
    {
        descriptionText.text = description;
    }
}
