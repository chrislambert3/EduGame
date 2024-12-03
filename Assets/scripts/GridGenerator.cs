using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{

    private GameObject numberPrefab;                            // Used to display the grid numbers
    private GameObject parent;                                  // Container to stuff with grid lines and numbers
    private Tilemap tilemap;                                    // This script is currently attached to the relevant tilemap
    private float lineThickness = 0.025f;                       // Thickness of the grid lines
    private Color lineColor = new Color32(255, 226, 0, 127);    // Color of the grid lines, edit here please.
    //private Color numberColor = new Color32(255, 226, 0, 127);  // Color of the grid numbers, edit here please.
    private Color numberColor = Color.black;  // Color of the grid numbers, edit here please.
    private CoordinateMapper coordinateMapper;                  // Used to convert between world coordinates and grid coordinates

    void Start()
    {
        this.tilemap = this.GetComponentInChildren<Tilemap>();
        this.numberPrefab = Resources.Load<GameObject>("Prefabs/NumberPrefab");
        this.coordinateMapper = new CoordinateMapper(this.tilemap);
        CreateParent();
        DrawGrid();
    }

    // Not used
    // void Update() { }
    void CreateParent()
    {
        this.parent = new GameObject("GridOverlay");
        if (this.parent == null)
        {
            this.parent = new GameObject("GridOverlay");
        }
    }

    void DrawLine(Vector3 start, Vector3 dir)
    {
        GameObject line = new GameObject("GridLine");
        line.transform.SetParent(this.parent.transform);
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + dir);

        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void DrawNumber(Vector3 position, int x, int y)
    {
        GameObject numberObject = Instantiate(numberPrefab, position, Quaternion.identity, this.parent.transform);

        // Handle displaying above Tilemap, but below UI
        Renderer renderer = numberObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = "Grid Numbers";
            renderer.sortingOrder = 1;
        }

        TextMesh textMesh = numberObject.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = $"({x}, {y})";
            textMesh.color = numberColor;
        }
    }

    void DrawGrid()
    {
        // Figure out the size of each "tile" in the tilemap
        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3 cellSize = tilemap.cellSize;        

        // Draw a grid of lines at each cell position,
        for (int x = cellBounds.xMin; x <= cellBounds.xMax; x++) {
            for (int y = cellBounds.yMin; y <= cellBounds.yMax; y++) { 
                Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));
                Vector3 coord = coordinateMapper.worldPosToCoordinates(worldPos);
                
                DrawLine(worldPos, new Vector3(cellSize.x, 0, 0));
                DrawLine(worldPos, new Vector3(0, cellSize.y, 0));

                // label the grid with numbers if x or y is 0
                const float labelOffset = -0.33f;
                if (coord.x == 0 && coord.y == 0)
                {
                    DrawNumber(worldPos+ new Vector3(labelOffset, labelOffset, -0.1f), (int)coord.x, (int)coord.y);
                } else if (coord.x == 0)
                {
                    DrawNumber(worldPos + new Vector3(labelOffset, 0, -0.1f), (int)coord.x, (int)coord.y);
                } else if (coord.y == 0)
                {
                    DrawNumber(worldPos + new Vector3(0, labelOffset, -0.1f), (int)coord.x, (int)coord.y);
                }
            }
        }
    }
}
