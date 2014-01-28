using UnityEngine;
using System.Collections;

//small scrip for level changing by "level_name" parameter
public class LevelChanger : MonoBehaviour {

	public string level_name;

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "Player") {
			if (Input.GetButton("Vertical")) {
				Application.LoadLevel(level_name);
			}
		}
	}

}
