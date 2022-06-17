using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapIconSelect : MonoBehaviour
{
	[Header("Map Icon Scriptable Object")]
    public MapIcons mapIcon;

    [Header("Map Icon Image / Text")]
    public TextMeshProUGUI LevelName;
    public Sprite LevelIcon;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        LevelName.text = mapIcon.LevelName;
        LevelIcon = mapIcon.LevelIcon;
    }

    public void MapSelectAnim()
	{

	}

    private void LoadMap()
	{

	}
}
