using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressStartToBegin1 : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;
	public string SceneToLoad;
	public bool fadeToBlack = false;
	[SerializeField] private Animator animator;

	public void Awake()
	{
		animator = GetComponent<Animator>();
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		gameManager.ChangeGameMode(Mode.TITLE_SCREEN);
	}

	public void PlayAnim()
	{
		animator.Play("Fade To Black");
		return;
	}

	public void LoadMainMenu()
	{
		StartCoroutine(LoadTitleScene());
		return;
	}

	IEnumerator LoadTitleScene()
	{
		gameManager.ChangeGameMode(Mode.MAIN_MENU);
		AsyncOperation asynLoad = SceneManager.LoadSceneAsync(SceneToLoad);
		while (!asynLoad.isDone)
		{
			yield return null;
		}
	}
}
