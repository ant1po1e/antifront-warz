using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject deadPanel;
    public GameObject pausePanel;
    public Button continueButton;
    public Button rematchButton;
    
    public bool isDead;

    private PlayerControls controls;

    private bool isPaused = false;

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
        controls.Menu.Pause.performed += Pause;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Menu.Pause.performed -= Pause;
        controls.Disable();
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        deadPanel.SetActive(false);
    }

    private void Pause(CallbackContext ctx)
    {
        if (isDead != true)
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);

            if (isPaused)
            {
                continueButton.Select();
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("DontDestroy"))
            {
                Destroy(obj);
            }
        }

        SceneManager.LoadScene("Main Menu");
    }
}
