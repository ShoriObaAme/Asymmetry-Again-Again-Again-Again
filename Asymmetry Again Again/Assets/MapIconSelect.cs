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

	[SerializeField] private MapSelectAnimationHandler MSAH;

	private void Start()
	{
		mapImage.sprite = mapIcon.LevelIcon;
		mapName.text = mapIcon.LevelName;
		MSAH = transform.GetComponentInParent<MapSelectAnimationHandler>();
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
	}
}
