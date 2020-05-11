using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour {
    // Start is called before the first frame update
    Tilemap groundMap, floor1Map, collisionMap;

    void Start() {
        // Identify each tilemap
        foreach (Tilemap map in GetComponentsInChildren<Tilemap>()) {
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

        drawGround();
        drawObstacles();
    }

    void drawGround() {
        Tile basicFloorTile = TileLoader.GetBasicFloorTile();

        foreach (Vector3Int pos in groundMap.cellBounds.allPositionsWithin) {
            groundMap.SetTile(pos, basicFloorTile);
        }
    }

    void drawObstacles() {
        Tile basicFloorCube = TileLoader.GetBasicFloorCube();
        Tile collisionCube = TileLoader.GetBasicColliderCube();
        Vector3Int[] spaces = {
            new Vector3Int(0, 3, 0),
            new Vector3Int(2, 8, 0),
            new Vector3Int(4, 4, 0),
            new Vector3Int(6, 1, 0),
            new Vector3Int(6, 7, 0),
            new Vector3Int(7, 2, 0),
            new Vector3Int(9, 5, 0),
        };
        foreach (Vector3Int pos in spaces) {
            floor1Map.SetTile(pos, basicFloorCube);
            collisionMap.SetTile(pos, collisionCube);
        }
    }
    // Update is called once per frame
    void Update() {

    }
}
