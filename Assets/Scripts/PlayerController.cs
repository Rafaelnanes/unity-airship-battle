using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [Header("Control Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float horizontalLimit = 10f;
    [SerializeField] float verticalLimit = 10f;
    float horizontalPressValue, verticalPressValue;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float xOffset = horizontalPressValue * Time.deltaTime * speed;
        var horizontalValue = Mathf.Clamp(transform.localPosition.x + xOffset, -horizontalLimit, horizontalLimit);
        float yOffset = verticalPressValue * Time.deltaTime * speed;
        var verticalValue = Mathf.Clamp(transform.localPosition.y + yOffset, -verticalLimit, verticalLimit);
        characterController.transform.localPosition = new Vector3(horizontalValue, verticalValue, 0);
    }

    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        horizontalPressValue = direction.x;
        verticalPressValue = direction.y;
    }


}
