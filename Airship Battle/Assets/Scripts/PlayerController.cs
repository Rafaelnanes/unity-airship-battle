using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;
    [SerializeField] float speed = 10f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        characterController.Move(moveVector * speed * Time.fixedDeltaTime);
    }

    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log(direction.x);
        moveVector = new Vector3(direction.x, 0, direction.y);
    }


}
