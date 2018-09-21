using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	void Start ()
	{
		rotationSpeed = Random.Range (1.0f, 3.0f) * 70;
	}
	float rotationSpeed;

	void Update ()
	{
		transform.Rotate (new Vector3 (0, 0, rotationSpeed) * Time.deltaTime);
	}
}
