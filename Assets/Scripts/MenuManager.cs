using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {


	bool playing = false;
			
	Vector3 menuPosition = new Vector3(0, 280, 0);
	Vector3 menuOldPosition;
	
	// Use this for initialization
	void Start () {
	}
	
	public float speed = 0.1F;
	float lerpTime = 1f;
	float currentLerpTime;
	
	
	// Update is called once per frame
	void Update () {		
		if (Input.GetKeyDown("space")) {
			menuStatus = !menuStatus;
			movingTowardsMenu = !movingTowardsMenu;
			currentLerpTime = 0f;
			changeMenuStatus();
		}
		
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
		
		float perc = currentLerpTime / lerpTime;
		
		
		if (changingMenuStatus) {
			if (movingTowardsMenu) {
				transform.position = Vector3.Lerp(transform.position, menuPosition, perc);
			}
			if (!movingTowardsMenu) {
				transform.position = Vector3.Lerp(transform.position, menuOldPosition, perc);
			}
			
			if (perc.Equals(1)) {
				changingMenuStatus = false;
			}
			
		}
		
	}
	
	bool menuStatus = false;

	public bool getMenuStatus () {
		Debug.Log(menuStatus);
		return menuStatus;
	}

	private bool changingMenuStatus = false;	
	private float timeStartedLerping;
	bool movingTowardsMenu;
	
	void changeMenuStatus () {
		changingMenuStatus = true;
		//go to menu
		if (movingTowardsMenu) {
			Debug.Log ("going to menu");
			menuOldPosition = transform.position;
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<CapsuleCollider>().enabled = false;
		}
		//leave menu
		if (!movingTowardsMenu) {
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<CapsuleCollider>().enabled = true;
		}
	}
	
}
