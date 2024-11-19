using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        var mover = FindObjectsOfType<PlayerMovement>();
        var index = playerInput.playerIndex;
        playerMovement = mover.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext ctx)
    {
        playerMovement.SetInputVector(ctx.ReadValue<Vector2>());
    }
}
