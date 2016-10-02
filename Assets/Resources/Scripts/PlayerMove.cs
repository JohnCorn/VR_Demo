using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public Transform Head;
	public float Speed;
	public float MoveSpeed;

	private bool gyrosupported;
	private Gyroscope gyro;
	private Quaternion rotFix;

	private float startY;

	void Start () {
		gyrosupported = SystemInfo.supportsGyroscope;
		Head = transform.GetChild (0);

		if(gyrosupported){
			gyro = Input.gyro;
			gyro.enabled = true;

			transform.rotation = Quaternion.Euler(90f, 180f, 0f);
			rotFix = new Quaternion (0, 0, 1, 0);
		}
	}

	void Update() {

		if(gyrosupported && startY == 0){
			ResetGyroRotation ();
		}
		Head.transform.localRotation = gyro.attitude * rotFix;
		//Head.transform.rotation  = Quaternion.Euler((((gyro.attitude.z * 90) +15) - headRotOffset)*-1, 0, 0);
		//transform.rotation  = Quaternion.Euler(0, gyro.attitude.x * 90, 0);

	}

	void ResetGyroRotation (){
		startY = transform.eulerAngles.y;

	}
}
