using System;
using UnityEngine;

public class PrinterLight : MonoBehaviour
{
    [SerializeField] private Material lightMaterial;
    private Material currentMaterial;
    
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (lightMaterial == null) return;
        currentMaterial = new Material(lightMaterial);
        meshRenderer.material = currentMaterial;
    }

    public void SetLight(bool on)
    {
        if (lightMaterial == null) return;
        currentMaterial.color = on ? Color.red : lightMaterial.color;
    }
}
