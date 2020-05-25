using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    GameBoard gameBoard;
    // Start is called before the first frame update
    void Start()
    {
        gameBoard = transform.parent.GetComponent<GameBoard>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        print("You triggered the burger!");
        gameBoard.PlaceBurger();
        gameBoard.Restart();
    }

    // Update is called once per frame
    void Update() {

    }
}
