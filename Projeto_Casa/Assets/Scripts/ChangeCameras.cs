﻿using UnityEngine;
using System.Collections;

public class ChangeCameras : MonoBehaviour {

	public Camera[] cams;

	public void CamOneMove(){
		cams [0].enabled = true;
		cams [1].enabled = false;
	}
	public void CamTwoMove(){
		cams [0].enabled = false;
		cams [1].enabled = true;
	}
}