#pragma strict

public var sphereA : GameObject;
public var sphereB : GameObject;
public var inst : GameObject;
public var HUDCanvas : GameObject;


function Update () {
	var distanceX = sphereA.transform.position.x - sphereB.transform.position.x; //Vector3.Distance(sphereA.transform.position.x, sphereB.transform.position.x);
	var distanceZ = sphereA.transform.position.z - sphereB.transform.position.z;//Vector3.Distance(sphereA.transform.position.z, sphereB.transform.position.z);
	
	
	//inst.transform.SetParent(HUDCanvas.transform, true);
	var distance = Vector3.Distance(sphereA.transform.position, sphereB.transform.position);
	//Instantiate (inst, Vector3(50, 50, 50), Quaternion.identity);

	if(distance < 30){
		Debug.Log("Distance: " + distance);	
		inst.SetActive(true);
		inst.transform.position.y = 8;
	}

	//if((distanceX < 150 && distanceX > -150) && (distanceZ < 150 && distanceZ > -150)){
	//		Debug.Log("DistanceX: " + distanceX);
	//		Debug.Log("DistanceZ: " + distanceZ);	
	//}
}