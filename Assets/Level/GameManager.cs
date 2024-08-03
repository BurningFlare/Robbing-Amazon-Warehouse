using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public Player player;
    [SerializeField] private GameObject gameOverPanel;

    private List<MerchBase> merch;

    public Animator cameraAnimator;

    private bool gameOver = false;
    public static event Action OnDayEnd;
    public static event Action OnPlayerDeath;

    // these members should probably go into a different script at some other point since they aren't tied to the day
    public float totalFunds = 0;
    public float currentRent = 0;
    public HashSet<string> collectedMerch = new HashSet<string>();

    private void Awake()
    {
        // we only want one game manager in our game
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        SetupForLevel();
        // TODO get the player somehow idk

        RegisterEventBindings();
    }

    private void RegisterEventBindings()
    {
        Player.onTransmutation += handleTransmutationChanged;
    }

    private void handleTransmutationChanged(TransmutationBase obj)
    {
        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow = obj.gameObject.transform;
    }

    private void SetupForLevel()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        generateMerch();
        gameOver = false;
    }

    private void generateMerch()
    {
        // getting all merch that is in the game for some reason
        merch = new List<MerchBase>(Resources.FindObjectsOfTypeAll<MerchBase>());
        // TODO actually generate the merch
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
        // gotta check if the game is already over or not
        if (!gameOver)
        {
            OnPlayerDeath?.Invoke();
            cameraAnimator.SetBool("Dead", true);
            gameOver = true;
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

    public bool endDay()
    {
        // TODO animation and scene transition
        foreach (MerchBase merchItem in player.inventory.merchList)
        {
            // delete the numbering added by unity
            // FIXME this might actually be (clone) and not a number so uhhhhh fix that if that is the case
            Match match = Regex.Match(merchItem.name, @"^(.*) \([0-9]+\)$");
            if (match.Success)
            {
                collectedMerch.Add(match.Groups[1].Value);
            } else
            {
                collectedMerch.Add(merchItem.name);
            }
        }
        totalFunds += player.inventory.totalCost;
        player.inventory.clear();

        string items = "";
        foreach (string itemName in collectedMerch)
        {
            items += itemName + " ";
        }
        Debug.Log(items);
        Debug.Log(totalFunds);
        return true;
    }
}