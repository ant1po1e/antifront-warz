using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int setHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private ParticleSystem[] bloodFX;

    private GameObject deadPanel;
    private Button rematchButton;

    private bool isCoroutineRunning = false;

    private void Awake() 
    {    
        deadPanel = GameManager.Instance.deadPanel;
        rematchButton = GameManager.Instance.rematchButton;
    }

    void Start()
    {
        deadPanel.SetActive(false);
        currentHealth = setHealth;
    }

    void Update()
    {
        if (this.currentHealth <= 0 && !isCoroutineRunning)
        {
            isCoroutineRunning = true; 
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerController.enabled = false;
            StartCoroutine(PauseDelay());

            GameManager.Instance.isDead = true;
        }
    }

    private IEnumerator PauseDelay()
    {
        rematchButton.Select();
        yield return new WaitForSeconds(1f);
        deadPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SubtractHealth()
    {
        currentHealth -= 1;

        PlayRandomBloodFX();
    }

    private void PlayRandomBloodFX()
    {
        if (bloodFX.Length == 0) return; 

        int randomIndex = Random.Range(0, bloodFX.Length);
        ParticleSystem selectedFX = bloodFX[randomIndex];

        if (selectedFX != null)
        {
            selectedFX.Play();

            StartCoroutine(StopParticleAfterPlay(selectedFX));
        }
    }

    private IEnumerator StopParticleAfterPlay(ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.main.duration);

        yield return new WaitUntil(() => !particle.IsAlive());

        particle.Stop();
    }
}
