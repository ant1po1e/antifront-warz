using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    #region Singleton
    public static PlayerConfigManager instance { get; set;}

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }
    #endregion

    private List<PlayerConfiguration> playerConfigs;
    [SerializeField] private int maxPlayer = 2;

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].PlayerMat = color;
    }

    public void PlayerIsReady(int index)
    {
        playerConfigs[index].IsReady = true;
        
        if (playerConfigs.Count == maxPlayer && playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void PlayerJoinHandler(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);

        pi.transform.SetParent(transform);

        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMat { get; set; }
}
