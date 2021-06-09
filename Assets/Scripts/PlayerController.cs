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
        if (horizontalPressValue == 0)
        {
            if (isRotatingRight())
            {
                float horizontalValue = move(0.3f, 0.3f);
                transform.localPosition = new Vector3(horizontalValue, 0, 0);
            }
        }
        Vector3 vector3 = new Vector3(0, 0, -xRotate);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(vector3.x, vector3.y, vector3.z), rotationSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        Debug.Log($"transform.localEulerAngles.z: {transform.localEulerAngles.z}, horizontalPressValue : {horizontalPressValue}");
        float horizontalIntensity = 0.5f;
        if (horizontalPressValue == 1 && isRotatingRight())
        {
            horizontalIntensity = 1;
        }
        if (horizontalPressValue == -1 && isRotatingLeft())
        {
            horizontalIntensity = 1;
        }

        float horizontalValue = move(horizontalPressValue, horizontalIntensity);

        float yOffset = verticalPressValue * Time.deltaTime * speedOffset;
        float verticalValue = Mathf.Clamp(transform.localPosition.y + yOffset, -verticalPosition, verticalPosition);

        transform.localPosition = new Vector3(horizontalValue, verticalValue, 0);
    }

    private bool isRotatingLeft()
    {
        return transform.localEulerAngles.z < 65;
    }

    private bool isRotatingRight()
    {
        return transform.localEulerAngles.z > 65;
    }

    private float move(float horizontalPressValue, float horizontalIntensity)
    {
        float xOffset = horizontalPressValue * Time.deltaTime * (speedOffset * horizontalIntensity);
        return Mathf.Clamp(transform.localPosition.x + xOffset, -horizontalPosition, horizontalPosition);
    }

    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        horizontalPressValue = direction.x;
        verticalPressValue = direction.y;
    }


}
