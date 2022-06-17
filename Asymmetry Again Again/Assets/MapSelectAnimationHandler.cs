using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectAnimationHandler : MonoBehaviour
{
	public Animator anim;
    public GameObject[] AnimationOBJS;

	public int AnimationNumber;
	public int AnimationToPlay;
    public void DecideAnimationToPlay()
	{
		AnimationNumber = Random.Range(0, AnimationOBJS.Length);
		AnimationToPlay = AnimationNumber;
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		if (AnimationToPlay == 0)
		{
			anim.Play("Animation One");
		}
		else if (AnimationToPlay == 1)
		{
			anim.Play("Animation Two");
		} 
		else if (AnimationToPlay == 2)
		{
			anim.Play("Animation Three");
		}
	}
}
