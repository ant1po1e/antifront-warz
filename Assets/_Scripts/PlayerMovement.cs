using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float speed;
    public float walkSpeed = 8f;
    public float runSpeed = 15f;

    private Vector2 inputVector = Vector2.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity = 0.1f;
    
    [SerializeField] private int playerIndex = 0;
    

    void Start() {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    void Update() {
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }
}
