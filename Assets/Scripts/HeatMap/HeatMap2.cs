using UnityEngine;

public class HeatMap2 : MonoBehaviour
{
    public Transform player;  // Reference to the player object
    public Renderer planeRenderer;  // Reference to the plane's Renderer
    public int textureSize = 256;  // Resolution of the heatmap texture
    public float blurRadius = 3f;  // How much the heat "spreads"
    public float intensity = 0.1f;  // How much the color increases per frame

    private Texture2D heatmapTexture;
    private Color[] heatmapColors;

    void Start()
    {
        // Initialize the heatmap texture
        heatmapTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        heatmapColors = new Color[textureSize * textureSize];

        // Clear the texture
        for (int i = 0; i < heatmapColors.Length; i++)
        {
            heatmapColors[i] = Color.black;  // Initial heatmap is black (no heat)
        }
        heatmapTexture.SetPixels(heatmapColors);
        heatmapTexture.Apply();

        // Assign the texture to the plane's material
        planeRenderer.material.mainTexture = heatmapTexture;
    }

    void Update()
    {
        // Convert player position to UV coordinates
        Vector3 localPosition = planeRenderer.transform.InverseTransformPoint(player.position);
        float uvX = (localPosition.x + 0.5f * planeRenderer.transform.localScale.x) / planeRenderer.transform.localScale.x;
        float uvY = (localPosition.z + 0.5f * planeRenderer.transform.localScale.z) / planeRenderer.transform.localScale.z;

        // Map UV to texture coordinates
        int x = Mathf.Clamp((int)(uvX * textureSize), 0, textureSize - 1);
        int y = Mathf.Clamp((int)(uvY * textureSize), 0, textureSize - 1);

        // Add heat to the texture
        AddHeat(x, y);

        // Apply the updated texture
        heatmapTexture.SetPixels(heatmapColors);
        heatmapTexture.Apply();
    }

    void AddHeat(int x, int y)
    {
        // Add heat around the player's position
        for (int i = -Mathf.FloorToInt(blurRadius); i <= Mathf.FloorToInt(blurRadius); i++)
        {
            for (int j = -Mathf.FloorToInt(blurRadius); j <= Mathf.FloorToInt(blurRadius); j++)
            {
                int posX = Mathf.Clamp(x + i, 0, textureSize - 1);
                int posY = Mathf.Clamp(y + j, 0, textureSize - 1);

                float distance = Mathf.Sqrt(i * i + j * j);
                if (distance <= blurRadius)
                {
                    int index = posY * textureSize + posX;
                    heatmapColors[index] += new Color(intensity, 0, 0, 1); // Red heat
                    heatmapColors[index].r = Mathf.Clamp01(heatmapColors[index].r);
                }
            }
        }
    }
}
