using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
    private PlayerActions playerAction;

    private void Start()
    {
        playerAction = GetComponent<PlayerActions>();
    }

    void Update()
    {
        Rotation();
        Movement();
    }

    public void OnMovementChange(float hPressValue, float vPressValue)
    {
        this.hPressValue = hPressValue;
        this.vPressValue = vPressValue;
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
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(vector3), RotationSpeed * Time.deltaTime);
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
