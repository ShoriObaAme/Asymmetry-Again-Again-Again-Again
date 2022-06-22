using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndex : MonoBehaviour
{
    [Range(1, 4)] public int currentPlayerNo;
    [SerializeField] private GameObject[] PlayerBody;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerNo();
    }

    private void SetPlayerBody()
	{
        PlayerBody[currentPlayerNo - 1].SetActive(true);
        return;
    }

   private void SetPlayerNo()
	{
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        currentPlayerNo = gameManager.PlayerToAdd;
        gameManager.PlayerToAdd += 1;
        SetPlayerBody();
        return;
	}
}
