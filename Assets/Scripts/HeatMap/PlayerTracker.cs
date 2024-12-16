using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);
    public Vector2 areaSize = new Vector2(100, 100);
    public float[,] heatmapData;
    public float MaxValue;

    private Vector2 cellSize;

    void Start()
    {
        heatmapData = new float[gridSize.x, gridSize.y];
        cellSize = new Vector2(areaSize.x / gridSize.x, areaSize.y / gridSize.y);
    }

    void Update()
    {
        Vector3 playerPosition = transform.position;
        Vector2Int cell = GetGridCell(playerPosition);

        if (IsCellValid(cell))
            heatmapData[cell.x, cell.y] += Time.deltaTime;
    }

    Vector2Int GetGridCell(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + areaSize.x / 2) / cellSize.x);
        int y = Mathf.FloorToInt((position.z + areaSize.y / 2) / cellSize.y);
        return new Vector2Int(x, y);
    }

    bool IsCellValid(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < gridSize.x && cell.y >= 0 && cell.y < gridSize.y;
    }
}
