 using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public enum MatchStatus 
{
    NOT_STARTED, //The match has not yet started.
    STARTING, //The match is about to begin.
    IN_PROGRESS, //The match is currently in progress.
    ENDING //the match is over.
}
public class MatchManager : MonoBehaviour
{
    [SerializeField] public MatchStatus matchStatus;
    [SerializeField] public static MatchManager instance;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerInputManager PIM;
    public List<GameObject> players;

    [Header("Mode Stats / Scoreboard Text")]
    [Header("Players Left Alive")]
    public int playersLeftAlive;
    [Header("Players Dead")]
    public int playersDead;
    [Header("Time")]
    [Tooltip("Enter match length in Seconds. For example, for a one minute match, enter 60. For a two minute match, enter 120, and so on and so forth.")]
    public float matchDurationInSeconds = 180;
    public bool timerIsRunning = false;
    public bool MatchStarting = false;
    public float matchStartTime = 5f;
    [SerializeField] private bool matchHasStarted = false;
    [SerializeField] private bool PlayersInLobby = false;

    [Header("Text")]
    public TextMeshProUGUI timeText;
    public Canvas MultiplayerUI;

    [Header("Animation")]
    private Animator anim;

    //[Header("Readying Up")]
    //[SerializeField] private GameObject ReadyUpBox;

    public static event Action<MatchStatus> OnMatchStatusChange;

	private void Awake()
	{
        instance = this;
	}

	private void OnEnable()
	{
        ChangeMatchStatus(MatchStatus.NOT_STARTED);
        MultiplayerUI.enabled = true;
        SpawnPoints SP = GetComponent<SpawnPoints>();
        SP.enabled = true;
	}

	private void OnDisable()
	{
        ChangeMatchStatus(MatchStatus.NOT_STARTED);
	}

	// Start is called before the first frame update
	void Start()
    {
        //timeText = GameObject.Find("Multiplayer Canvas").GetComponentInChildren<TextMeshProUGUI>();
        timeText.text = "Press A to join.";
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
            players.Add(player);
		}
		anim = GameObject.FindGameObjectWithTag("Multiplayer Canvas").GetComponent<Animator>();

        if (gameManager.gameMode != Mode.MULTIPLAYER)
		{
            this.enabled = false;
		}
        else matchStatus = MatchStatus.NOT_STARTED;
        if (PlayersInLobby == false)
		{
            if (players.Count <= 1)
			{
                timeText.text = "Press any button to join game.";
			}
        }
        /*if (players.Count > 1 && players.Count != 4)
        {
            timeText.text = ReadyUpText;
        }
        else if (players.Count == 4)
        {
            timeText.text = "Please enter the designated area to begin the match.";
        }*/
    }

    public void ChangeMatchStatus(MatchStatus status)
	{
        matchStatus = status;

        switch (status)
		{
            case MatchStatus.NOT_STARTED:
                //LocateReadyUpBox();
                EndReadyUp();
                break;
            case MatchStatus.STARTING:
                MatchStarting = true;
                break;
            case MatchStatus.IN_PROGRESS:
                BeginMatch();
                break;
            case MatchStatus.ENDING:
                EndMatch();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
		}
        OnMatchStatusChange?.Invoke(matchStatus);
	}
    // Update is called once per frame
    void Update()
    {
        if (players.Count >= 2 && !matchHasStarted)
        {
            ChangeMatchStatus(MatchStatus.STARTING);
        }
        if (timerIsRunning)
		{
            matchDurationInSeconds -= Time.deltaTime;
            MatchTrack();
		}

        //if ((timerIsRunning && matchDurationInSeconds <= 0) || (playersDead >= 3 && playersLeftAlive <= 1 && matchStatus == MatchStatus.IN_PROGRESS))
        if (playersDead >= (players.Count - 1) && playersLeftAlive <= 1 && matchStatus == MatchStatus.IN_PROGRESS)
        {
            ChangeMatchStatus(MatchStatus.ENDING);
		}

        if (MatchStarting && matchStatus == MatchStatus.STARTING)
		{
            timeText.text = matchStartTime.ToString("0");
            matchStartTime -= Time.deltaTime;
            if (matchStartTime <= 0)
            {
                MatchStarting = false;
                timeText.text = "Begin!";
                Invoke("StartMatch", 1.5f);
            }
		}

        if (matchHasStarted && matchStatus == MatchStatus.IN_PROGRESS)
        {
            timerIsRunning = true;
        }

        if (matchDurationInSeconds <= 0 && matchHasStarted && matchStatus == MatchStatus.IN_PROGRESS)
        {
            timerIsRunning = false;
            ChangeMatchStatus(MatchStatus.ENDING);
        }
    }

    private void MatchTrack()
	{
        DisplayTime(matchDurationInSeconds);
        //PlayersLeftAliveText.text = "Players Alive: " + playersLeftAlive.ToString("0");
      //xx  PlayersDeadText.text = "Players Dead:  " + playersDead.ToString("0");
        return;
    }

    private void BeginMatch()
	{
        PIM.enabled = false;
        matchHasStarted = true;
        MatchStarting = false;
        timerIsRunning = true;
        GameObject.FindGameObjectWithTag("Ready Up Box").SetActive(false);
	}


    private void DisplayTime(float timeToDisplay)
	{
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

    private void LocateReadyUpBox()
	{
     //   ReadyUpBox = GameObject.FindGameObjectWithTag("Ready Up Box");
	}

    private void EndMatch()
	{
        timerIsRunning = false;
        foreach (GameObject player in players)
		{
            UpdatedCharacterController PCC = player.GetComponent<UpdatedCharacterController>();
            PCC.enabled = false;
		}
        
        if (playersDead == 0)
        {
            timeText.text = ("How did none of you die?!? We really need to keep working on this game.");
        }
        else if (playersDead > 0 && playersDead != 4)
        {
            PlayerIndex PI = players[0].GetComponent<PlayerIndex>();
            timeText.text = "Game Over. Player " + PI.currentPlayerNo + " wins!";
        }
        else if (playersDead == 4)
        {
            timeText.text = ("Either you're good or bad. There is no inbetween.");
        }
        Invoke("LoadLobby", 5f);
	}

    private void StartMatchCountdown(float secondsUntilStart)
	{
        timeText.text = secondsUntilStart.ToString("0");
        secondsUntilStart -= Time.deltaTime;
        if (secondsUntilStart <= 0)
		{
            MatchStarting = false;
            timeText.text = "Begin!";
            Invoke("StartMatch", 1.5f);
            return;
		}
	}

    private void StartMatch()
	{
        ChangeMatchStatus(MatchStatus.IN_PROGRESS);
        matchHasStarted = true;
        foreach (GameObject player in players)
		{
            playersLeftAlive++;
		}
        return;
	}

    private void ReturnToLobbyAnim()
	{
        //anim.Play("FadeToBlack");
        Invoke("LoadLobby", 5f);
        return;
	}

    public void LoadLobby()
	{
        MultiplayerUI.enabled = false;
        gameManager.ChangeGameMode(Mode.MAIN_MENU);
        SceneManager.LoadScene("Map Select");
        return;
	}

    private void EndReadyUp()
	{
        timeText = GameObject.FindGameObjectWithTag("Time Text").GetComponent<TextMeshProUGUI>();
	}
}
