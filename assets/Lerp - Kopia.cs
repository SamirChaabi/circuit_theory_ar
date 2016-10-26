using UnityEngine;
using System.Collections;

public class Lerp : MonoBehaviour {

	int speed = 1;
	Vector2 pos;

	void Start(){
		pos.x = this.transform.position.x;
		pos.y = this.transform.position.y;
	}
	

	// Update is called once per frame
	void Update () {
		float i = Mathf.PingPong(Time.time * speed, 1);
		Vector2 newPos = new Vector2(this.transform.position.x, this.transform.position.y+10);
		transform.position = Vector2.Lerp(pos, newPos, i);
		newPos = new Vector2(this.transform.position.x, this.transform.position.y-10);
		transform.position = Vector2.Lerp(newPos, pos, i);
		//transform.position = pos; //Translate(new Vector3(this.transform.position.x, this.transform.position.y-2, this.transform.position.z));
	}
}