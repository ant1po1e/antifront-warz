using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Button[] colorButtons; 
    [SerializeField]
    private GameObject colorTakenText;

    private Coroutine currentCoroutine;


    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    private void Start() 
    {
        colorTakenText.SetActive(false);    
    }

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SelectColor(Material mat)
    {
        if(!inputEnabled) { return; }

        if (PlayerConfigurationManager.Instance.IsColorAvailable(mat))
        {
            PlayerConfigurationManager.Instance.SetPlayerColor(playerIndex, mat);
            UpdateColorButtons();
            readyPanel.SetActive(true);
            readyButton.interactable = true;
            menuPanel.SetActive(false);
            readyButton.Select();
        }
        else
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            
            currentCoroutine = StartCoroutine(EnableText());
        }
    }

    private void UpdateColorButtons()
    {
        foreach (Button button in colorButtons)
        {
            var buttonImage = button.GetComponent<Image>();
            button.interactable = PlayerConfigurationManager.Instance.IsColorAvailable(buttonImage.material);
        }
    }

    private IEnumerator EnableText()
    {
        colorTakenText.SetActive(true);
        yield return new WaitForSeconds(2f);
        colorTakenText.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
