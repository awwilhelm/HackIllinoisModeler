using UnityEngine;
using System.Collections;
using Modeler;

public class RotateCamera : GameBehavior {

	public float scrollConstant = 4;
	public float transformConstant = 1.0F;
	public float rotationConstant = 100.0F;

	private Vector3 mouseOrigin;

	float scrollWheelValue = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// rotate

		if (Input.GetMouseButtonDown(0)) {
			mouseOrigin = Input.mousePosition;
		}

		if (Input.GetMouseButton(0)) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * rotationConstant);
			transform.RotateAround(transform.position, Vector3.up, pos.x * rotationConstant);
		}
		// pan
		if (Input.GetMouseButton(1)) {

			float transformX = Input.GetAxis("Mouse X") * transformConstant * -1;
			float transformY = Input.GetAxis("Mouse Y") * transformConstant * -1;

			transform.Translate(new Vector3(transformX, transformY, 0));
		}
		scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * scrollConstant;
		//zoom
		transform.Translate(transform.forward * scrollWheelValue);
	}
}
