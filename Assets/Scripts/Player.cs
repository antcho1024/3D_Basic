using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float spinSpeed = 360.0f;
    private float spinInput = 0.0f;
    private float moveInput = 0.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime); 
        transform.Rotate(Vector3.up * spinInput * spinSpeed * Time.deltaTime); 
    }
    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        spinInput = input.x;
        moveInput = input.y;
    }
    
}
