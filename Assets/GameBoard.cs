using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public HashSet<Vector3Int> obstacleIsoCoordinates;
    const int numObstacles = 37;
    public const int mapSize = 15;
    TileCreator tileCreator;
    public Vector3Int burgerIsoCoordinates;
    public Vector3Int witchIsoCoordinates;
    GameObject burger;
    GameObject witch;
    Pathfinding witchPathfinding;

    void Awake() {
        tileCreator = GetComponentInChildren<TileCreator>();
        burger = GameObject.Find("BurgerObject");
        witch = GameObject.Find("Player_Isometric_Witch");
        witchPathfinding = witch.GetComponent<Pathfinding>();
    }

    void Start()
    {
        obstacleIsoCoordinates = GenerateObstacleCoordinates();

        tileCreator.DrawGround(mapSize);
        tileCreator.DrawObstacles(obstacleIsoCoordinates);
        PlaceBurger();
        PlaceWitch();
        StartBurgerHunt();
    }

    public void StartBurgerHunt() {
        List<Vector3Int> pathToBurger = witchPathfinding.FindBurger();
        tileCreator.DrawPath(pathToBurger);
        witch.GetComponent<IsometricPlayerMovementController>().setPath(pathToBurger);

    }

    public void Restart() {
        FindWitch();
        tileCreator.DrawGround(mapSize);
        tileCreator.DrawObstacles(obstacleIsoCoordinates);
        // Re-route the witch to the new burger
        StartBurgerHunt();
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

    public void PlaceWitch() {
        witchIsoCoordinates = GenerateLegalIsoCoordinates();
        witch.transform.position = IsoToRealConverter.IsometricToRealCoordinates(witchIsoCoordinates);
    }

    public void FindWitch() {
        witchIsoCoordinates = IsoToRealConverter.RealToIsometricCoordinates(witch.transform.position);
    }

    public void PlaceBurger() {
        burgerIsoCoordinates = GenerateLegalIsoCoordinates();
        burger.transform.position = IsoToRealConverter.IsometricToRealCoordinates(burgerIsoCoordinates, 0.0f, -.75f);
        print("Burger at " + burgerIsoCoordinates);
    }

    Vector3Int GenerateLegalIsoCoordinates() {
        Vector3Int coordinates = new Vector3Int(Random.Range(0, mapSize), Random.Range(0, mapSize), 0);
        while (obstacleIsoCoordinates.Contains(coordinates)) {
            coordinates.x = Random.Range(0, mapSize);
            coordinates.y = Random.Range(0, mapSize);
        }

        return coordinates;
    }

    void FixedUpdate() {
        Vector3Int oldWitch = new Vector3Int(witchIsoCoordinates.x, witchIsoCoordinates.y, witchIsoCoordinates.z);
        FindWitch();
        if (oldWitch != witchIsoCoordinates) {
            print("witch at " + witchIsoCoordinates);
        }

    }
}
