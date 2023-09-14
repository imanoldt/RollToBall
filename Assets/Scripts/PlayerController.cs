using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Vector2 jump;

    [SerializeField] float speed = 0;
    [SerializeField] float jumpForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void OnJump(InputValue jumpValue)
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    bool IsGrounded()
    {
        return GetComponent<Rigidbody>().velocity.y == 0;
    }
}
