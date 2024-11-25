using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int setHealth = 3;
    [SerializeField] private int currentHealth;

    private GameObject deadPanel;

    private void Awake() 
    {    
        deadPanel = GameObject.Find("Dead Panel");
    }

    void Start()
    {
        deadPanel.SetActive(false);
        currentHealth = setHealth;
    }

    void Update()
    {
        if (this.currentHealth <= 0)
        {
            deadPanel.SetActive(true);
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerController.enabled = false;
            Time.timeScale = 0f;
            Debug.Log("Dead");
        }
    }

    public void SubtractHealth()
    {
        currentHealth -= 1;
    }
}
