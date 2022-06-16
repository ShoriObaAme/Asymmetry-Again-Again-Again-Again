using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDestroy : MonoBehaviour
{
	[SerializeField] private BoxCollider BC;
    public ParticleSystem GlassShatter;
	public GameObject GlassPane;
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Shatter();
		}
	}

	public void Shatter()
	{
		GlassPane.SetActive(false);
		GlassShatter.Play();
		BC.enabled = false;
		this.enabled = false;
	}
}
