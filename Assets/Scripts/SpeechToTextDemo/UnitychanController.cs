using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanController : MonoBehaviour
{

    private Animator _animator;

	// Use this for initialization
	void Start ()
	{
	    _animator = GetComponent<Animator>();
	}

    public void ChangeMotion(string motionKeyword)
    {
        if (_animator == null)
        {
            return;
        }

        if (motionKeyword.Contains("go"))
        {
            _animator.CrossFade("Run", 0f);
        }
        else if (motionKeyword.Contains("jump"))
        {
            _animator.CrossFade("Jump", 0f);
        }
        else if (motionKeyword.Contains("stop"))
        {
            _animator.CrossFade("Last", 0f);
        }
    }
}
