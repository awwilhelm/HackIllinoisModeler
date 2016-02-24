using UnityEngine;
using System.Collections;
using Modeler;

[RequireComponent(typeof(Camera))]
public class ToolManager : GameBehavior {

	// Use this for initialization
	void Start () {
		selectedPrefab = ballPrefab;
	}

	public GameObject ballPrefab;

	GameObject selectedPrefab;
	Color selectedColor = Color.white;
	Material selectedMaterial;

	public float smooth;

	[System.Serializable]
	public class Tool
	{
		public string name;
		public GameObject prefab;
	}

	public Tool[] tools;

	float placementDelay = 1;
	float carryDistance = 5f;
	bool carryingObject = false;
	GameObject carriedObject;
	GameObject hitUIObject;

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
					if (!GetComponent<MenuManager>().getMenuStatus()) {
						if (hit.collider.tag.Equals("Ball")) {
							Debug.Log("Hit ball");

							if (hitUIObject != null) {
								hitUIObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
							}

							hitUIObject = hit.collider.gameObject;
							carryDistance = Vector3.Distance(hitUIObject.transform.position, transform.position);
							Color defaultOutlineColor = hitUIObject.GetComponent<Renderer>().material.GetColor("_OutlineColor");
							hitUIObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);

							string prefabLocation = "prefabs/" + hitUIObject.name;
							carriedObject = hitUIObject.gameObject;
							carriedObject.GetComponent<SphereCollider>().enabled = false;
							carryingObject = true;
						}
					}
					if (GetComponent<MenuManager>().getMenuStatus()) {
						if (hit.collider.tag.Equals("ColorOption")) {
							Debug.Log("color");

//							hitUIObject.GetComponent<Renderer>().sharedMaterial.SetColor("_OutlineColor", Color.black);
							hitUIObject = hit.collider.gameObject;
//							hitUIObject.GetComponent<Renderer>().sharedMaterial.SetColor("_OutlineColor", Color.green);

							selectedColor = hitUIObject.GetComponent<Renderer>().material.GetColor("_Color");
						}
						if (hit.collider.tag.Equals("SizeOption")) {
							selectedPrefab = hit.collider.gameObject;
						}
					}
				}
			}
		}
		if (Input.GetMouseButton(0) && !carryingObject) {
			placeObject(selectedPrefab, 5);
		}
	}

	int placeDelayTimer = 0;

	void placeObject (GameObject prefabToPlace, int placeFrequency) {
		if (placeDelayTimer == 0) {
			GameObject newObject = (GameObject) Instantiate(prefabToPlace, transform.position + (transform.forward * 2), Quaternion.identity);
			newObject.GetComponent<Renderer>().material.SetColor("_Color", selectedColor);

		}
		if (placeDelayTimer >= placeFrequency) {
			placeDelayTimer = 0;
		} else {
			placeDelayTimer++;
		}
	}

}



public abstract class Tool {
	public abstract string name {get; set;}
	public abstract void Use();
}

public class BallPlacer: Tool {
	public BallPlacer () {
		this.name = "BallPlacer";
	}
}