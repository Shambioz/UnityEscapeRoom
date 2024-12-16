using UnityEngine;

public class HeatMapUpdater : MonoBehaviour
{
    public PlayerTracker tracker;
    public Renderer targetRenderer;
    public Gradient colorGradient;

    private Texture2D heatmapTexture;

    void Start()
    {
        heatmapTexture = new Texture2D(tracker.gridSize.x, tracker.gridSize.y);
        targetRenderer.material.mainTexture = heatmapTexture;
    }

    void Update()
    {
        float[,] data = tracker.heatmapData;
        for (int x = 0; x < tracker.gridSize.x; x++)
        {
            for (int y = 0; y < tracker.gridSize.y; y++)
            {
                float value = Mathf.Clamp01(data[x, y] / tracker.MaxValue);
                Color color = colorGradient.Evaluate(value);
                heatmapTexture.SetPixel(x, y, color);
            }
        }

        heatmapTexture.Apply();
    }
}

public static class HeatmapExtensions
{
    public static float MaxValue(this float[,] array)
    {
        float max = float.MinValue;
        foreach (float value in array)
            if (value > max)
                max = value;
        return max;
    }
}
