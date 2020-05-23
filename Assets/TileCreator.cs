using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {
    // Start is called before the first frame update
    Tilemap groundMap, floor1Map, collisionMap;

    void Awake() {
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
    }

    public void DrawGround(int mapSize) {
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

    public void DrawObstacles(HashSet<Vector3Int> obstacleCoordinates) {
        Tile basicFloorCube = TileLoader.GetBasicFloorCube();
        Tile collisionCube = TileLoader.GetBasicColliderCube();

        foreach (Vector3Int pos in obstacleCoordinates) {
            floor1Map.SetTile(pos, basicFloorCube);
            collisionMap.SetTile(pos, collisionCube);
        }
    }

    public void DrawPath(List<Vector3Int> path) {
        Tile moltenCenterTile = TileLoader.GetMoltenCenterTile();
        foreach(Vector3Int location in path) {
            groundMap.SetTile(location, moltenCenterTile);
        }
    }

    // }
    // Update is called once per frame
    void Update() {

    }
}
