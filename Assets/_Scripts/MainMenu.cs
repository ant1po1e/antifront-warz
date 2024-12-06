using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    private PlayerControls controls;

    public GameObject mapSelectPanel;
    public GameObject mainLayout;

    public Animator animator;

    public Button mapButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("[Singleton] Trying to instantiate a second instance of a singleton class.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Menu.Quit.performed += QuitGame;
        controls.Menu.MapSelect.performed += MapSelect;
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
            mapButton.Select();
        }
    }

    public void CloseMapSelect()
    {
        mapSelectPanel.SetActive(false);
        mainLayout.SetActive(true);
    }

    private void QuitGame(CallbackContext ctx)
    {
        animator.SetTrigger("Start");
        Application.Quit();
    }
}
