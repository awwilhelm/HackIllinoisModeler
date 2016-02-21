using UnityEngine;
using System.Collections;
using Modeler;

[RequireComponent(typeof(Camera))]
public class ManipulateObjects : GameBehavior {

	// Use this for initialization
	void Start () {
	
	}

	public GameObject ballPrefab;
	public float smooth;

	float carryDistance = 5f;
	bool carryingObject = false;
	GameObject carriedObject;

	// Update is called once per frame
	void Update () {

		if (carryingObject) {
			carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, transform.position + transform.forward * carryDistance, Time.deltaTime * smooth);
			if (Input.GetMouseButtonDown(1)) {
				carryingObject = false;
				carriedObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
				carriedObject.GetComponent<SphereCollider>().enabled = true;

			}
		} else {
			Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2));
			RaycastHit hit;
			if (Input.GetMouseButtonDown(1)) {
				if (carryingObject) {
					carryingObject = false;
					carriedObject = null;
				}
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider.tag.Equals("Ball")) {
						Debug.Log("Hit ball");

						GameObject hitObject = hit.collider.gameObject;
						carryDistance = Vector3.Distance(hitObject.transform.position, transform.position);
						Color defaultOutlineColor = hitObject.GetComponent<Renderer>().material.GetColor("_OutlineColor");
						hitObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);

						carriedObject = hitObject;
						carriedObject.GetComponent<SphereCollider>().enabled = false;
						carryingObject = true;
					}
				}
			}
		}
		if (Input.GetMouseButtonDown(0)) {
			Instantiate(ballPrefab, transform.position + (transform.forward * 2), Quaternion.identity);
		}
	}
}
