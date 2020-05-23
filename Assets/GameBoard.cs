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
        List<Vector3Int> path = witchPathfinding.FindBurger();
        tileCreator.DrawPath(path);
    }

    // Update is called once per frame
    void Update()
    {

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

    public void PlaceBurger() {
        burgerIsoCoordinates = GenerateLegalIsoCoordinates();
        burger.transform.position = IsoToRealConverter.IsometricToRealCoordinates(burgerIsoCoordinates, 0.0f, -.75f);
    }

    Vector3Int GenerateLegalIsoCoordinates() {
        Vector3Int coordinates = new Vector3Int(Random.Range(0, mapSize), Random.Range(0, mapSize), 0);
        while (obstacleIsoCoordinates.Contains(coordinates)) {
            coordinates.x = Random.Range(0, mapSize);
            coordinates.y = Random.Range(0, mapSize);
        }

        return coordinates;
    }
}
