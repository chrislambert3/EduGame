using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{

    //[SerializeField] private GameObject numberPrefab;
    private GameObject numberPrefab;
    private GameObject parent;
    private Tilemap tilemap;
    private Color lineColor = new Color32(255, 226, 0, 127);
    private Color numberColor = new Color32(255, 226, 0, 127);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.tilemap = this.GetComponentInChildren<Tilemap>();
        this.numberPrefab = Resources.Load<GameObject>("Prefabs/NumberPrefab");
        CreateParent();
        DrawGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateParent()
    {
        parent = new GameObject("GridOverlay");
        if (parent == null)
        {
            parent = new GameObject("GridOverlay");
        }
    }

    void DrawLine(Vector3 start, Vector3 dir)
    {
        GameObject line = new GameObject("GridLine");
        line.transform.SetParent(parent.transform);
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + dir);

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void DrawNumber(Vector3 position, int x, int y)
    {
        GameObject numberObject = Instantiate(numberPrefab, position, Quaternion.identity, parent.transform);

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
        //Tilemap tilemap = course.GetComponentInChildren<Tilemap>();

        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3 cellSize = tilemap.cellSize;

        for (int x = cellBounds.xMin; x <= cellBounds.xMax; x++) {
            for (int y = cellBounds.yMin; y <= cellBounds.yMax; y++) { 
                Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));

                DrawLine(worldPos, new Vector3(cellSize.x, 0, 0));
                DrawLine(worldPos, new Vector3(0, cellSize.y, 0));

                //GameObject numberObject = Instantiate(numberPrefab, worldPos + new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, -0.1f), Quaternion.identity);

                // Handle displaying above Tilemap, but below UI
                //Renderer renderer = numberObject.GetComponent<Renderer>();
                //if (renderer != null)
                //{
                //renderer.sortingLayerName = "Grid Numbers";
                //renderer.sortingOrder = 1;
                //}

                //TextMesh textMesh = numberObject.GetComponent<TextMesh>();
                //if (textMesh != null)
                //{
                //textMesh.text = $"{x}, {y}";
                //textMesh.color = Color.white;
                //}
                if (x == 0 || y == 0)
                {
                    //DrawNumber(worldPos + new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, -0.1f), x, y);
                    DrawNumber(worldPos + new Vector3(0, 0, -0.1f), x, y);
                }
            }
        }
    }
}


/*
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.scripts
{
    public class GridGenerator : MonoBehaviour
    {
        public Color lineColor = Color.white;
        public float lineWidth = 0.1f;
        public int width;
        public int height;
        public float spacing = 1f;

        private Tilemap tilemap;


        private void OnDrawGizmos()
        {
            Gizmos.color = lineColor;

            GameObject courseObject = GameObject.Find("Course");
            // TODO: handle failure
            if (courseObject != null)
            {
                Tilemap tilemap = courseObject.GetComponentInChildren<Tilemap>();
                this.width = tilemap.cellBounds.size.x * (int)tilemap.cellSize.x;
                this.height = tilemap.cellBounds.size.y * (int)tilemap.cellSize.y;
            }

            for (int x = -width / 2; x < width / 2; x++)
            {
                Gizmos.DrawLine(new Vector3(x * spacing, -height / 2, 0), 
                                new Vector3(x * spacing, height / 2 * spacing, 0));
            }

            for (int y = -height / 2; y < height / 2; y++)
            {
                Gizmos.DrawLine(new Vector3(-width / 2, y * spacing, 0),
                                new Vector3(width / 2 * spacing, y * spacing, 0));
            }
        }
    }
}
*/