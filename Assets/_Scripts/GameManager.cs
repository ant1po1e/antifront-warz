using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject deadPanel;
    public GameObject pausePanel;
    public Button continueButton;
    public Button rematchButton;

    public TMP_Text winnerText;

    public Animator animator;
    
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

        Time.timeScale = 0f;
        StartCoroutine(Countdown());
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

    public void SetWinnerText(string text)
    {
        winnerText.text = "Player " + text + " Wins!";
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        isPaused = !isPaused;
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

        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1f;
    }
}
