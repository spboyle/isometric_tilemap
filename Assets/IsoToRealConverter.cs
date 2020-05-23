using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class IsoToRealConverter
{
    private const string basicFloorTileName = "Tilemaps/Isometric/Tiles/Temple/temple-sliced_16";
    private const string basicFloorCubeName = "Tilemaps/Isometric/Tiles/Temple/temple-sliced_26";
    private const string colliderCubeName = "Tilemaps/Isometric/Colliders/ColliderTiles/cube08-a";

    public static Vector3 IsometricToRealCoordinates(Vector3Int isoCoordinates, float xOffset=0.0f, float yOffset=0.0f) {
        float x = .5f * (isoCoordinates.x - isoCoordinates.y + xOffset);
        float y = .25f * (isoCoordinates.x + isoCoordinates.y + yOffset);
        // Impossible to use print?
        // if (isoCoordinates.z != 0) {
        //     print("z was not 0: " + isoCoordinates.z);
        // }
        return new Vector3(x, y, 0);
    }
}
