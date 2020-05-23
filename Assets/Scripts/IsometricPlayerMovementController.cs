using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Clamp movement speed
        Vector2 gameMovement = Vector2.ClampMagnitude(
            new Vector2(horizontalInput, verticalInput), 1
        );
        // Translate to isometric movement
        float xMove = gameMovement.x + gameMovement.y;
        float yMove = (float)(0.5) * (gameMovement.y - gameMovement.x);
        Vector2 moveVector = new Vector2(xMove, yMove);
        // Render the movement
        Vector2 movement = moveVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private void Update() {

    }
}
