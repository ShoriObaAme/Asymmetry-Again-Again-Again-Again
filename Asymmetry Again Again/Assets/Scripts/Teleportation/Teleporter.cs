using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject TargetTeleporter;
    [SerializeField] private Transform TargetDestination;
    // Start is called before the first frame update
    void Awake()
    {
        TargetDestination = TargetTeleporter.GetComponent<Transform>();
        if (TargetDestination == null)
		{
            Debug.LogError("Target Destination has not been assigned.");
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && TargetDestination != null)
		{
            TeleportCheck TC = other.GetComponent<TeleportCheck>();
            if (TC.hasTeleported == false)
            {
                TC.hasTeleported = true;
                other.gameObject.transform.position = TargetDestination.position;
            }
            return;
        }
	}
}
