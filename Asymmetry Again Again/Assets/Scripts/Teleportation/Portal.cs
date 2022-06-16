using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    public Transform targetDestination;

    private TeleportCheck TC;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			TC = other.GetComponent<TeleportCheck>();
			if (TC.hasTeleported == false)
			{
				other.transform.position = targetDestination.position;
				//other.transform.rotation = new Quaternion.Identity (other.transform.rotation.x, targetDestination.transform.forward, other.transform.rotation.z);
				TC.hasTeleported = true;
			}
		}
	}
}
