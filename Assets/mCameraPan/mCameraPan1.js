// camera panning v1.0 - mgear - http://unitycoder.com/blog

#pragma strict

private var startpoint:Vector3; // startpoint
private var endpoint:Vector3; // endpoint
private var panning:boolean=false;

// mainloop
function Update ()
{

	// starts here, left mousebutton is pressed down
	if(Input.GetMouseButtonDown(0)) 
	{
		// make ray
		var ray1 : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		startpoint = ray1.GetPoint (10) ; // Returns a point at distance units along the ray
		startpoint.z = 0; // fix z to 0
		panning = true;
	}

	// then left mousebutton is released
	if(panning) 
	{
		var ray2 : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		endpoint = ray2.GetPoint (10) ; // Returns a point at distance units along the ray
		endpoint.z = 0; // fix z, somehow its not always 0?
		
		// åanning
		transform.position+=startpoint-endpoint;
	}
	
	
	if(Input.GetMouseButtonUp(0)) // release button, stop pan
	{
		panning = false;
	}


}