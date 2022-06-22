using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints SP;
    [SerializeField] public List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private int chosenSpawnPoint;
    [SerializeField] private SpawnPointChosen chosen;
    public float timeUntilCancel;
    // Start is called before the first frame update
    void OnEnable()
    {
        SP = this;
        foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("Spawn Point"))
		{
            spawnPoints.Add(spawnPoint);
		}
        return;
    }

    void chooseSpawnPoint()
	{

        chosenSpawnPoint = Random.Range(0, spawnPoints.Count);
        return;
	}

	private void LateUpdate()
	{
        Invoke("chooseSpawnPoint", 3f);
	}

	public void RespawnPlayer(GameObject Player)
	{
        chosen = spawnPoints[chosenSpawnPoint].GetComponent<SpawnPointChosen>();
        if (chosen.isChosen == false)
		{
            Player.transform.position = spawnPoints[chosenSpawnPoint].transform.position;
            chosen.isChosen = true;
            Invoke("resetIsChosen", timeUntilCancel);
            return;
		}
        else if (chosen.isChosen == true)
		{
            chooseSpawnPoint();
            Invoke("RespawnPlayer", 0.1f);
            return;
		}
        return;
	}

    private void resetIsChosen()
	{
        chosen.isChosen = false;
        return;
	}
}
