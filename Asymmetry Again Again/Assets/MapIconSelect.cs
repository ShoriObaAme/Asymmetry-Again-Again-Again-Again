using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapIconSelect : MonoBehaviour
{
	[Header("Map Icon Scriptable Object")]
	public MapIcons mapIcon;

	[Header("Text / Image")]
	public Image mapImage;
	public TextMeshProUGUI mapName;

	private GameManager gameManager;

	[SerializeField] private MapSelectAnimationHandler MSAH;

	private void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		MSAH = transform.GetComponentInParent<MapSelectAnimationHandler>();
		mapImage.sprite = mapIcon.LevelIcon;
		mapName.text = mapIcon.LevelName;
	}

	public void DebugButton()
	{
		Debug.Log("This button (" + mapIcon.name + ") works as intended");
	}

	public void MapSelectAnim()
	{
		MSAH.DecideAnimationToPlay();
		StartCoroutine(LoadScene());
	}

	public IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(mapIcon.SceneToLoad);
		gameManager.ChangeGameMode(Mode.MULTIPLAYER);
	}
}
