using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
