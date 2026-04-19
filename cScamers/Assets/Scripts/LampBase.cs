using UnityEngine;

public class LampBase : MonoBehaviour
{
    public GameObject light;

    public void ToggleLight()
    {
        light.SetActive(!light.activeSelf);
    }
}
