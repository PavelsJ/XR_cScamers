using UnityEngine;

public class MarkerBase : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private float touchRadius = 0.05f;
    [SerializeField] private LayerMask paintableLayer;

    public Color brushColor = Color.red;
    public int brushSize = 6;

    private Texture2D texture;
    private Renderer currentRenderer;

    private bool inHand = true;

    void Update()
    {
        if (!inHand) return;

        RaycastHit hit;
        if (Physics.Raycast(point.position + Vector3.up * 0.01f, Vector3.down, out hit, 1f, paintableLayer))
        {
            Paint(hit);
        }
    }

    private void Paint(RaycastHit hit)
    {
        Renderer rend = hit.collider.GetComponent<Renderer>();
        if (rend == null) return;

        if (rend != currentRenderer)
        {
            currentRenderer = rend;
            Texture2D sourceTex = rend.material.mainTexture as Texture2D;

            if (sourceTex == null)
            {
                Debug.LogError($"{rend.gameObject.name} doesnt have texture 2d");
                return;
            }

            texture = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);
            Graphics.CopyTexture(sourceTex, texture);
            texture.Apply();

            currentRenderer.material.mainTexture = texture;
        }

        if (texture == null) return;

        Vector2 pixelUV = hit.textureCoord;
        int x = (int)(pixelUV.x * texture.width);
        int y = (int)(pixelUV.y * texture.height);

        DrawCircle(x, y);
        texture.Apply();
    }

    private void DrawCircle(int centerX, int centerY)
    {
        int diameter = brushSize * 2 + 1;

        int startX = Mathf.Max(centerX - brushSize, 0);
        int startY = Mathf.Max(centerY - brushSize, 0);
        int endX = Mathf.Min(centerX + brushSize, texture.width - 1);
        int endY = Mathf.Min(centerY + brushSize, texture.height - 1);

        Color[] pixels = texture.GetPixels(startX, startY, endX - startX + 1, endY - startY + 1);

        for (int y = 0; y <= endY - startY; y++)
        {
            for (int x = 0; x <= endX - startX; x++)
            {
                int px = x + startX - centerX;
                int py = y + startY - centerY;

                if (px * px + py * py <= brushSize * brushSize)
                {
                    int index = y * (endX - startX + 1) + x;
                    pixels[index] = Color.Lerp(pixels[index], brushColor, 0.7f);
                }
            }
        }

        texture.SetPixels(startX, startY, endX - startX + 1, endY - startY + 1, pixels);
    }

    public void SetInHand(bool state)
    {
        inHand = state;
    }
    private void OnDrawGizmosSelected()
    {
        if (point == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(point.position, touchRadius);
    }
}