using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public float scaleTime;
	public float shootSpeed;
	public GameObject ball;
	public GameObject shootPt;
	public GameObject Gun;
	public GameObject curPointer;
	public GameObject ParticleObject;
	public AudioClip shootFX, squishFX, MoveFX;

	private AudioSource audio;
	private bool isRunning;
	private ParticleSystem particleSystem;

	void Start () {
		audio = GetComponent<AudioSource> ();
		particleSystem = ParticleObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && !isRunning){
			if(curPointer != null){
			StartCoroutine(curPointer.GetComponent<Ring_anims> ().Unscale());
			}
			StartCoroutine(ScaleGun ());
		}

		if(Input.GetKeyDown(KeyCode.RightCommand) && curPointer != null){
			audio.PlayOneShot (MoveFX);
			transform.position = new Vector3(curPointer.transform.position.x,  transform.position.y, curPointer.transform.position.z);
		}
	}

	IEnumerator ScaleGun(){
		isRunning = true;
		Vector3[] ScaleGun = new Vector3[5];
		ScaleGun[0] = Gun.transform.localScale;
		ScaleGun[1] = new Vector3 (ScaleGun[0].x + 0.05f, ScaleGun[0].y +0.025f, ScaleGun[0].z - 0.5f);
		ScaleGun[2] = new Vector3 (ScaleGun[0].x - 0.025f, ScaleGun[0].y - 0.05f, ScaleGun[0].z + 0.25f);
		ScaleGun[3] = new Vector3 (ScaleGun[0].x + 0.0125f, ScaleGun[0].y + 0.025f, ScaleGun[0].z - 0.125f);
		ScaleGun [4] = ScaleGun [0];

		Gun.transform.localScale = ScaleGun[0];
		audio.PlayOneShot (squishFX);
		for (int i = 0; i < 2; i++) {
			float t = 0;
			while (Gun.transform.localScale != ScaleGun [i + 1]) {
				t += scaleTime / 100;
				Gun.transform.localScale = Vector3.Lerp (ScaleGun [i], ScaleGun [i + 1], t);
				yield return null;
			}
		}
		particleSystem.Play ();
		GameObject Go = Instantiate (ball, shootPt.transform.position, shootPt.transform.rotation)as GameObject;
		Go.GetComponent<Rigidbody>().AddForce (shootPt.transform.forward * shootSpeed);
		audio.PlayOneShot (shootFX);

		for (int i = 2; i < 4; i++) {
			float t = 0;
			while (Gun.transform.localScale != ScaleGun[i+1]) {
				t += scaleTime / 100;
				Gun.transform.localScale = Vector3.Lerp (ScaleGun[i], ScaleGun[i+1], t);
				yield return null;
			}
		}
		isRunning = false;
	}
}