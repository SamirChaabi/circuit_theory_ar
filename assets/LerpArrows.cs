using UnityEngine;
using System.Collections;

public class LerpArrows : MonoBehaviour {

	private int speed = 1;
	private Vector2 pos;
	private float i;


	void Start(){
		pos.x = this.transform.position.x;
		pos.y = this.transform.position.y;
	}
	

	// Update is called once per frame
	void Update () {
		i = Mathf.PingPong(Time.time * speed, 1);
		switch (gameObject.transform.parent.name) {
			case "ArrowsLeft":
			Vector2 newPosLeft = new Vector2(this.transform.position.x, this.transform.position.y+10);
			transform.position = Vector2.Lerp(pos, newPosLeft, i);
			newPosLeft = new Vector2(this.transform.position.x, this.transform.position.y-10);
			transform.position = Vector2.Lerp(newPosLeft, pos, i);
			break;
		case "ArrowsRight":
			Vector2 newPosRight = new Vector2(this.transform.position.x, this.transform.position.y-10);
			transform.position = Vector2.Lerp(pos, newPosRight, i);
			newPosRight = new Vector2(this.transform.position.x, this.transform.position.y+10);
			transform.position = Vector2.Lerp(newPosRight, pos, i);
			break;
		case "ArrowsUp":
			Vector2 newPosUp = new Vector2(this.transform.position.x+10, this.transform.position.y);
			transform.position = Vector2.Lerp(pos, newPosUp, i);
			newPosUp = new Vector2(this.transform.position.x-10, this.transform.position.y);
			transform.position = Vector2.Lerp(newPosUp, pos, i);
			break;
		case "ArrowsDown":
			Vector2 newPosDown = new Vector2(this.transform.position.x-10, this.transform.position.y);
			transform.position = Vector2.Lerp(pos, newPosDown, i);
			newPosDown = new Vector2(this.transform.position.x+10, this.transform.position.y);
			transform.position = Vector2.Lerp(newPosDown, pos, i);
			break;
		}

		//transform.position = pos; //Translate(new Vector3(this.transform.position.x, this.transform.position.y-2, this.transform.position.z));
	}
}