using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public GameObject Circle;
	private Quaternion rot = Quaternion.Euler (90, 0, 0);

	void OnCollisionEnter(Collision Other){
		if(Other.gameObject.tag == "Ground"){
			GameObject Go = Instantiate (Circle, new Vector3(transform.position.x, 0.2f, transform.position.z), rot)as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.GetComponent<Shoot> ().curPointer = Go;
			Destroy (gameObject);
		}
	}
}
