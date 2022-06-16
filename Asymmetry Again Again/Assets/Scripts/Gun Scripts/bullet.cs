using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Glass")
		{
			GlassDestroy GC = collision.gameObject.GetComponent<GlassDestroy>();
			GC.Shatter();
		}
	}
}
