using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    private PlayerControls controls;

    [SerializeField] private GameObject mapSelectPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject mainLayout;

    [Space]

    public Animator animator;

    public Button mapButton;
    public Slider musicSlider;

    private bool isOptionActive = true;

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
        controls.Menu.Options.performed += Options;
        controls.Menu.Credits.performed += Credits;
        controls.Menu.Tutorial.performed += Tutorial;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Menu.Quit.performed -= QuitGame;
        controls.Menu.MapSelect.performed -= MapSelect;
        controls.Menu.Options.performed -= Options;
        controls.Menu.Credits.performed -= Credits;
        controls.Menu.Tutorial.performed -= Tutorial;
        controls.Disable();
    }

    private void Start()
    {
        mapSelectPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() 
    {
        if (isOptionActive == true)
        {   
            creditsPanel.SetActive(false);
            tutorialPanel.SetActive(false);
        }    
    }

    private void MapSelect(CallbackContext ctx)
    {
        if (ctx.performed) 
        {
            bool isActive = mapSelectPanel.activeSelf; 
            mapSelectPanel.SetActive(!isActive); 
            creditsPanel.SetActive(false);
            mainLayout.SetActive(isActive);
            mapButton.Select();
        }
    }

    private void Options(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isOptionActive = optionsPanel.activeSelf; 
            optionsPanel.SetActive(!isOptionActive); 
            mapSelectPanel.SetActive(false); 
            mainLayout.SetActive(isOptionActive);
            musicSlider.Select();
        }
    }

    private void Credits(CallbackContext ctx)
    {
        if (!isOptionActive)
        {
            if (ctx.performed)
            {
                bool isActive = creditsPanel.activeSelf; 
                creditsPanel.SetActive(!isActive); 
                tutorialPanel.SetActive(false);
            } 
        }
    }

    private void Tutorial(CallbackContext ctx)
    {
        if (!isOptionActive)
        {
            if (ctx.performed)
            {
                bool isActive = tutorialPanel.activeSelf;
                tutorialPanel.SetActive(!isActive);
                creditsPanel.SetActive(false); 
            }
        }
    }

    public void CloseMapSelect()
    {
        mapSelectPanel.SetActive(false);
        mainLayout.SetActive(true);
    }

    private void QuitGame(CallbackContext ctx)
    {
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
