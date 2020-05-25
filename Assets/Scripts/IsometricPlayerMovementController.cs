using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    List<Vector3Int> path;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    public void setPath(List<Vector3Int> newPath) {
        path = newPath;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = new Vector2();
        Vector2 currentPos = rbody.position;
        // Player-controlled movement
        // Vector2 moveVector = getMovementInput();

        //Game-controlled movement
        if (path != null && path.Count > 0) {
            Vector2 destination = IsoToRealConverter.IsometricToRealCoordinates(path[0]);
            Vector2 desiredMove = destination - rbody.position;

            if (desiredMove.magnitude < .05) {
                path.RemoveAt(0);
            }
            if (path.Count > 0) {
                destination = IsoToRealConverter.IsometricToRealCoordinates(path[0]);
                desiredMove = destination - rbody.position;
                moveVector = desiredMove.normalized;
            }

        }

        // Render the movement
        Vector2 movement = moveVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    Vector2 getMovementInput() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Clamp movement speed
        Vector2 gameMovement = Vector2.ClampMagnitude(
            new Vector2(horizontalInput, verticalInput), 1
        );

        // Translate to isometric movement
        float xMove = gameMovement.x + gameMovement.y;
        float yMove = 0.5f * (gameMovement.y - gameMovement.x);
        return new Vector2(xMove, yMove);
    }
    private void Update() {

    }
}
