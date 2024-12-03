using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class MainMenu : MonoBehaviour
{
    private PlayerControls controls;

    public GameObject mapSelectPanel;
    public GameObject mainLayout;

    public Button mapButton;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Menu.Quit.performed += QuitGame;
        controls.Menu.MapSelect.performed += MapSelect;
        mapButton.Select();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Menu.Quit.performed -= QuitGame;
        controls.Menu.MapSelect.performed += MapSelect;
        controls.Disable();
    }

    private void Start()
    {
        mapSelectPanel.SetActive(false);
    }

    private void MapSelect(CallbackContext ctx)
    {
        if (ctx.performed) 
        {
            bool isActive = mapSelectPanel.activeSelf; 
            mapSelectPanel.SetActive(!isActive); 
            mainLayout.SetActive(isActive);
        }
    }

    public void CloseMapSelect()
    {
        mapSelectPanel.SetActive(false);
        mainLayout.SetActive(true);
    }

    private void QuitGame(CallbackContext ctx)
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
