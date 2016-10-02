using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring_anims : MonoBehaviour {

	public float scaleTime;

	void Start(){
		StartCoroutine (Scale());
	}
	IEnumerator Scale(){
		Vector3[] ringScale = new Vector3[4];
		ringScale [0] = new Vector3 (0, 0, 0);// Zero
		ringScale [3] = new Vector3 (1, 1, 1);// One
		ringScale[1] = new Vector3 (ringScale[3].x + 0.25f, 1, ringScale[3].z + 0.25f);//Stretch
		ringScale[2] = new Vector3 (ringScale[3].x - 0.125f, 1, ringScale[3].z - 0.125f);//Squash


		transform.localScale = ringScale[0];

		for (int i = 0; i < 3; i++) {
			float t = 0;
			while (transform.localScale != ringScale [i + 1]) {
				t += scaleTime / 100;
				transform.localScale = Vector3.Lerp (ringScale [i], ringScale [i + 1], t);
				yield return null;
			}
		}
	}

	public IEnumerator Unscale(){
		Vector3[] ringScale = new Vector3[3];
		ringScale [2] = new Vector3 (0, 0, 0);// Zero
		ringScale [0] = new Vector3 (1, 1, 1);// One
		ringScale[1] = new Vector3 (ringScale[0].x + 0.25f, 1, ringScale[0].z + 0.25f);//Stretch

		transform.localScale = ringScale[0];

		for (int i = 0; i < 2; i++) {
			float t = 0;
			while (transform.localScale != ringScale [i + 1]) {
				t += scaleTime / 100;
				transform.localScale = Vector3.Lerp (ringScale [i], ringScale [i + 1], t);
				yield return null;
			}
		}
		Destroy (gameObject);
	}
}
