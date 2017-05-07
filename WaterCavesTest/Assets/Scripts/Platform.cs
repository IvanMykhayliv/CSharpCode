using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void Update() {
		
	}
}
