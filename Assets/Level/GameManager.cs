using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public Player player;
    [SerializeField] private GameObject gameOverPanel;
    
    private Interactor interactor;
    public Animator cameraAnimator;

    public float totalFunds = 0;
    public float currentRent = 0;
    public HashSet<System.Type> collectedMerch = new HashSet<System.Type>();

    private bool gameOver = false;

    private void Awake()
    {
        // we only want one game manager in our game
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        cameraAnimator = Camera.main.GetComponent<Animator>();
        // TODO get the player somehow idk

        interactor = player.GetComponent<Interactor>();
    }

    private void Update()
    {
        // temp
        if (Input.GetKeyDown(KeyCode.C))
        {
            handlePlayerDeath();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(player.inventory.ToString());
        }
    }

    // should be called when player dies
    public void handlePlayerDeath()
    {
        if (!gameOver)
        {
            cameraAnimator.SetBool("Dead", true);
            gameOver = true;
            player.Die();
            StartCoroutine(waitToOpenGameOverPanel());
            // TODO death screen
        }
    }

    IEnumerator waitToOpenGameOverPanel()
    {
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
        openGameOverPanel();
    }

    // should be called by the animator when the grayscale animation is over
    public void openGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    // should be called by the player when they switch transmutations, should tell all relevant objects about this change
    public void handleTransmutationChanged(TransmutationBase player)
    {
        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow = player.gameObject.transform;
        interactor.handleTransmutationChanged();
    }

    public bool endDay()
    {
        // TODO animation and scene transition
        foreach (MerchBase merchItem in player.inventory.merchList)
        {
            collectedMerch.Add(merchItem.GetType());
        }
        totalFunds += player.inventory.totalCost;
        player.inventory.clear();
        return true;
    }
}
