﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class cameraEffectController : MonoBehaviour {

	public bool isUsingTDevice = false;

	public Shader shader;
	public void Awake() 
	{
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.D))
		{
			if(isUsingTDevice)
			{
				isUsingTDevice = false;
			}
			else
			{
				isUsingTDevice = true;
			}
		}
			
		if(isUsingTDevice)
		{
			transform.camera.SetReplacementShader(shader, null);
		}
		else
		{
			transform.camera.SetReplacementShader(null, null);
		}

	}
}
