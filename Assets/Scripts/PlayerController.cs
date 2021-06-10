using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float MovementSpeed = 5f;
    [SerializeField] float RotationSpeed = 10f;
    [Header("Horizontal")]
    [SerializeField] float HPositionLimit = 30f;
    [SerializeField] float HRotationLimit = 30f;
    [SerializeField] float HThrustLimit = 15f;
    [SerializeField] float HThrustFactor = 0.05f;
    [Header("Vertical")]
    [SerializeField] float VPositionLimit = 20f;
    [SerializeField] float VRotationLimit = 40f;
    [SerializeField] float VThrustLimit = 15f;
    [SerializeField] float VThrustFactor = 0.05f;
    private float hPressValue, vPressValue;
    private float hRotate, vRotate;
    private float hThrustMovement, vThrustMovement;

    void Update()
    {
        Rotation();
        Movement();
    }

    private void FixedUpdate()
    {
        hThrustMovement = CalculateThrust(hThrustMovement, hPressValue);
        vThrustMovement = CalculateThrust(vThrustMovement, vPressValue);
    }

    private void Rotation()
    {
        hRotate = CalculateRotation(hRotate, hPressValue, HRotationLimit);
        vRotate = CalculateRotation(vRotate, vPressValue, VRotationLimit);
        Vector3 vector3 = new Vector3(-vRotate, 0, -hRotate);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(vector3.x, vector3.y, vector3.z), RotationSpeed * Time.deltaTime);
    }

    private void Movement()
    {

        hThrustMovement = Mathf.Clamp(hThrustMovement, -HThrustLimit, HThrustLimit);
        float hOffset = hPressValue * Time.deltaTime * MovementSpeed;
        float horizontalValue = Mathf.Clamp(transform.localPosition.x + hOffset + (hThrustMovement * HThrustFactor), -HPositionLimit, HPositionLimit);

        vThrustMovement = Mathf.Clamp(vThrustMovement, -VThrustLimit, VThrustLimit);
        float vOffset = vPressValue * Time.deltaTime * MovementSpeed;
        float verticalValue = Mathf.Clamp(transform.localPosition.y + vOffset + (vThrustMovement * VThrustFactor), -VPositionLimit, VPositionLimit);

        transform.localPosition = new Vector3(horizontalValue, verticalValue, 0);
    }


    public void OnMovementChange(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        hPressValue = direction.x;
        vPressValue = direction.y;
    }

    private float CalculateRotation(float rotationValue, float pressValue, float rotationLimit)
    {
        rotationValue += pressValue * RotationSpeed;
        rotationValue = Mathf.Clamp(rotationValue, -rotationLimit, rotationLimit);
        rotationValue = pressValue == 0 ? 0 : rotationValue;
        return rotationValue;
    }

    private float CalculateThrust(float thrustMovement, float pressValue)
    {
        thrustMovement += pressValue;
        if (pressValue == 0)
        {
            if (thrustMovement > 0)
            {
                thrustMovement--;
            }
            if (thrustMovement < 0)
            {
                thrustMovement++;
            }
            if (thrustMovement < 1 && thrustMovement > -1)
            {
                thrustMovement = 0;
            }
        }
        return thrustMovement;
    }


}
