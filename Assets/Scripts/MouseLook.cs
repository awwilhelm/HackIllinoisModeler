using UnityEngine;
using System.Collections;
using Modeler;

public class RotateCamera : GameBehavior {

	public float scrollConstant = 4;
	public float transformConstant = 1.0F;
	public float rotationConstant = 6.0F;

	private Vector3 mouseOrigin;

	float zRot = 0;

	float scrollWheelValue = 0;

	public GameObject pivotPoint;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// rotate
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRot);
        
		if (Input.GetMouseButton(1)) {
			float rotateX = Input.GetAxis("Mouse X") * rotationConstant;
			float rotateY = Input.GetAxis("Mouse Y") * rotationConstant;

			if (Input.GetKey(KeyCode.LeftShift)) {
				Debug.Log("pivot");
				transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotateX);
			} else {
				transform.Rotate(new Vector3(rotateY, -rotateX, 0));
			}
		}
		// pan
		if (Input.GetMouseButton(2) && Input.GetButton("leftAlt")) {

			float transformX = Input.GetAxis("Mouse X") * transformConstant * -1;
			float transformY = Input.GetAxis("Mouse Y") * transformConstant * -1;

			transform.Translate(new Vector3(transformX, transformY, 0));
		}

		scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * scrollConstant;
		//zoom
		transform.Translate(transform.forward * scrollWheelValue);
	}
}
