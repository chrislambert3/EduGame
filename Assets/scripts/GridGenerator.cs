using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{

    private Tilemap tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.tilemap = this.GetComponentInChildren<Tilemap>();
        drawGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void drawLine(Vector3 start, Vector3 dir)
    {
        GameObject line = new GameObject("Line");
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + dir);

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    void drawGrid()
    {
        //Tilemap tilemap = course.GetComponentInChildren<Tilemap>();

        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3 cellSize = tilemap.cellSize;

        for (int x = cellBounds.xMin; x <= cellBounds.xMax; x++) {
            for (int y = cellBounds.yMin; y <= cellBounds.yMax; y++) { 
                Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));

                drawLine(worldPos, new Vector3(cellSize.x, 0, 0));
                drawLine(worldPos, new Vector3(0, cellSize.y, 0));
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