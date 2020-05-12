using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {
    // Start is called before the first frame update
    Tilemap groundMap, floor1Map, collisionMap;
    private const int numObstacles = 17;
    private const int mapSize = 15;

    void Start() {
        // Identify each tilemap
        foreach (Tilemap map in GetComponentsInChildren<Tilemap>()) {
            // How to set tilemap size programmatically?
            // map.cellBounds.SetMinMax(new Vector3Int(0, 0, 0), new Vector3Int(mapSize, mapSize, 1));
            switch (map.name) {
                case "Groundmap":
                    groundMap = map;
                    break;
                case "Floor1map":
                    floor1Map = map;
                    break;
                case "Collision":
                    collisionMap = map;
                    break;
            }
        }

        DrawGround();
        DrawObstacles();
    }

    void DrawGround() {
        Tile basicFloorTile = TileLoader.GetBasicFloorTile();

        for (int mapX = 0; mapX < mapSize; mapX++) {
            for (int mapY = 0; mapY < mapSize; mapY++) {
                Vector3Int pos = new Vector3Int(mapX, mapY, 0);
                groundMap.SetTile(pos, basicFloorTile);
            }
        }

        // Is there a way to set tilemap size beforehand and use cellBounds.allPositionsWithin?
        // foreach (Vector3Int pos in groundMap.cellBounds.allPositionsWithin) {
        //     groundMap.SetTile(pos, basicFloorTile);
        // }
    }

    void DrawObstacles() {
        Tile basicFloorCube = TileLoader.GetBasicFloorCube();
        Tile collisionCube = TileLoader.GetBasicColliderCube();

        foreach (Vector3Int pos in GenerateObstacleCoordinates()) {
            floor1Map.SetTile(pos, basicFloorCube);
            collisionMap.SetTile(pos, collisionCube);
        }
    }

    HashSet<Vector3Int> GenerateObstacleCoordinates() {
        Vector3Int[] coordinates = new Vector3Int[numObstacles];
        HashSet<Vector3Int> coordinateSet = new HashSet<Vector3Int>();
        while (coordinateSet.Count < numObstacles) {
            int x = Random.Range(0, mapSize);
            int y = Random.Range(0, mapSize);
            coordinateSet.Add(new Vector3Int(x, y, 0));
        }
        return coordinateSet;
    }
    // Update is called once per frame
    void Update() {

    }
}
