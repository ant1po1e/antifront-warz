using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float speed;
    public float walkSpeed = 8f;
    public float runSpeed = 12f;

    public bool isRunning;

    private Vector2 inputVector = Vector2.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity = 0.1f;

    
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    

    void Start() {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
    }

    void Update() 
    {
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void Sprint()
    {
        speed = runSpeed;
    }

    public void Walk()
    {
        speed = walkSpeed;
    }
}
