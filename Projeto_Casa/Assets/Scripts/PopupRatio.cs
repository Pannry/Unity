﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp{
public class PopupRatio : MonoBehaviour {

		private GameObject panel;
		private GameObject canvas;
		private GameObject mySquare;
		private InputField[] args;
		private ClickEvent myEvents;

		public static void CreateRatioBox(GameObject popup, ClickEvent e, GameObject square){
			//Creating a Canvas:
			GameObject canvas = new GameObject("Canvas");
			Canvas c = canvas.AddComponent<Canvas>();
			c.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.AddComponent<CanvasScaler>();
			canvas.AddComponent<GraphicRaycaster>();

			//Creating a Panel:
			GameObject panel = Instantiate(popup);
			panel.transform.SetParent (canvas.transform, false);
			panel.GetComponent<PopupRatio> ().SetPopupObject (panel);
			panel.GetComponent<PopupRatio> ().SetCanvasObject (canvas);
			panel.GetComponent<PopupRatio> ().SetEventObject (e);
			panel.GetComponent<PopupRatio> ().SetAreaObject (square);

			InputField[] array = panel.GetComponentsInChildren<InputField> ();
			// A ordem é largura e comprimento.
			Button refresh = panel.GetComponentInChildren<Button>();
		}

		public void SetPopupObject(GameObject mypopup){
			panel = mypopup;
		}
			
		public void SetCanvasObject(GameObject mycanvas){
			canvas = mycanvas;
		}

		public void SetEventObject(ClickEvent e){
			myEvents = e;
		}

		public void SetAreaObject(GameObject square){
			mySquare = square;
		}

		// RB means refresh button, not right button
		public void OnClickRB(){
			string wid = panel.GetComponentsInChildren<InputField> ()[0].text;
			string len = panel.GetComponentsInChildren<InputField> ()[1].text;

			float myXRatio;
			float.TryParse (wid, out myXRatio);
			float myYRatio;
			float.TryParse (len, out myYRatio);

			LineRenderer lr = mySquare.GetComponent<LineRenderer> ();
			float worldXRatio = lr.GetPosition (1).x - lr.GetPosition (0).x;
			float worldYRatio = lr.GetPosition (2).z - lr.GetPosition (1).z;
			if (worldXRatio < 0)
				worldXRatio *= -1;
			if (worldYRatio < 0)
				worldYRatio *= -1;
			myXRatio = worldXRatio / myXRatio;
			myYRatio = worldYRatio / myYRatio;
			myEvents.SetRatios (myXRatio, myYRatio);
			Debug.Log (myYRatio);
			myEvents.popupOpen = false;
			Destroy (mySquare);
			Destroy (canvas);
		}
	}
}