using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RingCollapse : MonoBehaviour
{
	[SerializeField] private GameObject[] rings;



	[Header("Time and Drop Settings")]
	[SerializeField] private bool AboutToDrop = false;
	[SerializeField] private bool dropWarned = false;
	public float StartTime;
	[SerializeField] private float TimeToDrop;
	[SerializeField] private int RingToDrop;
	[SerializeField, Range(1f, 1.5f)] private int TimerMulti;



	[Header("Gravity Settings")]
	[SerializeField, Range(12.5f, 25)] private float DropForce;
	[SerializeField] private float CrackEffect = -2f;
	[SerializeField] private float timeValue;

	// Start is called before the first frame update
	void Awake()
	{
		rings = GameObject.FindGameObjectsWithTag("Hellscape Ring");

		foreach (GameObject ring in rings)
		{
			Debug.Log("Rings Found. Instance ID: " + ring.GetInstanceID());
		}
		RingToDrop = rings.Length - 1;
		TimeToDrop = (StartTime * TimerMulti);
	}



	private void FixedUpdate()
	{
		CrackEffect = Mathf.Clamp(CrackEffect, -2, 0.07f);
		TimeToDrop -= Time.deltaTime;
		if (TimeToDrop < (StartTime / 2) && !dropWarned)
		{
			WarnDrop();
		}
		else if (TimeToDrop <= 0 && dropWarned && RingToDrop != 0)
		{
			Invoke("DropRing", 0.1f);
		}
		else if (TimeToDrop <= 0 && dropWarned && RingToDrop == 0)
		{
			DropRingDisable();
		}

		Material mat = rings[RingToDrop].GetComponent<MeshRenderer>().materials[1];
		if (dropWarned == true)
		{
			mat.SetFloat("Alpha", CrackEffect);
			Debug.Log("Crack effect set");
			CrackEffect += (Time.deltaTime * timeValue);
		}
	}



	private void WarnDrop()
	{
		Debug.Log(rings[RingToDrop].name + " will collapse in " + (StartTime / 2) + " seconds");
		Material mat = rings[RingToDrop].GetComponent<MeshRenderer>().materials[1];
		dropWarned = true;
	}



	private void DropRing()
	{
		dropWarned = false;
		Rigidbody RB = rings[RingToDrop].GetComponent<Rigidbody>();
		RB.isKinematic = false;
		RB.AddForce(-Vector3.up * DropForce, ForceMode.Impulse);
		TimeToDrop = (StartTime * TimerMulti);
		RingToDrop -= 1;
		CrackEffect = -2f;
	}



	private void DropRingDisable()
	{
		Rigidbody RB = rings[RingToDrop].GetComponent<Rigidbody>();
		RB.isKinematic = false;
		RB.AddForce(-Vector3.up * DropForce, ForceMode.Impulse);
		RingToDrop--;
		this.enabled = false;
	}
}