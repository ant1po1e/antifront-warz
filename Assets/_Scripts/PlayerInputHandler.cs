using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement playerMovement;

    [SerializeField] private MeshRenderer playerMesh;
    
    private PlayerControls controls; 

    private void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.PlayerMat;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == controls.PlayerInputSystem.Movement.name)
        {
            OnMove(ctx);
        }
    }

    public void OnMove(CallbackContext ctx)
    {
        playerMovement.SetInputVector(ctx.ReadValue<Vector2>());
    }
}
