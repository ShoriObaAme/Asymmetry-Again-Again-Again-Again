using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
	[SerializeField]private UpdatedCharacterController UCC;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			UCC.Shield();
			Destroy(other.gameObject);
		}
	}
}
