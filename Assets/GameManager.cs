using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public Player player;
    [SerializeField] private GameObject gameOverPanel;
    
    public Animator cameraAnimator;

    private bool gameOver = false;

    private void Awake()
    {
        // we only want one game manager in our game
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        cameraAnimator = Camera.main.GetComponent<Animator>();
        // TODO get the player somehow idk
    }

    private void Update()
    {
        // temp
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerDeath();
        }
    }

    // should be called when player dies
    public void playerDeath()
    {
        if (!gameOver)
        {
            cameraAnimator.SetBool("Dead", true);
            gameOver = true;
            player.Die();
            gameOverPanel.SetActive(true);
            // TODO death screen
        }
    }
}
