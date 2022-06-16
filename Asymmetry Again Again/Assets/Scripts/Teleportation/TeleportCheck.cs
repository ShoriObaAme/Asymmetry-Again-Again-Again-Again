using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCheck : MonoBehaviour
{
	public bool hasTeleported = false;
	[SerializeField] private float TeleportCooldownDuration;

	private void Update()
	{
		if (hasTeleported) StartCoroutine(TeleportCooldown());
	}

	private IEnumerator TeleportCooldown()
	{
		yield return new WaitForSecondsRealtime(TeleportCooldownDuration);
		hasTeleported = false;
	}
}
