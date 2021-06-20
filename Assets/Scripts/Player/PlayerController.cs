using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActions playerActions;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        playerActions.OnShoot(context.ReadValue<float>());
    }

    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        playerMovement.OnMovementChange(direction.x, direction.y);
    }

    public void OnBomb(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValue<float>() > 0)
        {
            playerActions.OnBomb();
        }

    }
}
