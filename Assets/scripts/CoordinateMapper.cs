using UnityEngine;
using UnityEngine.Tilemaps;

public class CoordinateMapper
{
    // world position of the lower left corner of the tilemap
    // used to convert between world coordinates and game coordinates
    // (0,0) in game coordinates
    private Vector3 lowerLeftCorner;

    public CoordinateMapper(Tilemap tilemap)
    {
        // access tilemap and determine lower left corner
        BoundsInt cellBounds = tilemap.cellBounds;
        lowerLeftCorner = tilemap.CellToWorld(new Vector3Int(cellBounds.xMin, cellBounds.yMin, 0));
    }

    // accept game coordinates and return the corresponding world position
    public Vector3 coordinatesToWorldPos(Vector3 coord)
    {
        float x = coord.x;
        float y = coord.y;
        return new Vector3(x, y, 0) + lowerLeftCorner;
    }

    // accept world position and return the corresponding game coordinates
    public Vector3 worldPosToCoordinates(Vector3 worldPos)
    {
        float x = worldPos.x;
        float y = worldPos.y;
        return new Vector3(x, y, 0) - lowerLeftCorner;
    }

}
