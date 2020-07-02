using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{

	public Transform target;
	void Awake()
	{
		transform.LookAt(target);
	}
}
