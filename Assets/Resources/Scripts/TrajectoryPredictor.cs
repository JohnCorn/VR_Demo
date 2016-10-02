using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryPredictor : MonoBehaviour {

	public float throwForceX = 10;
	public float throwForceY = 10;

	public float simulationGranularity = 1;

	public float pointsToCapture = 50;

	private List<Vector3> trajectory;

	private bool thrown;
	private bool collided;

	private Rigidbody rb;
	private MeshCollider collider;


	private float startingX;
	private float x;
	private float g;
	private float yo;
	private float theta;
	private float v;


	public void Throw(){
		SimulatePhysics ();
		DebugDrawLine (Mathf.Infinity);
		rb.AddRelativeForce (throwForceX, throwForceY, 0, ForceMode.Impulse);
		thrown = true;
	}

	private void Awake(){
		rb = gameObject.GetComponent<Rigidbody> ();
		collider = gameObject.GetComponent<MeshCollider> ();
		trajectory = new List<Vector3> ();

		collided = false;

		CalculateStatics ();
	}


	private void Update(){
		if (!thrown)
			SimulateThrow ();
	}


	public void SimulateThrow(){
		SimulatePhysics ();
		DebugDrawLine (0.0f);
	}


	private void OnTriggerEnter(){
		collided = true;
	}


	private void CalculateStatics(){
		startingX = 0;

		g = -Physics.gravity.y;
		yo = 0;
		theta = Mathf.Atan(throwForceY / throwForceX);
		v = (new Vector2(throwForceX, throwForceY).magnitude)/rb.mass;

		Debug.Log ("Gravity: " + g + ". Yo: " + yo + ". Theta: " + theta + ". Velocity: "+v+".");

	}


	private float GetMaxTravelDistance(){
		float maxDist = Square (v) * Mathf.Sin (2 * theta) / g;
		return maxDist;
	}


	private void SimulatePhysics(){
		trajectory.Clear ();
		x = startingX;

		for (int i=0; i < pointsToCapture; i++){			
			float firstTerm = yo;
			float secondTerm = x * Mathf.Tan (theta);
			float thirdTerm = (g * Square (x)) / (2 * (Square(v * (Mathf.Cos (theta)))));
			float newY = firstTerm + secondTerm - thirdTerm;

			Vector3 newPos = new Vector3(x, newY, 0);
			Vector3 worldNewPos = transform.TransformDirection (newPos) +transform.position;
			trajectory.Add(worldNewPos);

			x += simulationGranularity;
		}
	}

	private void DebugDrawLine(float duration){
		Vector3 prevPos = transform.position;
		Vector3 startPos;
		Vector3 endPos;
		Debug.Log (prevPos);
		for (int i = 0; i < trajectory.Count; i++) {
			startPos = prevPos;
			endPos = trajectory [i];

			Debug.DrawLine (startPos, endPos, Color.green, duration);

			prevPos = endPos;
		}
	}

	private void DrawLine(float duration){
		// Your implementation here...
	}

	private float Square(float num){
		return num*num;
	}
}
