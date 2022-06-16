using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBagScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerKnockBackValue PKBV = other.gameObject.GetComponent<PlayerKnockBackValue>();
			PKBV.LaunchPlayer(transform);
			Destroy(this.gameObject);
		}
	}
}
