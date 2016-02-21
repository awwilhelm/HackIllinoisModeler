using UnityEngine;
using System.Collections;

public class GUICrosshair : MonoBehaviour {

	public Texture2D crosshair;
	Rect position;
	static bool OriginalOn = true;

	// Use this for initialization
	void Start () {
		position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) / 2, crosshair.width, crosshair.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (OriginalOn) {
			GUI.DrawTexture(position, crosshair);
		}
	}
}
