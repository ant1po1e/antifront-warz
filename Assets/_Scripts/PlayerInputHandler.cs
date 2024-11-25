using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerController playerController;

    private GunController gun;

    [SerializeField] private MeshRenderer playerMesh;
    
    private PlayerControls controls; 

    private void Awake() 
    {
        playerController = GetComponent<PlayerController>();
        
        gun = GetComponent<GunController>();
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.playerMaterial;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        playerConfig.Input.onActionTriggered += Input_onShootTriggered;
        playerConfig.Input.onActionTriggered += Input_onSprintTriggered;
    }

    private void Input_onActionTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == controls.PlayerMovement.Movement.name)
        {
            OnMove(ctx);
        }
    }

    private void Input_onShootTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == controls.PlayerMovement.Shoot.name)
        {
            OnShoot();
        }
    }

    private void Input_onSprintTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == controls.PlayerMovement.Sprint.name)
        {
            OnSprint(ctx);
        }
    }

    public void OnMove(CallbackContext ctx)
    {
        playerController.SetInputVector(ctx.ReadValue<Vector2>());
    }

    public void OnShoot()
    {
        gun.Shoot();
    }

    public void OnSprint(CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerController.Sprint();
        }else if (ctx.canceled)
        {
            playerController.Walk();
        }
    }
}
