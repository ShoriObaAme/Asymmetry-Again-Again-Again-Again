using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosScript : MonoBehaviour
{

    public Transform cameraPos;
	public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        AssignCameraPos(); 
    }

	private void OnEnable()
	{
        AssignCameraPos();
	}

	private void AssignCameraPos()
	{
        transform.position = cameraPos.position + offset;
	}

	private void UnassignCameraPos()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}
