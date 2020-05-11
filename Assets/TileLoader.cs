using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileLoader
{
    private const string basicFloorTileName = "Tilemaps/Isometric/Tiles/Temple/temple-sliced_16";
    private const string basicFloorCubeName = "Tilemaps/Isometric/Tiles/Temple/temple-sliced_26";
    private const string colliderCubeName = "Tilemaps/Isometric/Colliders/ColliderTiles/cube08-a";

    public static Tile GetBasicFloorTile()
    {
        return (Tile)Resources.Load(basicFloorTileName, typeof(Tile));
    }

    public static Tile GetBasicFloorCube()
    {
        return (Tile)Resources.Load(basicFloorCubeName, typeof(Tile));
    }

    public static Tile GetBasicColliderCube()
    {
        return (Tile)Resources.Load(colliderCubeName, typeof(Tile));
    }
}
