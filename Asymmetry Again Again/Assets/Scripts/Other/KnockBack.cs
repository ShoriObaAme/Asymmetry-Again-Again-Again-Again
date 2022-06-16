using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnockBack : MonoBehaviour
{
    [Range(1, 5)]
    public int Lives = 3;
    public float knockBackValue;
    public float KnockBackDisplay;
    public TextMeshProUGUI KnockBackDisplayText;
    public TextMeshProUGUI LivesText;
    private float hello;
    [SerializeField] private MatchManager MM;
    private SpawnPoints spawnPoints;
    void Start()
    {
        MM = GameObject.Find("Game Wide Manager").GetComponent<MatchManager>();
        spawnPoints = SpawnPoints.SP;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        KnockBackDisplay = (knockBackValue / 100);
        DisplayText();
    }

    private void DisplayText()
	{
        KnockBackDisplayText.text = "%" + KnockBackDisplay.ToString("000");
        LivesText.text = "Lives: " + Lives.ToString("0");
    }

    public void Die()
    {
        if (Lives > (Lives / Lives))
		{
            spawnPoints.RespawnPlayer(this.gameObject);
            Lives--;
            return;
		}
        else if (Lives <= (Lives / Lives))
		{
            Lives--;
            EditScoreBoardStats();
            PlayerCharacterController PCC = this.gameObject.GetComponent<PlayerCharacterController>();
            Camera cam = gameObject.GetComponentInChildren<Camera>();
            lookAtBody LAB = cam.gameObject.GetComponent<lookAtBody>();
            PCC.enabled = false;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            LAB.IsPlayerDead = true;
            return;
        }
        
	}

    private void EditScoreBoardStats()
	{
        MM.players.Remove(gameObject);
        MM.playersLeftAlive--;
        MM.playersDead++;
        return;
    }
}
