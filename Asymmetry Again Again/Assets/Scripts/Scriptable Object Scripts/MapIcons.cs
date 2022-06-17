using UnityEngine;
[CreateAssetMenu(menuName =("ScriptableObjects/Main Menu/Map Icons"), fileName = "Map Icon")]
public class MapIcons : ScriptableObject
{
	public string LevelName;
	public string SceneToLoad;
	public Sprite LevelIcon;
}
