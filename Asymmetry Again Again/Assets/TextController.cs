using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField] private TextMeshProUGUI[] TitleTextParts;

    // Update is called once per frame
    void Update()
    {
     foreach (TextMeshProUGUI titleTextPart in TitleTextParts)
		{
            titleTextPart.fontSharedMaterial.SetFloat("_FaceDilate", Time.deltaTime * scrollSpeed);
		}   
    }
}
