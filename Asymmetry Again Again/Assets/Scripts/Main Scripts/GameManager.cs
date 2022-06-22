using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Mode
{
	DEFAULT, //Fallback mode. Mode should NEVER be this.
	MULTIPLAYER, //Multiplayer PvP
	OPIONS, //OptionsMenu
	CREDITS, //CreditsMenu 
	MAIN_MENU, //MainMenu
	TITLE_SCREEN //Title Screen for the game.
} 


public class GameManager : MonoBehaviour
{
	public Mode gameMode;
	public static GameManager gameManager;
	public static event Action<Mode> OnModeChange;
	public MatchManager matchManager;
	[Range(1, 4)]public int PlayerToAdd;
	private void Awake()
	{
		#region singleton
		gameManager = this;

		if (gameManager == null)
		{
			Debug.LogError("No Game Manager found!!!");
		}
		if (gameMode == Mode.DEFAULT) Quit();

		matchManager = GetComponent<MatchManager>();

		if (matchManager == null)
		{
			Debug.LogError("No Match Manager Found!!!");
		}
#endregion
		DontDestroyOnLoad(this);
		Application.targetFrameRate = 60;
	}

	public void ChangeGameMode(Mode newMode)
	{
		gameMode = newMode;

		switch (newMode)
		{
			case Mode.DEFAULT:
				Quit();
				break;
			case Mode.MULTIPLAYER:
				StartMultiplayer();
				break;
			case Mode.OPIONS:
				break;
			case Mode.CREDITS:
				break;
			case Mode.MAIN_MENU:
				EndMultiplayer();
				break;
			case Mode.TITLE_SCREEN:
				DisableMatchManager();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(newMode), newMode, null);
		}

		OnModeChange?.Invoke(gameMode);
	}

	private void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else 
Application.Quit();
#endif
	}

	void StartMultiplayer()
	{
		PlayerInputManager PIM = GetComponent<PlayerInputManager>();
		PIM.enabled = true;
		gameMode = Mode.MULTIPLAYER;
		matchManager.enabled = true;
	}

	private void EndMultiplayer()
	{
		PlayerInputManager PIM = GetComponent<PlayerInputManager>();
		PIM.enabled = false;
		matchManager.enabled = false;
		return;
	}

	private void DisableMatchManager()
	{
		matchManager.enabled = false;
		return;
	}
}
