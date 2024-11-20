using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int PlayerIndex;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] GameObject readyPanel;
    [SerializeField] GameObject colorSelectPanel;
    [SerializeField] private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetColor(Material color)
    {
        if (!inputEnabled) { return; }

        PlayerConfigManager.instance.SetPlayerColor(PlayerIndex, color);
        readyPanel.SetActive(true);
        readyButton.Select();
        colorSelectPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigManager.instance.PlayerIsReady(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
