using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float speedOffset = 50f;
    [SerializeField] float rotationOffset = 10f;
    [SerializeField] float rotationSpeed = 2f;
    [Header("Horizontal Limit")]
    [SerializeField] float horizontalPosition = 30f;
    [SerializeField] float horizontalRotation = 90f;
    [Header("Vertical Limit")]
    [SerializeField] float verticalPosition = 20f;
    [SerializeField] float verticalRotation = 40f;
    float horizontalPressValue, verticalPressValue;
    private float xRotate, yRotate;

    void Update()
    {
        Rotation();
        Movement();
    }

    private void FixedUpdate()
    {

    }

    private void Rotation()
    {
        xRotate += horizontalPressValue * rotationOffset;
        xRotate = Mathf.Clamp(xRotate, -horizontalRotation, horizontalRotation);
        xRotate = horizontalPressValue == 0 ? 0 : xRotate;
        Vector3 vector3 = new Vector3(0, 0, -xRotate);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(vector3.x, vector3.y, vector3.z), rotationSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        float xOffset = horizontalPressValue * Time.deltaTime * speedOffset;
        float horizontalValue = Mathf.Clamp(transform.localPosition.x + xOffset, -horizontalPosition, horizontalPosition);

        float yOffset = verticalPressValue * Time.deltaTime * speedOffset;
        float verticalValue = Mathf.Clamp(transform.localPosition.y + yOffset, -verticalPosition, verticalPosition);

        transform.localPosition = new Vector3(horizontalValue, verticalValue, 0);
    }


    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        horizontalPressValue = direction.x;
        verticalPressValue = direction.y;
    }


}
